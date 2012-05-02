package edu.forum.server.network.reactor;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.nio.channels.ClosedChannelException;
import java.nio.channels.SelectionKey;
import java.nio.channels.Selector;
import java.nio.channels.ServerSocketChannel;
import java.util.Iterator;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;
import java.util.logging.Logger;

import edu.forum.server.network.tokenizer.TokenizerFactory;
import edu.forum.shared.network.protocol.ServerProtocolFactory;

/**
 * An implementation of the Reactor pattern.
 */
public class Reactor implements Runnable {
    private static final Logger logger = Logger.getLogger("edu.spl.reactor");
    private final int _port;
    private final int _poolSize;
    private final int _waitingTime = 2000;
    private final ServerProtocolFactory _protocolFactory;
    private final TokenizerFactory _tokenizerFactory;
    private volatile boolean _shouldRun = true;
    private ReactorData _data;
    private int _incomingBytes;
    private int _outgoingBytes;
    private int _connectionsNum;
    private long _connectionLatency;

    /**
     * Creates a new Reactor
     *
     * @param poolSize
     *            the number of WorkerThreads to include in the ThreadPool
     * @param port
     *            the port to bind the Reactor to
     * @param protocol
     *            the protocol factory to work with
     * @param tokenizer
     *            the tokenizer factory to work with
     */
    public Reactor(int port, int poolSize, ServerProtocolFactory protocol, TokenizerFactory tokenizer) {
        _port = port;
        _poolSize = poolSize;
        _protocolFactory = protocol;
        _tokenizerFactory = tokenizer;
    }

    /**
     * Create a non-blocking server socket channel and bind to to the Reactor
     * port
     */
    private ServerSocketChannel createServerSocket(int port)
        throws IOException {
        try {
            ServerSocketChannel ssChannel = ServerSocketChannel.open();
            ssChannel.configureBlocking(false);
            ssChannel.socket().bind(new InetSocketAddress(port));
            return ssChannel;
        } catch (IOException e) {
            logger.info("Port " + port + " is busy");
            throw e;
        }
    }

    /**
     * Main operation of the Reactor:
     * <UL>
     * <LI>Uses the <CODE>Selector.select()</CODE> method to find new
     * requests from clients
     * <LI>For each request in the selection set:
     * <UL>
     * If it is <B>acceptable</B>, use the ConnectionAcceptor to accept it,
     * create a new ConnectionHandler for it register it to the Selector
     * <LI>If it is <B>readable</B>, use the ConnectionHandler to read it,
     * extract messages and insert them to the ThreadPool
     * </UL>
     */
    public void run() {
        // Create & start the ThreadPool
        ExecutorService executor = Executors.newFixedThreadPool(_poolSize);
        Selector selector = null;
        ServerSocketChannel ssChannel = null;

        try {
            selector = Selector.open();
            ssChannel = createServerSocket(_port);
        } catch (IOException e) {
            logger.info("cannot create the selector -- server socket is busy?");
            return;
        }

        _data = new ReactorData(executor, selector, _protocolFactory, _tokenizerFactory);
        ConnectionAcceptor connectionAcceptor = new ConnectionAcceptor( ssChannel, _data);

        // Bind the server socket channel to the selector, with the new
        // acceptor as attachment

        try {
            ssChannel.register(selector, SelectionKey.OP_ACCEPT, connectionAcceptor);
        } catch (ClosedChannelException e) {
            logger.info("server channel seems to be closed!");
            return;
        }

        while (_shouldRun && selector.isOpen()) {
            // Wait for an event
            try {
                selector.select();
            } catch (IOException e) {
                logger.info("trouble with selector: " + e.getMessage());
                continue;
            }

            // Get list of selection keys with pending events
            Iterator<SelectionKey> it = selector.selectedKeys().iterator();

            // Process each key
            while (it.hasNext()) {
                // Get the selection key
                SelectionKey selKey = (SelectionKey) it.next();

                // Remove it from the list to indicate that it is being
                // processed. it.remove removes the last item returned by next.
                it.remove();

                // Check if it's a connection request
                if (selKey.isValid() && selKey.isAcceptable()) {
                	long cl = System.currentTimeMillis();
                    ConnectionAcceptor acceptor = (ConnectionAcceptor) selKey.attachment();
                    try {
                        acceptor.accept();
                        _connectionsNum++; //Increases the _connectionsNum
                        cl = System.currentTimeMillis() - cl; //stops the stopper
                        _connectionLatency += cl; //adds to the sum of _connectionLatency
                    } catch (IOException e) {
                        logger.info("problem accepting a new connection: "
                                    + e.getMessage());
                    }
                    continue;
                }
                // Check if a message has been sent
                if (selKey.isValid() && selKey.isReadable()) {
                    ConnectionHandler handler = (ConnectionHandler) selKey.attachment();
                    _incomingBytes += handler.read();
                }
                // Check if there are messages to send
                if (selKey.isValid() && selKey.isWritable()) {
                    ConnectionHandler handler = (ConnectionHandler) selKey.attachment();
                    logger.info("Channel is ready for writing");
                    _outgoingBytes += handler.write();
                }
            }
        }
        stopReactor();
    }

    /**
     * Returns the listening port of the Reactor
     *
     * @return the listening port of the Reactor
     */
    public int getPort() {
        return _port;
    }

    /**
     * Stops the Reactor activity, including the Reactor thread and the Worker
     * Threads in the Thread Pool.
     */
    public synchronized void stopReactor() {
        if (!_shouldRun)
            return;
        _shouldRun = false;
        _data.getSelector().wakeup(); // Force select() to return
        _data.getExecutor().shutdown();
        System.out.print("Incoming bytes: " + _incomingBytes + "\n" +
        		"Outgoing bytes: " + _outgoingBytes + "\n" +
        		"Connections number: " + _connectionsNum + "\n" +
        		"Incoming messages: " + this._data.get_incomingMessages() + "\n" +
        		"Connection latency: " + _connectionLatency + " msec -- " + (double)_connectionLatency/(double)_connectionsNum + " msec per connection average" + "\n" +
        		"Response latency: " + this._data.get_responseLatency() + " msec -- " + (double)this._data.get_responseLatency()/(double)this._data.get_incomingMessages() + " msec per response average" + "\n" +
        		"Read Fragmentation Level: " + (double)this._data.get_readFragmentationCounter()/(double)this._data.get_readCounter() + "\n" +
        		"Write Fragmentation Level: " + (double)this._data.get_writeFragmentationCounter()/(double)this._data.get_writeCounter() + "\n");
        try {
            _data.getExecutor().awaitTermination(_waitingTime, TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            // Someone didn't have patience to wait for the executor pool to
            // close
            e.printStackTrace();
        }
    }
}
