package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

public interface RemoteController extends Remote {

	public String enter() throws RemoteException;

	public String register(User toRegister) throws RemoteException;
	
	public String login(User toLogin) throws RemoteException;
	
	public String logout(User toLogout) throws RemoteException;
	
	public Map<Timestamp, RemotePost> view(Post toView) throws RemoteException;
	
	public String post(Post current, Post toPost) throws RemoteException;
}
