package edu.forum.shared;

import java.io.Serializable;
import java.sql.Timestamp;

public class PostKey implements Serializable{
	private static final long serialVersionUID = -7943564716491302371L;
	private String username;
	private Timestamp time;
	
	public PostKey(String username, Timestamp time) {
		super();
		this.username = username;
		this.time = time;
	}
	
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public Timestamp getTime() {
		return time;
	}
	public void setTime(Timestamp time) {
		this.time = time;
	}
}
