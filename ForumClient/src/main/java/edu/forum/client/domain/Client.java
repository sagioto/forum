package edu.forum.client.domain;

import java.io.IOException;
import java.lang.reflect.Method;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.HashMap;
import java.util.Scanner;

import edu.forum.client.gui.PrintUtils;
import edu.forum.client.network.NetworkUtils;
import edu.forum.shared.Constants;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;
import edu.forum.shared.User;

public class Client {
	private static RemoteController controller;

	public static void main(String...args){
		System.setProperty("java.rmi.server.codebase", Constants.CODE_BASE);
		PrintUtils.printTitle();
		Scanner in = connectToServer();

		User user = defaultUser();
		Post curreunt;
		curreunt = new Post("main","", user, new Timestamp(System.currentTimeMillis()));
		PrintUtils.printAvailableommands();

		HashMap <String, Method> methods = new HashMap<String, Method>();
		for(Method method : RemoteController.class.getDeclaredMethods()){
			methods.put(method.getName(), method);
		}
		try {
			String command;
			while(true){
				System.out.print(user.getUsername() + "@" + curreunt.getTitle() + "> ");
				command = in.nextLine();
				String[] cmdArray = command.split(" ");
				switch(cmdArray[0]){
				case "quit":
					if(user.isLoggedIn())
						controller.logout(user);
					System.out.println("bye bye");
					System.exit(0);
				case "register":
					if(cmdArray.length >= 3){
						setUserCredentials(user, cmdArray[1], cmdArray[2]);
						if (controller.register(user)){
							System.out.println("registration success");
							user.setLoggedIn(true);
						}
						else
							System.out.println("failed! user name " + user.getUsername() + " is already taken!");
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				case "login":
					if(cmdArray.length >= 3){
						setUserCredentials(user, cmdArray[1], cmdArray[2]);
						if (controller.login(user)){
							System.out.println("welcome " + user.getUsername());
							user.setLoggedIn(true);
						}
						else{
							System.out.println("failed! please check your username and password");
							user = defaultUser();
						}
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
					break;
				case "logout":
					if (controller.logout(user)){
						System.out.println("you are now logged out");
						setUserCredentials(user, Constants.GUEST_USER_NAME, Constants.GUEST_PASSWORD);
					}
					else
						System.out.println("failed! you weren't logged in");
					break;
				case "view":
					if(cmdArray.length >= 2){
						//TODO complete handling
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
				case "post":
					if(cmdArray.length >= 3){
						//TODO complete handling
					}
					else
						System.out.println( cmdArray[0] +"- incorrect number of argument");
				default:
					System.out.println("command " + cmdArray[0] + " is not supported");

				}

			}
		} catch (IOException | IllegalArgumentException e) {
			e.printStackTrace();
		}
	}

	private static void setUserCredentials(User user, String username, String password) {
		user.setUsername(username);
		user.setPassword(password);
	}

	private static User defaultUser() {
		return new User(Constants.GUEST_USER_NAME, Constants.GUEST_PASSWORD);
	}

	private static Scanner connectToServer() {
		Scanner in = new Scanner(System.in);
		boolean connected = false;
		while(!connected){
			System.out.print("please enter server host address: ");
			String host = in.nextLine();

			System.out.print("trying to connect to " + host);
			try {
				controller = NetworkUtils.lookupServer(host);
			} catch (RemoteException | NotBoundException e) {
				System.out.print("\rcouldn't connect to server, let's try again\n");
				continue;
			}
			System.out.println("\rconnected successfully to " + host);
			connected = true;
		}
		return in;
	}



}
