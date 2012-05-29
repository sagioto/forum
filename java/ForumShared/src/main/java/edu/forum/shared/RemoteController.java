package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

public interface RemoteController extends Remote {

	public Post enter() throws RemoteException;

	public boolean register(String username, String password) throws RemoteException;
	
	public boolean login(String username, String password) throws RemoteException;
	
	public boolean logout(String username, String password) throws RemoteException;
	
	public Map<Timestamp, Post> view(Post toView) throws RemoteException;
	
	public boolean post(Post current, Post toPost) throws RemoteException;
	
	public Post registerForPost() throws RemoteException;
}
