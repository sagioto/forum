package edu.forum.server.domain;

import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.TimeUnit;

import org.apache.log4j.Logger;

import edu.forum.server.data.DataManager;
import edu.forum.server.network.NetworkManager;
import edu.forum.server.security.SecurityManager;
import edu.forum.shared.AdminExistException;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;
import edu.forum.shared.User;


public class Controller implements RemoteController {
	static Logger log = Logger.getLogger(Controller.class.getName());
	private User admin;
	private ConcurrentMap<String, User> users = new ConcurrentHashMap<String, User>();
	private ConcurrentMap<String, Post> posts = new ConcurrentHashMap<String, Post>();
	private Post latestPost = DataManager.getMainPost();

	public Post enter() throws RemoteException {
		log.info("received request to enter");	
		return DataManager.getMainPost();
	}

	public boolean register(String username, String password) throws RemoteException {
		return SecurityManager.register(this, username, password);
	}

	public boolean login(String username, String password) throws RemoteException {
		return (this.getUsers().get(username) != null)
				&& (SecurityManager.login(this, username, password));
	}

	public boolean logout(String username, String password) throws RemoteException {
		if(this.getUsers().get(username) != null
				&& SecurityManager.isLoggedIn(this, getUsers().get(username))
				&& getUsers().get(username).getPassword().equals(password)){
			SecurityManager.logout(this, username);
			return true;
		}
		else return false;
	}


	public Map<Timestamp, Post> view(Post toView) throws RemoteException {
		return posts.get(toView.getTitle()).getReplies(); 

	}

	public boolean post(Post current, Post toPost) throws RemoteException {
		if(SecurityManager.isAuthorizedToPost(this, current, toPost.getUsername())){
			DataManager.post(this, posts.get(current.getTitle()), toPost);
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
		Controller controller = new Controller();
		try{
			NetworkManager.bind(controller);
			log.info("Controller bound");
		} catch (Exception e) {
			log.fatal("Forum Server couldn't rebind:", e);
			System.exit(-1);
		}
		createAdmin(controller, args);
		log.info("populating data");
		try {
			DataManager.populateData(controller);
		} catch (RemoteException | InterruptedException e) {
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

	public User getAdmin() {
		return this.admin;
	}

	public void setAdmin(User admin) {
		this.admin = admin;
	}

	public ConcurrentMap<String, User> getUsers() {
		return users;
	}

	public void setUsers(ConcurrentMap<String, User> users) {
		this.users = users;
	}

	public ConcurrentMap<String, Post> getPosts() {
		return posts;
	}

	public void setPosts(ConcurrentMap<String, Post> posts) {
		this.posts = posts;
	}

	public Post getLatestPost() {
		return latestPost;
	}

	//TODO: maybe add synchronized on this method
	public void setLatestPost(Post latestPost) {
		this.latestPost = latestPost;
	}

	@Override
	public synchronized Post registerForPost() throws RemoteException {
			try {
				long start = System.currentTimeMillis();
				wait(TimeUnit.MINUTES.toMillis(1));
				if (System.currentTimeMillis() - start < TimeUnit.MINUTES.toMillis(1)) {
					return getLatestPost();					
				}
			} catch (InterruptedException e) {}
			return null;
		}


}