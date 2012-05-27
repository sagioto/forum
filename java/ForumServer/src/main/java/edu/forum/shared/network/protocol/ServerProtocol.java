package edu.forum.shared.network.protocol;

import edu.forum.server.network.reactor.Sender;

/**
 * A protocol that describes the behaviour of the server.
 */
public interface ServerProtocol {

    /**
     * processes a message
     * @param msg the message to process
     * @param sender the client that sends this message if one wants to keep track of it
     *               to send messages asynchronously
     * @return the reply that should be sent to the client, or null if no reply needed
     */
    String processMessage(String msg, Sender sender);

    /**
     * detetmine whether the given message is the termination message
     * @param msg the message to examine
     * @return true if the message is the termination message, false otherwise
     */
    boolean isEnd(String msg);

}
