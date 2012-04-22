package edu.forum.shared.network.protocol;

public interface ServerProtocolFactory {
    /**
     * @return AsyncServerProtocol
     */
    AsyncServerProtocol create();
}
