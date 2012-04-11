package edu.forum.server.domain;

import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

import org.apache.log4j.Logger;

import edu.forum.server.data.DataUtils;
import edu.forum.server.network.NetworkUtils;
import edu.forum.server.security.SecurityUtils;
import edu.forum.shared.AdminExistException;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;
import edu.forum.shared.RemotePost;
import edu.forum.shared.RemoteUser;
import edu.forum.shared.User;


public class Controller implements RemoteController {
	static Logger log = Logger.getLogger(Controller.class.getName());
	private RemoteUser admin;
	private ConcurrentMap<String, RemoteUser> users = new ConcurrentHashMap<String, RemoteUser>();
	private ConcurrentMap<String, RemotePost> posts = new ConcurrentHashMap<String, RemotePost>();

	public boolean enter() throws RemoteException {
		log.info("received request to enter");
		return true;
	}

	public boolean register(User toRegister) throws RemoteException {
		RemoteUser prev = users.putIfAbsent(toRegister.getUsername(), toRegister);
		return prev != null;
	}

	public boolean login(User toLogin) throws RemoteException {
		return (this.getUsers().get(toLogin.getUsername()) != null)
			&& (SecurityUtils.login(this, toLogin.getUsername(), toLogin.getPassword()));
	}

	public boolean logout(User toLogout) throws RemoteException {
		if(!SecurityUtils.isLoggedIn(this, toLogout)){
			SecurityUtils.logout(this, toLogout);
			return true;
		}
		else return false;
	}


	public Map<Timestamp, RemotePost> view(Post toView) throws RemoteException {
		return toView.getReplies();

	}

	public boolean post(Post current, Post toPost) throws RemoteException {
		if(SecurityUtils.isAuthorizedToPost(this, current, toPost.getUser())){
			DataUtils.post(current, toPost);
			return true;
		}
		return false;
	}

	public static void main(String...args){
		if(args.length < 2){
			log.fatal("usage: java -jar ForumServer.jar <admin-name> <admin-password>");
			System.exit(-1);
		}

		log.info("starting initialization sequence...");
		log.info("trying to bind service...");

		Controller controller = new Controller();
		try{
			NetworkUtils.bind(controller);
			log.info("Controller bound");
		} catch (Exception e) {
			log.error("Forum Server couldn't rebind:", e);
		}
		createAdmin(controller, args);
		log.info("populating data");
		try {
			DataUtils.populateData(controller);
		} catch (RemoteException e) {
			log.error("error while populating data");
			e.printStackTrace();
		}
		log.info("server ready to accept connections...");

	}


	private static void createAdmin(Controller controller, String... args) {
		try {
			log.info("creating admin user with username: " + args[0] + " and password: ******");
			controller.setAdmin(User.createAdmin(args[0], args[1]));
		} catch (AdminExistException e) {
			log.fatal("admin already exists", e);
			System.exit(-1);
		}
	}

	public RemoteUser getAdmin() {
		return this.admin;
	}

	public void setAdmin(RemoteUser admin) {
		this.admin = admin;
	}

	public ConcurrentMap<String, RemoteUser> getUsers() {
		return users;
	}

	public void setUsers(ConcurrentMap<String, RemoteUser> users) {
		this.users = users;
	}

	public ConcurrentMap<String, RemotePost> getPosts() {
		return posts;
	}

	public void setPosts(ConcurrentMap<String, RemotePost> posts) {
		this.posts = posts;
	}


}