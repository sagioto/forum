package edu.forum.client.gui;

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
				"\tview <title>\n" +
				"\tpost <title> <message>\n" +
				"\tquit");
		
	}
}
