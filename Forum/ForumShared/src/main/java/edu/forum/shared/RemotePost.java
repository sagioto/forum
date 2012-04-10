
package edu.forum.shared;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;

public interface RemotePost extends Remote{

	public String getTitle() throws RemoteException;

	public void setTitle(String title) throws RemoteException;

	public String getBody() throws RemoteException;

	public void setBody(String body) throws RemoteException;
	
	public Map<Timestamp, RemotePost> getReplies() throws RemoteException;
	
	public void addReply(RemotePost reply) throws RemoteException;
	
	public RemoteUser getUser() throws RemoteException;
	
	public Timestamp getTime() throws RemoteException;
	
	public boolean equals(Object obj);

}
