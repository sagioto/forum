package edu.forum.shared;

import java.io.Serializable;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;


public class Post implements RemotePost, Comparable<PostKey>, Serializable {
	private static final long serialVersionUID = 1503444426061687478L;
	private PostKey key;
	private String title;
	private String body;
	private ConcurrentMap<Timestamp, RemotePost> replies;
	private RemoteUser user;
	private Timestamp time;
	
	public Post(String title, String body, RemoteUser user, Timestamp time) throws RemoteException {
		super();
		this.title = title;
		this.body = body;
		this.user = user;
		this.time = time;
		replies = new ConcurrentHashMap<Timestamp, RemotePost>();
		this.key = new PostKey(user.getUsername(), time);
	}

	public String getTitle() throws RemoteException {
		return title;
	}

	public void setTitle(String title) throws RemoteException {
		this.title = title;
	}

	public String getBody() throws RemoteException {
		return body;
	}

	public void setBody(String body) throws RemoteException {
		this.body = body;
	}

	public Map<Timestamp, RemotePost> getReplies() throws RemoteException {
		return replies;
	}

	public void addReply(RemotePost reply) throws RemoteException {
		this.replies.put(reply.getTime(), reply);
	}

	public RemoteUser getUser() throws RemoteException {
		return user;
	}

	public Timestamp getTime() throws RemoteException {
		return time;
	}

	public PostKey getKey() {
		return key;
	}

	public void setKey(PostKey key) {
		this.key = key;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((time == null) ? 0 : time.hashCode());
		result = prime * result + ((user == null) ? 0 : user.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Post other = (Post) obj;
		if (time == null) {
			if (other.time != null)
				return false;
		} else if (!time.equals(other.time))
			return false;
		if (user == null) {
			if (other.user != null)
				return false;
		} else if (!user.equals(other.user))
			return false;
		return true;
	}

	public int compareTo(PostKey o) {
		if(getKey().getUsername().compareTo(o.getUsername()) != 0)
			return getKey().getTime().compareTo(o.getTime());
		return getKey().getUsername().compareTo(o.getUsername());
	}
	
	

}
