
package edu.forum.client.domain;

import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

import edu.forum.shared.Constants;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;
import edu.forum.shared.User;


public class ClientController {
	
	private static RemoteController forumServer;
	private User currentUser;
	private Post currentPost;
	/**
	 * 
	 */
	public ClientController() {
		// TODO Auto-generated constructor stub
		System.setProperty("java.rmi.server.codebase", Constants.CODE_BASE);
		setDefaultUser();
		currentPost = null;
	}
	
	public void setUserCredentials(User user, String username, String password) {
		user.setUsername(username);
		user.setPassword(password);
	}

	public void setDefaultUser() {
		currentUser = new User(Constants.GUEST_USER_NAME, Constants.GUEST_PASSWORD);
	}
	
	public void setForumServer(RemoteController forumServer) throws Exception  {
		try
		{
			ClientController.forumServer = forumServer;
		}
		catch (Exception e) {
			throw e;
		}
	}

	public RemoteController getForumServer() {
		return forumServer;
	}

	public void setCurrentUser(User currentUser) {
		this.currentUser = currentUser;
	}

	public User getCurrentUser() {
		return currentUser;
	}

	public void setCurrentPost(Post currentPost) {
		this.currentPost = currentPost;
	}

	public Post getCurrentPost() {
		return currentPost;
	}

	public boolean logout(User currentUser) throws RemoteException {
		return forumServer.logout(currentUser);
	}

	public boolean register(User currentUser) throws RemoteException {
		return forumServer.register(currentUser);
	}

	public boolean login(User currentUser) throws RemoteException {
		return forumServer.login(currentUser);
	}
	
	public Map<Timestamp, Post> view(Post toView) throws RemoteException
	{
		return forumServer.view(toView);
	}

	public boolean post(Post post) throws RemoteException {
		return forumServer.post(currentPost, post);
	}
	
	
	
}
