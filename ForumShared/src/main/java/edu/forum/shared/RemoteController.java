package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

public interface RemoteController extends Remote {

	public boolean enter() throws RemoteException;

	public boolean register(User toRegister) throws RemoteException;
	
	public boolean login(User toLogin) throws RemoteException;
	
	public boolean logout(User toLogout) throws RemoteException;
	
	public Map<Timestamp, RemotePost> view(Post toView) throws RemoteException;
	
	public boolean post(Post current, Post toPost) throws RemoteException;
}
