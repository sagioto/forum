package edu.forum.client.gui;

import java.sql.Timestamp;
import java.util.Map.Entry;

import edu.forum.shared.Post;

public class PrintUtils {

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
		System.out.println("you can now give the following commands: ");
		System.out.println("\tregister <user-name> <password>\n" +
				"\tlogin <user-name> <password>\n" +
				"\tlogout\n" +
				"\tenter <title>\n" +
				"\tview\n" +
				"\tpost\n" +
				"\tquit");
	}

	public static void printPost(Post postToPrint){
		if(!postToPrint.isSubForum()){
			System.out.println(postToPrint.getTime() + " : " + "\t"+ postToPrint.getTitle() + "\tBy: " + postToPrint.getUsername() + "\n\t\t" + postToPrint.getBody());
			System.out.print("\n\t");
			for (Entry<Timestamp, Post> entry : postToPrint.getReplies().entrySet())
			{
				printPost(entry.getValue());			
			}
		}
		else{
			System.out.println("\t" + postToPrint.getTitle());
		}
	}
}
