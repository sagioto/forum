package edu.forum.client.gui;

import java.io.IOException;
import java.lang.reflect.Method;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Scanner;

import edu.forum.client.domain.ClientController;
import edu.forum.client.network.NetworkUtils;
import edu.forum.shared.Constants;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;

public class Client {
	private static ClientController clientController = new ClientController(); 

	public static void main(String...args){
		PrintUtils.printTitle();
		Scanner in = connectToServer();

		PrintUtils.printAvailableommands();
		HashMap <String, Method> methods = new HashMap<String, Method>();
		for(Method method : RemoteController.class.getDeclaredMethods()){
			methods.put(method.getName(), method);
		}
		try {
			clientController.setCurrentPost(clientController.getForumServer().enter());
			String command;
			while(true){
				System.out.print(clientController.getCurrentUser().getUsername() + "@" + clientController.getCurrentPost().getTitle() + "> ");
				command = in.nextLine();
				String[] cmdArray = command.split(" ");
				switch (cmdArray[0]){
				case "quit":
					if(clientController.getCurrentUser().isLoggedIn())
						try
					{
							clientController.logout(clientController.getCurrentUser());
					}
					catch (RemoteException e) {
						System.out.println("Coudlnt logout from server: " + e.getMessage());
					}
					System.out.println("bye bye");
					System.exit(0);
				case "register":
					if(cmdArray.length >= 3){
						clientController.setUserCredentials(clientController.getCurrentUser(), cmdArray[1], cmdArray[2]);
						try
						{
							if (clientController.register(clientController.getCurrentUser())){
								System.out.println("registration success");
								clientController.getCurrentUser().setLoggedIn(true);
							}
							else
								System.out.println("failed! user name " + clientController.getCurrentUser().getUsername() + " is already taken!");
						}
						catch (RemoteException e) {
							System.out.println("Coudlnt register to server: " + e.getMessage());
						}
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				case "login":
					if(cmdArray.length >= 3){
						clientController.setUserCredentials(clientController.getCurrentUser(), cmdArray[1], cmdArray[2]);
						try {
							if (clientController.login(clientController.getCurrentUser())){
								System.out.println("welcome " + clientController.getCurrentUser().getUsername());
								clientController.getCurrentUser().setLoggedIn(true);
							}
							else{
								System.out.println("failed! please check your username and password");
								clientController.setDefaultUser();
							}
						} 
						catch (RemoteException e) {
							System.out.println("Coudlnt login to server: " + e.getMessage());
						}

					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				case "logout":
					if (clientController.logout(clientController.getCurrentUser())){
						System.out.println("you are now logged out");
						clientController.setUserCredentials(clientController.getCurrentUser(), Constants.GUEST_USER_NAME, Constants.GUEST_PASSWORD);
					}
					else
						System.out.println("failed! you weren't logged in");
					break;
				case "enter":
					if(cmdArray.length >= 2){
						Collection<Post> values = clientController.getCurrentPost().getReplies().values();
						for (Post post : values) {
							if(post.getTitle().equals(cmdArray[1]))
								clientController.setCurrentPost(post);							
						}
					}
					else
						System.out.println(cmdArray[0] +"- incorrect number of argument");
					break;
				case "view":
					if(cmdArray.length >= 1){
						Map<Timestamp, Post> postsToView = clientController.view(clientController.getCurrentPost());
						if(postsToView != null){
							List<Timestamp> keyList = new ArrayList<Timestamp>(postsToView.keySet());
							Collections.sort(keyList);
							for (Timestamp timestamp : keyList)
							{
								PrintUtils.printPost(postsToView.get(timestamp));
							}
						}
						else System.out.println("this post has no replies to display");
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				case "post":
					if(cmdArray.length >= 1){
						System.out.print("Post title: ");
						String title = in.nextLine();
						System.out.print("Post Body: ");
						String body = in.nextLine();
						Post post = new Post(title, body, clientController.getCurrentUser().getUsername(), new Timestamp(System.currentTimeMillis()),
								clientController.getCurrentPost());
						try {
							if (!clientController.post(post))
							{
								System.out.println("Unauthorized post due to user permissions");
							}
						} catch (RemoteException e) {
							System.out.println("Coudlnt post your messege to server: " + e.getMessage());
						}

					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				default:
					System.out.println("command " + cmdArray[0] + " is not supported");

				}

			}
		} catch ( IOException | IllegalArgumentException e) {
			e.printStackTrace();
		}
	}



	private static Scanner connectToServer() {
		Scanner in = new Scanner(System.in);
		boolean connected = false;
		while(!connected){
			System.out.print("please enter server host address: ");
			String host = in.nextLine();

			System.out.print("trying to connect to " + host);
			try {
				//controller = NetworkUtils.lookupServer(host);
				clientController.setForumServer(NetworkUtils.lookupServer(host)); 
			} catch (Exception e) {
				System.out.print("\rcouldn't connect to server, let's try again\n");
				continue;
			}
			System.out.println("\rconnected successfully to " + host);
			connected = true;
		}
		return in;
	}



}
