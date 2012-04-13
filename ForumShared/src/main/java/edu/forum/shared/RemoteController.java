package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

public interface RemoteController extends Remote {

	public boolean enter() throws RemoteException;

	public boolean register(RemoteUser toRegister) throws RemoteException;
	
	public boolean login(RemoteUser toLogin) throws RemoteException;
	
	public boolean logout(RemoteUser toLogout) throws RemoteException;
	
	public Map<Timestamp, RemotePost> view(Post toView) throws RemoteException;
	
	public boolean post(Post current, Post toPost) throws RemoteException;
}
