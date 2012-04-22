package edu.forum.server.network.tokenizer;

import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.nio.charset.CharsetEncoder;
import java.nio.charset.CharacterCodingException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;

public class FixedSeparatorMessageTokenizer implements StringMessageTokenizer {
    private final String _messageSeparator;
    private final StringBuffer _stringBuf = new StringBuffer();
    private final CharsetDecoder _decoder;
    private final CharsetEncoder _encoder;

    /**
     * @param separator the String
     * @param charset the Charset
     */
    public FixedSeparatorMessageTokenizer(String separator, Charset charset) {
        this._messageSeparator = separator;
        this._decoder = charset.newDecoder();
        this._encoder = charset.newEncoder();
    }

    /**
     * Add some bytes to the message.
     * Bytes are converted to chars, and appended to the internal StringBuffer.
     * Complete messages can be retrieved using the nextMessage() method.
     *
     * @param bytes an array of bytes to be appended to the message.
     */
    public synchronized void addBytes(ByteBuffer bytes) {
        CharBuffer chars = CharBuffer.allocate(bytes.remaining());
        // Last param false: more bytes may follow. Any unused bytes are kept in the decoder.
        this._decoder.decode(bytes, chars, false);
        chars.flip();
        this._stringBuf.append(chars);
    }

    /**
     * Is there a complete message ready?
     *
     * @return true the next call to nextMessage() will not return null, false otherwise.
     */
    public synchronized boolean hasMessage() {
        return this._stringBuf.indexOf(this._messageSeparator) > -1;
    }

    /**
     * Get the next complete message if it exists, advancing the tokenizer to the next message.
     * @return the next complete message, and null if no complete message exist.
     */
    public synchronized String nextMessage() {
        String message = null;
        int messageEnd = this._stringBuf.indexOf(this._messageSeparator);
        if (messageEnd > -1) {
            message = this._stringBuf.substring(0, messageEnd);
            this._stringBuf.delete(0, messageEnd+this._messageSeparator.length());
        }
        return message;
    }

    /**
     * Convert the String message into bytes representation, taking care of encoding and framing.
     * 
     * @param msg the message
     * @throws CharacterCodingException if there is a problem
     * 
     * @return a ByteBuffer with the message content converted to bytes,
     *         after framing information has been added.
     */
    public ByteBuffer getBytesForMessage(String msg)  throws CharacterCodingException {
        StringBuilder sb = new StringBuilder(msg);
        sb.append(this._messageSeparator);
        ByteBuffer bb = this._encoder.encode(CharBuffer.wrap(sb));
        return bb;
    }

}
