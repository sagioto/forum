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
import java.util.concurrent.TimeUnit;

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
							System.out.println("Coudln't logout from server: " + e.getMessage());
					}
					for (int i=0; i< 3; i++){
						System.out.print("By ");
						try {
							TimeUnit.MILLISECONDS.sleep(500);
						} catch (InterruptedException e) {						}
					}
					System.out.println("!");
					System.exit(0);
					
				case "register":
					if(cmdArray.length >= 3){
						clientController.setUserCredentials(clientController.getCurrentUser(), cmdArray[1], cmdArray[2]);
						try
						{
							if (clientController.register(clientController.getCurrentUser())){
								System.out.println("Registration success");
								clientController.getCurrentUser().setLoggedIn(true);
							}
							else
								System.out.println("Failed! username " + clientController.getCurrentUser().getUsername() + " is already taken!");
								clientController.setDefaultUser();
						}
						catch (RemoteException e) {
							System.out.println("Couldn't register to server: " + e.getMessage());
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
								System.out.println("Welcome " + clientController.getCurrentUser().getUsername());
								clientController.getCurrentUser().setLoggedIn(true);
							}
							else{
								System.out.println("Failed! please check your username and password");
								clientController.setDefaultUser();
							}
						} 
						catch (RemoteException e) {
							System.out.println("Couldn't login to server: " + e.getMessage());
						}
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
					
				case "logout":
					if (clientController.logout(clientController.getCurrentUser())){
						System.out.println("You are now logged out");
						clientController.setUserCredentials(clientController.getCurrentUser(), Constants.GUEST_USER_NAME, Constants.GUEST_PASSWORD);
					}
					else
						System.out.println("Failed! you weren't logged in");
					break;
					
				case "enter":
					if(cmdArray.length >= 2){
						if (cmdArray[1].equals("..")) 	// In case of returning to a parent forum
						{
							if (clientController.getCurrentPost().getParent() != null)
								clientController.setCurrentPost(clientController.getCurrentPost().getParent());
						}
						else{ 	// In case of entering to a deeper level
							Collection<Post> values = clientController.getCurrentPost().getReplies().values();
							for (Post post : values) {
								if(post.getTitle().equals(cmdArray[1]))
									clientController.setCurrentPost(post);							
							}
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
							if (clientController.getCurrentPost().getParent() == null){
								System.out.println("|---------------------------------------------------------------------------------------------------------|");
								System.out.printf("| \t\t\t\t\t       %-58s |",  "SUB FORUMS MENU" );
								System.out.println();
							}
							else{
								System.out.println();
							}
							
							for (Timestamp timestamp : keyList)
							{
								PrintUtils.printPost(postsToView.get(timestamp), 0);
							}
						}
						else System.out.println("This post has no replies to display");
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
					
				case "post":
					if(cmdArray.length >= 1){
						if (clientController.getCurrentPost().getParent() == null)
						{
							System.out.println("No posts at sub forums level. Please enter an inner level for writing posts");
						}
						else
						{
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
			System.out.println();
			System.out.print("Please Enter Server Host Address: ");
			String host = in.nextLine();

			System.out.print("Trying to connect to " + host);
			for (int i=0; i<8; i++)
			{
				try {
					TimeUnit.MILLISECONDS.sleep(500);
				} catch (InterruptedException e) {}
				System.out.print(".");
			}
			try {
				clientController.setForumServer(NetworkUtils.lookupServer(host));
				
			} catch (Exception e) {
				System.out.print("\rCouldn't connect to server, let's try again\n");
				continue;
			}
			System.out.println("\rConnected successfully to " + host);
			try {
				TimeUnit.MILLISECONDS.sleep(800);
			} catch (InterruptedException e) {}
			
			connected = true;
		}
		return in;
	}



}
