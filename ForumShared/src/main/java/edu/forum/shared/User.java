package edu.forum.shared;

import java.io.Serializable;
import java.rmi.RemoteException;
import java.util.Set;
import java.util.concurrent.ConcurrentSkipListSet;


public class User implements RemoteUser, Serializable {
	private static final long serialVersionUID = 3696402751827425419L;
	private String username;
	private String password;
	private RemotePost currentPost;
	private Set<RemotePost> posts;
	private Set<RemoteUser> friends;
	private static boolean isAdmin = false;
	private AuthorizationLevel level = AuthorizationLevel.GUEST;
	
	public User(){
		super();
		posts = new ConcurrentSkipListSet<RemotePost>();
		friends = new ConcurrentSkipListSet<RemoteUser>();
	}
	
	public User(String username, String password){
		this();
		this.username = username;
		this.password = password;
	}
	
	private User(String username, String password, boolean isAdmin){
		this(username, password);
		User.isAdmin = isAdmin;
		level = AuthorizationLevel.ADMIN;
	}
	
	public static RemoteUser createAdmin(String username, String password) throws AdminExistException{
		if(isAdmin)
			throw new AdminExistException("admin user already exists");
		return new User(username, password, true);
	}
	
	public String getUsername() throws RemoteException {
		return username;
	}

	public void setUsername(String toSet) throws RemoteException {
		this.username = toSet;
	}

	public String getPassword() throws RemoteException {
		return password;
	}

	public void setPassword(String toSet) throws RemoteException {
		this.password = toSet;
	}

	public Set<RemotePost> getPosts() throws RemoteException{
		return posts;
	}

	public Set<RemoteUser> getFriends() throws RemoteException{
		return friends;
	}

	public RemotePost getCurrentPost() throws RemoteException{
		return currentPost;
	}

	public void setCurrentPost(RemotePost currentPost) throws RemoteException{
		this.currentPost = currentPost;
	}

	public void addPost(RemotePost toAdd) throws RemoteException {
		posts.add(toAdd);
		
	}

	public void addFriend(RemoteUser toAdd) throws RemoteException {
		friends.add(toAdd);
	}

	public AuthorizationLevel getLevel() throws RemoteException {
		return level;
	}

	public void setLevel(AuthorizationLevel level) throws RemoteException {
		this.level = level;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result
				+ ((username == null) ? 0 : username.hashCode());
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
		User other = (User) obj;
		if (username == null) {
			if (other.username != null)
				return false;
		} else if (!username.equals(other.username))
			return false;
		return true;
	}
	
	
	
}
