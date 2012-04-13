package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.Set;

public interface RemoteUser extends Remote {

	public String getUsername() throws RemoteException;
	
	public void setUsername(String toSet) throws RemoteException;

	public boolean isLoggedIn() throws RemoteException;
	
	public void setLoggedIn(boolean toSet) throws RemoteException;
	
	public String getPassword() throws RemoteException;
	
	public void setPassword(String toSet) throws RemoteException;
	
	public Set<RemotePost> getPosts() throws RemoteException;

	public void addPost(RemotePost toAdd) throws RemoteException;

	public Set<RemoteUser> getFriends() throws RemoteException;
	
	public void addFriend(RemoteUser toAdd) throws RemoteException;
	
	public RemotePost getCurrentPost() throws RemoteException;
	
	public void setCurrentPost(RemotePost toSet) throws RemoteException;
	
	public AuthorizationLevel getLevel() throws RemoteException;

	public void setLevel(AuthorizationLevel level) throws RemoteException;
	
	public boolean equals(Object obj);
	
}
