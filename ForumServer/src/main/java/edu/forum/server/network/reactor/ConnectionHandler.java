package edu.forum.server.network.reactor;

import java.io.IOException;
import java.net.SocketAddress;
import java.nio.ByteBuffer;
import java.nio.channels.CancelledKeyException;
import java.nio.channels.SelectionKey;
import java.nio.channels.SocketChannel;
import java.nio.charset.CharacterCodingException;
import java.util.Vector;
import java.util.logging.Logger;

import edu.forum.server.network.tokenizer.StringMessageTokenizer;
import edu.forum.shared.network.protocol.AsyncServerProtocol;

/**
 * Handles messages from clients
 */
public class ConnectionHandler implements Sender {
    private static final int BUFFER_SIZE = 1024;
    protected final SocketChannel _sChannel;
    protected final ReactorData _data;
    protected final AsyncServerProtocol _protocol;
    protected final StringMessageTokenizer _tokenizer;
    protected Vector<ByteBuffer> _outData = new Vector<ByteBuffer>();
    protected final SelectionKey _skey;
    private static final Logger logger = Logger.getLogger("edu.spl.reactor");
    private ProtocolTask _task = null;

    /**
     * Creates a new ConnectionHandler object
     *
     * @param sChannel
     *            the SocketChannel of the client
     * @param data
     *            a reference to a ReactorData object
     */
    private ConnectionHandler(SocketChannel sChannel, ReactorData data, SelectionKey key) {
        _sChannel = sChannel;
        _data = data;
        _protocol = _data.getProtocolMaker().create();
        _tokenizer = _data.getTokenizerMaker().create();
        _skey = key;
    }

    // make sure 'this' does not escape b4 the object is fully constructed!
    private void initialize() {
        _skey.attach(this);
        _task = new ProtocolTask(_protocol, _tokenizer, this);
    }

    /**
     * @param sChannel the SocketChannel
     * @param data the ReactorData
     * @param key the SelectionKey
     * @return the ConnectionHandler
     */
    public static ConnectionHandler create(SocketChannel sChannel, ReactorData data, SelectionKey key) {
        ConnectionHandler h = new ConnectionHandler(sChannel, data, key);
        h.initialize();
        return h;
    }

    /**
     * @param buf the ByteBuffer
     */
    public synchronized void addOutData(ByteBuffer buf) {
        _outData.add(buf);
        switchToReadWriteMode();
    }

    private void closeConnection() {
        // remove from the selector.
        _skey.cancel();
        try {
            _sChannel.close();
        } catch (IOException ignored) {
            ignored = null;
        }
    }

    /**
     * Reads incoming data from the client:
     * <UL>
     * <LI>Reads some bytes from the SocketChannel
     * <LI>create a protocolTask, to process this data, possibly generating an
     * answer
     * <LI>Inserts the Task to the ThreadPool
     * </UL>
     *
     */
    public int read() {
        // do not read if protocol has terminated. only write of pending data is
        // allowed
        if (_protocol.shouldClose()) {
            return 0;
        }

        SocketAddress address = _sChannel.socket().getRemoteSocketAddress();
        logger.info("Reading from " + address);
        _data.set_readCounter(_data.get_readCounter() + 1);
        ByteBuffer buf = ByteBuffer.allocate(BUFFER_SIZE);
        int numBytesRead = 0;
        try {
            numBytesRead = _sChannel.read(buf);
        } catch (IOException e) {
            numBytesRead = -1;
        }
        // is the channel closed?
        if (numBytesRead == -1) {
            // No more bytes can be read from the channel
            logger.info("client on " + address + " has disconnected");
            closeConnection();
            // tell the protocol that the connection terminated.
            _protocol.connectionTerminated();
            return numBytesRead;
        }
        //add the buffer to the protocol task
        buf.flip();
        _task.addBytes(buf);
        // add the protocol task to the reactor
        _data.getExecutor().execute(_task);
		return numBytesRead;
    }

    /**
     * attempts to send data to the client<BR>
     * if all the data has been succesfully sent, the ConnectionHandler will
     * automatically switch to read only mode, otherwise it'll stay in its
     * current mode (which is read / write).
     */
    public synchronized int write() {
    	int ans = 0;
        if (_outData.size() == 0) {
            // if nothing left in the output string, go back to read mode
            switchToReadOnlyMode();
            return ans;
        }
        // if there is something to send
        ByteBuffer buf = _outData.remove(0);
        ans = buf.position();
        if (buf.remaining() != 0) {
            try {
                _sChannel.write(buf);
                _data.set_writeCounter(_data.get_writeCounter()+1); //increases every time we send out bytes
                
            } catch (IOException e) {
                // If client closed connection without reading
                closeConnection();
                SocketAddress address = _sChannel.socket().getRemoteSocketAddress();
                logger.info("client disconnected on " + address);
            }
            // check if the buffer contains more data
            if (buf.remaining() != 0) {
                _outData.add(0, buf);
                _data.set_writeFragmentationCounter(_data.get_writeFragmentationCounter() + 1); //if it dodn't send all the bytes
            }
        }
        ans = buf.position() - ans;
        // check if the protocol indicated close.
        if (_protocol.shouldClose()) {
            switchToWriteOnlyMode();
            if (buf.remaining() == 0) {
                closeConnection();
                SocketAddress address = _sChannel.socket().getRemoteSocketAddress();
                logger.info("disconnecting client on " + address);
            }
        }
		return ans;
    }

    /**
     * switches the handler to read / write TODO Auto-generated catch blockmode
     *
     */
    public void switchToReadWriteMode() {
        try {
            _skey.interestOps(SelectionKey.OP_READ | SelectionKey.OP_WRITE);
            _data.getSelector().wakeup();
        } catch (CancelledKeyException e) {
            logger.info("Client disconnected");
        }
    }

    /**
     * switches the handler to read only mode
     *
     */
    public void switchToReadOnlyMode() {
        _skey.interestOps(SelectionKey.OP_READ);
        _data.getSelector().wakeup();
    }

    /**
     * switches the handler to write only mode
     *
     * ClosedChannelException
     *             if the channel is closed
     */
    public void switchToWriteOnlyMode() {
        _skey.interestOps(SelectionKey.OP_WRITE);
        _data.getSelector().wakeup();
    }

    /**
     * Frame and send a string to client
     * Invoked by protocol when it needs to send to various clients.
     *
     * @param s string to be framed (not encoded, no delimiter)
     */
    public void send(String s) {
        try {
            ByteBuffer bytes = _tokenizer.getBytesForMessage(s);
            addOutData(bytes);
        } catch (CharacterCodingException e) { e.printStackTrace(); }
    }
}
