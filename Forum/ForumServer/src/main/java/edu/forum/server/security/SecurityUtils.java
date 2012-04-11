package edu.forum.server.security;

import java.rmi.RemoteException;

import edu.forum.server.domain.Controller;
import edu.forum.shared.AuthorizationLevel;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteUser;
import edu.forum.shared.User;

public class SecurityUtils {

	public static boolean login(Controller controller, String username, String password) throws RemoteException{
		if(controller.getUsers().get(username).getPassword().equals(password)){
			controller.getUsers().get(username).setLevel(AuthorizationLevel.MEMBER);
			return true;
		}
		return false;
	}
	
	public static void logout(Controller controller, User toLogout) throws RemoteException {
		controller.getUsers().get(toLogout.getUsername()).setLevel(AuthorizationLevel.GUEST);
	}

	public static boolean isLoggedIn(Controller controller, User toLogout) throws RemoteException {
		return controller.getUsers().get(toLogout.getUsername()).getLevel() == AuthorizationLevel.GUEST;
	}

	public static boolean isAuthorizedToPost(Controller controller, Post current, RemoteUser remoteUser) throws RemoteException {
		return controller.getUsers().get(remoteUser.getUsername()).getLevel() != AuthorizationLevel.GUEST;
	}
}
