package edu.forum.server.network.tokenizer;

/**
 * @author spl staff
 *
 */
public interface TokenizerFactory {
    /**
     * @return a tokenizer
     */
    StringMessageTokenizer create();
}
