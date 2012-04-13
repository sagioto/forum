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
		try {
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
						System.out.println("bye bye");
						System.exit(0);
					case "register":
						user = new User(cmdArray[1], cmdArray[2]);
						if (controller.register(user))
							System.out.println("registration success");
						else
							System.out.println("failed! user name " + user.getUsername() + " is already taken!");
						break;
					case "login":
						user = new User(cmdArray[1], cmdArray[2]);
						if (controller.login(user))
							System.out.println("welcome " + user.getUsername());
						else{
							System.out.println("failed! please check your username and password");
							user = defaultUser();
						}
						break;
					case "logout":
						if (controller.logout(user)){
							System.out.println("you are now logged out");
							user = defaultUser();
						}
						else
							System.out.println("failed! you weren't logged in");
						break;
					case "view":
						//TODO complete handling
					case "post":
						//TODO complete handling
					default:
						System.out.println("command " + cmdArray[0] + " is not supported");

					}

				}
			} catch (IOException | IllegalArgumentException e) {
				e.printStackTrace();
			}
		} catch (RemoteException e1) {
			System.out.println("server connection problem: " + e1.getMessage());
		}
	}

	private static User defaultUser() {
		User user;
		user = new User(Constants.GUEST_USER_NAME, "init");
		return user;
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
				System.out.print("\rException connecting to server, let's try again\n");
				continue;
			}
			System.out.println("\rconnected successfully to " + host);
			connected = true;
		}
		return in;
	}



}
