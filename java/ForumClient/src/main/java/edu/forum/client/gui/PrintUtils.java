package edu.forum.client.gui;

import java.sql.Timestamp;
import java.util.Map.Entry;

import edu.forum.shared.Post;

public class PrintUtils {
	public static final String ANSI_RESET = "\u001B;0m";
	public static final String ANSI_BLACK = "\u001B;30m";
	public static final String ANSI_RED = "\u001B31;1m";
	public static final String ANSI_GREEN = "\u001B;32m";
	public static final String ANSI_YELLOW = "\u001B;33m";
	public static final String ANSI_BLUE = "\u001B;34m";
	public static final String ANSI_PURPLE = "\u001B;35m";
	public static final String ANSI_CYAN = "\u001B;36m";
	public static final String ANSI_WHITE = "\u001B;37m";
	
	
	public static void printTitle() {
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

	public static void printRed(String toPrint) {
		System.out.print("\u001B31;1m" + toPrint);
	}

	public static void printGreen(String toPrint) {
		System.out.print("\u001B32;1m" + toPrint);
	}

	public static void printAvailableommands() {
		System.out.println();
		System.out.println("================================================");
	    System.out.println("|              FORUM`S MAIN MENU               |");
	    System.out.println("|                VERSION 1.0                   |");
	    System.out.println("================================================");
	    System.out.println("| Select Command:                              |");
	    System.out.println("|      - register <user-name> <password>       |");
	    System.out.println("|      - login <user-name> <password>          |");
	    System.out.println("|      - logout                                |");
	    System.out.println("|      - enter <post title>                    |");
	    System.out.println("|      - view                                  |");
	    System.out.println("|      - post                                  |");
	    System.out.println("|      - quit                                  |");
	    System.out.println("================================================");
	    System.out.println();
	}

	public static void printPost(Post postToPrint, int tabsLevel){
		//System.out.printf("| %-15s | %-20s | %-30s |", "12.04.12", "sagi", "this is the title" );
		String tabs = "\n";
		for (int i=0; i<tabsLevel; i++){
			tabs = tabs + "\t";
		}
		if(!postToPrint.isSubForum()){
			System.out.print(tabs+"|=========================================================================================================|");
			String title = postToPrint.getTitle();
			
			if (title.length()>55){
				System.out.printf(tabs+"| %-25s | %-15s | %-57s |", postToPrint.getTime(), postToPrint.getUsername(), title.substring(0, 55) );
				for (int i=55;i<title.length();i+=55) {
					if (title.substring(i).length()>55){
						System.out.printf(tabs+"| %-43s | %-57s |","", title.substring(i, i+55) );
					}
					else{
						System.out.printf(tabs+"| %-43s | %-57s |","", title.substring(i));
					}
				}
			}
			else{
				System.out.printf(tabs+"| %-25s | %-15s | %-57s |", postToPrint.getTime(), postToPrint.getUsername(), title );
			}
			System.out.print(tabs+"|=========================================================================================================|");
			System.out.print(tabs+"|                                                                                                         |");
			System.out.print(tabs+"|                                                 BODY                                                    |");
			System.out.print(tabs+"|=========================================================================================================|");
			System.out.print(tabs+"|                                                                                                         |");
			
			String body = postToPrint.getBody();

			// Breaking lines of the body to lines length 90 chars
			for (int i=0;i<body.length();i+=90) {
				if (body.substring(i).length()>90){
					System.out.printf(tabs + "|  %-101s  |", body.substring(i,i+90));
				}
				else{
					System.out.printf(tabs + "|  %-101s  |", body.substring(i));
				}
			}
			System.out.print(tabs+"|                                                                                                         |");
			System.out.print(tabs+"|=========================================================================================================|\n");
			for (Entry<Timestamp, Post> entry : postToPrint.getReplies().entrySet())
			{
				printPost(entry.getValue(), tabsLevel++);			
			}
		}
		else{
			System.out.println("|---------------------------------------------------------------------------------------------------------|");
			System.out.printf("| \t\t\t\t\t\t %-56s |",  postToPrint.getTitle() );
			System.out.println();
		}
	}
}
