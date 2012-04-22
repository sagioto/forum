package edu.forum.server.network.reactor;

import java.nio.ByteBuffer;
import java.util.Vector;

import edu.forum.server.network.tokenizer.StringMessageTokenizer;
import edu.forum.shared.network.protocol.ServerProtocol;

/**
 * This class supplies some data to the protocol, which then processes the data,
 * possibly returning a reply. This class is implemented as an executor task.
 *
 */
/**
 * @author Sagi
 *
 */
public class ProtocolTask implements Runnable {
    private final ServerProtocol _protocol;
    private final StringMessageTokenizer _tokenizer;
    private final ConnectionHandler _handler;

    /**
     * the fifo queue, which holds data coming from the socket. Access to the
     * queue is serialized, to ensure correct processing order.
     */
    private final Vector<ByteBuffer> _buffers = new Vector<ByteBuffer>();

    /**
     * @param protocol the protocol
     * @param tokenizer the StringMessageTokenizer
     * @param h the ConnectionHandler
     */
    public ProtocolTask(final ServerProtocol protocol,
                        final StringMessageTokenizer tokenizer,
                        final ConnectionHandler h) {
        this._protocol = protocol;
        this._tokenizer = tokenizer;
        this._handler = h;
    }

    // we synchronize on ourselves, in case we are executed by several threads
    // from the thread pool.
    /* (non-Javadoc)
     * @see java.lang.Runnable#run()
     */
    public synchronized void run() {
        // first, add all the bytes we have to the tokenizer
        synchronized (_buffers) {
            while(_buffers.size() > 0) {
                ByteBuffer buf = _buffers.remove(0);
                _tokenizer.addBytes(buf);
            }
        }
if(!_tokenizer.hasMessage()){
	_handler._data.set_readFragmentationCounter(_handler._data.get_readFragmentationCounter()+1);
        }
        // now, go over all complete messages and process them.
        while (_tokenizer.hasMessage()) {
            String msg = _tokenizer.nextMessage();
            _handler._data.set_incomingMessages(_handler._data.get_incomingMessages() + 1);
            long rl = System.currentTimeMillis();
            String response = _protocol.processMessage(msg, _handler);
            rl = System.currentTimeMillis() - rl;
            _handler._data.set_responseLatency(_handler._data.get_responseLatency() + rl);
         
            if (response != null) {
                _handler.send(response);
            }
        }
    }

    /**
     * @param b the buffer
     */
    public void addBytes(ByteBuffer b) {
        // we synchronize on _buffers and not on "this" because
        // run() is synchronized on "this", and it might take a long time
        // to run.
        synchronized (_buffers) {
            _buffers.add(b);
        }
    }
}
