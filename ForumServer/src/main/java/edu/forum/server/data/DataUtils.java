package edu.forum.server.data;

import java.rmi.RemoteException;
import java.sql.Timestamp;

import edu.forum.server.domain.Controller;
import edu.forum.shared.Constants;
import edu.forum.shared.Post;
import edu.forum.shared.RemotePost;
import edu.forum.shared.RemoteUser;
import edu.forum.shared.User;



public class DataUtils {

	public static void populateData(Controller controller) throws RemoteException{
		for (int i = 0; i < Constants.INITIAL_NUM_OF_USERS; i++) {
			controller.getUsers().put("name-" + i, new User("name-" + i,"pass" + i));
		}
		Timestamp timestamp = new Timestamp(System.currentTimeMillis());
		for (int i = 0; i < Constants.INITIAL_SUB_FORUM_SIZE; i++) {
			controller.getPosts().put("sub-forum-" + i,
					new Post("sub-forum-" + i, "", controller.getAdmin(), timestamp));
		}
		for (RemoteUser user : controller.getUsers().values()) {
			for (int i = 0; i < Constants.INITIAL_SUB_FORUM_SIZE; i++) {
				post(controller.getPosts().get("sub-forum-" + i),
						new Post("title-" + i, "content", user, new Timestamp(System.currentTimeMillis())));
			}
		}

	}

	public static void post(RemotePost father, Post child) throws RemoteException {
		father.addReply(child);
		synchronized (father) {
			father.notifyAll();
		}
		synchronized (child.getUser()) {
			child.getUser().notifyAll();
		}
	}


}
