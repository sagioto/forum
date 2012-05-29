package edu.forum.shared;

import java.io.Serializable;
import java.util.Set;
import java.util.concurrent.ConcurrentSkipListSet;


public class User implements Serializable {
	private static final long serialVersionUID = 3696402751827425419L;
	private String username;
	private String password;
	private Post currentPost;
	private Set<Post> posts;
	private Set<String> friends;
	private static boolean isAdmin = false;
	private boolean loggedIn = false;
	private AuthorizationLevel level = AuthorizationLevel.GUEST;
	
	public User(){
		super();
		posts = new ConcurrentSkipListSet<Post>();
		friends = new ConcurrentSkipListSet<String>();
	}
	
	public User(String username, String password){
		this();
		assertNotGuestUsername(username);
		this.username = username;
		this.password = password;
	}
	
	public void assertNotGuestUsername(String toCheck) throws IllegalArgumentException{
		if(toCheck.equals(Constants.GUEST_USER_NAME));
	}
	
	private User(String username, String password, boolean isAdmin){
		this(username, password);
		User.isAdmin = isAdmin;
		level = AuthorizationLevel.ADMIN;
	}
	
	public static User createAdmin(String username, String password) throws AdminExistException{
		if(isAdmin)
			throw new AdminExistException("admin user already exists");
		return new User(username, password, true);
	}
	
	public String getUsername() {
		return username;
	}

	public void setUsername(String toSet) {
		assertNotGuestUsername(toSet);
		this.username = toSet;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String toSet) {
		this.password = toSet;
	}

	public Set<Post> getPosts(){
		return posts;
	}

	public Set<String> getFriends(){
		return friends;
	}

	public Post getCurrentPost(){
		return currentPost;
	}

	public void setCurrentPost(Post currentPost){
		this.currentPost = currentPost;
	}

	public void addPost(Post toAdd) {
		posts.add(toAdd);
		
	}

	public void addFriend(String toAdd) {
		friends.add(toAdd);
	}

	public AuthorizationLevel getLevel() {
		return level;
	}

	public void setLevel(AuthorizationLevel level) {
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

	public boolean isLoggedIn() {
		return this.loggedIn;
	}
	
	public void setLoggedIn(boolean toSet) {
		this.loggedIn = toSet;
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
