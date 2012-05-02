package edu.forum.server.network.reactor;

import java.nio.channels.Selector;
import java.util.concurrent.ExecutorService;

import edu.forum.server.network.tokenizer.TokenizerFactory;
import edu.forum.shared.network.protocol.ServerProtocolFactory;

/**
 * a simple data stucture that hold information about the reactor, including getter methods
 */
public class ReactorData {

    private final ExecutorService _executor;
    private final Selector _selector;
    private final ServerProtocolFactory _protocolMaker;
    private final TokenizerFactory _tokenizerMaker;
    private int _incomingMessages;
    private long _responseLatency;
    private int _readFragmentationCounter = 1;
    /**
	 * @return the _readFragmentationCounter
	 */
	public synchronized int get_readFragmentationCounter() {
		return _readFragmentationCounter;
	}

	/**
	 * @param _readFragmentationCounter the _readFragmentationCounter to set
	 */
	public synchronized void set_readFragmentationCounter(
			int _readFragmentationCounter) {
		this._readFragmentationCounter = _readFragmentationCounter;
	}

	/**
	 * @return the _readCounter
	 */
	public synchronized int get_readCounter() {
		return _readCounter;
	}

	/**
	 * @param _readCounter the _readCounter to set
	 */
	public synchronized void set_readCounter(int _readCounter) {
		this._readCounter = _readCounter;
	}

	/**
	 * @return the _writeFragmentationCounter
	 */
	public synchronized int get_writeFragmentationCounter() {
		return _writeFragmentationCounter;
	}

	/**
	 * @param _writeFragmentationCounter the _writeFragmentationCounter to set
	 */
	public synchronized void set_writeFragmentationCounter(
			int _writeFragmentationCounter) {
		this._writeFragmentationCounter = _writeFragmentationCounter;
	}

	/**
	 * @return the _writeCounter
	 */
	public synchronized int get_writeCounter() {
		return _writeCounter;
	}

	/**
	 * @param _writeCounter the _writeCounter to set
	 */
	public synchronized void set_writeCounter(int _writeCounter) {
		this._writeCounter = _writeCounter;
	}

	private int _readCounter;
    private int _writeFragmentationCounter = 1;
    private int _writeCounter;
    
    /**
	 * @return the _incomingMessages
	 */
	public synchronized int get_incomingMessages() {
		return _incomingMessages;
	}

	/**
	 * @param _incomingMessages the _incomingMessages to set
	 */
	public synchronized void set_incomingMessages(int _incomingMessages) {
		this._incomingMessages = _incomingMessages;
	}

	/**
	 * @return the _responseLatency
	 */
	public synchronized long get_responseLatency() {
		return _responseLatency;
	}

	/**
	 * @param _responseLatency the _responseLatency to set
	 */
	public synchronized void set_responseLatency(long _responseLatency) {
		this._responseLatency = _responseLatency;
	}

	/**
     * @return _executor
     */
    public ExecutorService getExecutor() {
        return _executor;
    }

    /**
     * @return _selector
     */
    public Selector getSelector() {
        return _selector;
    }

    /**
     * @param executor the executor
     * @param selector the selector 
     * @param protocol the protocol 
     * @param tokenizer the tokenizer 
     */
    public ReactorData(ExecutorService executor, Selector selector, ServerProtocolFactory protocol, TokenizerFactory tokenizer) {
        this._executor = executor;
        this._selector = selector;
        this._protocolMaker = protocol;
        this._tokenizerMaker = tokenizer;
    }


	/**
     * @return _protocolMaker
     */
    /**
     * @return
     */
    public ServerProtocolFactory getProtocolMaker() {
        return _protocolMaker;
    }

    /**
     * @return _tokenizerMaker
     */
    /**
     * @return
     */
    public TokenizerFactory getTokenizerMaker() {
        return _tokenizerMaker;
    }



}
