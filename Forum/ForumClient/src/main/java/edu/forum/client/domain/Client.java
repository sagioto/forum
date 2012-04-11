package edu.forum.client.domain;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.sql.Timestamp;
import java.util.HashMap;
import java.util.Scanner;

import edu.forum.client.network.NetworkUtils;
import edu.forum.shared.Constants;
import edu.forum.shared.Post;
import edu.forum.shared.RemoteController;
import edu.forum.shared.User;

public class Client {
	private static RemoteController controller;

	public static void main(String...args){
		System.setProperty("java.rmi.server.codebase", Constants.CODE_BASE);
		printTitle();
		Scanner in = new Scanner(System.in);
		boolean connected = false;
		while(!connected){
			System.out.print("please enter server host address: ");
			String host = in.nextLine();

			System.out.print("trying to connect to " + host);
			try {
				controller = NetworkUtils.lookupServer(host);
			} catch (RemoteException | NotBoundException e) {
				System.err.println("\rException connecting to server, let's try again");
				continue;
			}
			System.out.println("\rconnected successfully to " + host);
			connected = true;
		}

		User user = new User("guest", "init");
		Post curreunt;
		try {
			curreunt = new Post("main","", user, new Timestamp(System.currentTimeMillis()));
			System.out.print("you can now give the following commands: ");
			HashMap <String, Method> methods = new HashMap<String, Method>();
			for(Method method : RemoteController.class.getDeclaredMethods()){
				methods.put(method.getName(), method);
			}
			System.out.println(methods.keySet());
			/*	HashMap <String, String> methodsHelp = new HashMap<String, String>();
			for(Method method : RemoteController.class.getDeclaredMethods()){
				methodsHelp.put(method.getName(), Arrays.deepToString(method.getParameterTypes()));
			}*/
			try {
				String command;
				while(true){
					System.out.print(user.getUsername() + "@" + curreunt.getTitle() + ">> ");
					command = in.nextLine();
					String[] commndsArry = command.split(" ");
					switch(commndsArry[0]){
						case "register":
							System.out.println(methods.get(commndsArry[0]).invoke(controller, new User(commndsArry[1], commndsArry[2])));
							break;
					}
						
//					System.out.println(methods.get(commndsArry[0]).invoke(controller, new User("sagi", "sagi")));
				}
			} catch (IOException | IllegalArgumentException | IllegalAccessException | InvocationTargetException e) {
				e.printStackTrace();
			}
		} catch (RemoteException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
	}

	private static void printTitle() {
		StringBuilder sb = new StringBuilder();
		sb.append("  $$\\     $$\\                        $$$$$$\\                                             \n");
		sb.append("  $$ |    $$ |                      $$  __$$\\                                            \n");
		sb.append("$$$$$$\\   $$$$$$$\\   $$$$$$\\        $$ /  \\__|$$$$$$\\   $$$$$$\\  $$\\   $$\\ $$$$$$\\$$$$\\  \n");
		sb.append("\\_$$  _|  $$  __$$\\ $$  __$$\\       $$$$\\    $$  __$$\\ $$  __$$\\ $$ |  $$ |$$  _$$  _$$\\ \n");
		sb.append("  $$ |    $$ |  $$ |$$$$$$$$ |      $$  _|   $$ /  $$ |$$ |  \\__|$$ |  $$ |$$ / $$ / $$ |\n");
		sb.append("  $$ |$$\\ $$ |  $$ |$$   ____|      $$ |     $$ |  $$ |$$ |      $$ |  $$ |$$ | $$ | $$ |\n");
		sb.append("  \\$$$$  |$$ |  $$ |\\$$$$$$$\\       $$ |     \\$$$$$$  |$$ |      \\$$$$$$  |$$ | $$ | $$ |\n");
		sb.append("   \\____/ \\__|  \\__| \\_______|      \\__|      \\______/ \\__|       \\______/ \\__| \\__| \\__|\n");
		System.out.println(sb.toString());
	}

}
