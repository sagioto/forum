package edu.forum.shared;

import java.io.Serializable;
import java.sql.Timestamp;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;


public class Post implements Comparable<PostKey>, Serializable {
	private static final long serialVersionUID = 1503444426061687478L;
	private PostKey key;
	private Post parent;
	private String title;
	private String body;
	private ConcurrentMap<Timestamp, Post> replies = new ConcurrentHashMap<Timestamp, Post>();
	private String username;
	private Timestamp time;
	private boolean isSubForum = false;

	public Post(Post parent, String title) {
		super();
		this.title = title;
		this.parent = parent;
	}

	public Post(String title, String body, String string, Timestamp time, Post parent) {
		super();
		this.title = title;
		this.body = body;
		this.username = string;
		this.time = time;
		this.parent = parent;
		this.key = new PostKey(getUsername(), time);
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getBody() {
		return body;
	}

	public void setBody(String body) {
		this.body = body;
	}

	public Map<Timestamp, Post> getReplies() {
		return replies;
	}

	public void addReply(Post reply) {
		this.replies.put(reply.getTime(), reply);
		reply.setParent(this);
	}

	public String getUsername() {
		return username;
	}

	public Timestamp getTime() {
		return time;
	}

	public PostKey getKey() {
		return key;
	}

	public void setKey(PostKey key) {
		this.key = key;
	}

	public boolean isSubForum() {
		return isSubForum;
	}

	public Post getParent() {
		return parent;
	}

	public void setParent(Post parent) {
		this.parent = parent;
	}

	public void setSubForum(boolean isSubForum) {
		this.isSubForum = isSubForum;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((time == null) ? 0 : time.hashCode());
		result = prime * result + ((username == null) ? 0 : username.hashCode());
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
		if (username == null) {
			if (other.username != null)
				return false;
		} else if (!username.equals(other.username))
			return false;
		return true;
	}

	public int compareTo(PostKey o) {
		if(getKey().getUsername().compareTo(o.getUsername()) != 0)
			return getKey().getTime().compareTo(o.getTime());
		return getKey().getUsername().compareTo(o.getUsername());
	}

	@Override
	public String toString() {
		return "Post [key=" + key + ", title=" + title + ", body=" + body
				+ ", replies=" + replies.size() + ", username=" + username + ", time="
				+ time + ", isSubForum=" + isSubForum + "]";
	}



}
