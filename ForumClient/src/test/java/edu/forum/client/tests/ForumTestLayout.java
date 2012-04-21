/**
 * 
 */
package edu.forum.client.tests;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

/**
 * @author Dagan
 * 
 */
public class ForumTestLayout {

	String server = "localhost";
	ProcessBuilder pb;
	ProcessBuilder clientPB;
	Process client;
	Process serverProc;

	/**
	 * Initiates the client. Server is expected to be running.
	 * 
	 * @throws java.lang.Exception
	 * 
	 */
	@Before
	public void setUp() throws Exception {

		pb = new ProcessBuilder();
		File workingDir = new File(System.getProperty("user.dir")
				+ "\\..\\testSupport");
		pb.directory(workingDir);
		pb.command("cmd.exe", "/C", "start-server.bat");
		try {
			pb.start();
		} catch (IOException e) {
			e.printStackTrace();
		}

		// for later automation?
		clientPB = new ProcessBuilder();
		File clientDir = new File(System.getProperty("user.dir")
				+ "\\..\\testSupport");
		clientPB.directory(clientDir);
		clientPB.command("cmd.exe", "/C",
		 "start java -jar ForumClient.jar");
//				"start-client.cmd");

		try {
			client = clientPB.start();
		} catch (IOException e) {
			e.printStackTrace();
		}

	}

	/**
	 * @throws java.lang.Exception
	 */
	@After
	public void tearDown() throws Exception {
		client.destroy();
		serverProc.destroy();
	}

	@Test
	public final void test() {
		OutputStream os = client.getOutputStream();
		PrintWriter pw = new PrintWriter(new BufferedWriter(
				new OutputStreamWriter(os)));

		final InputStream is = client.getInputStream();

		new Thread(new Runnable() {
			public void run() {
				try {
					BufferedReader br = new BufferedReader(
							new InputStreamReader(is));
					String line = null;
					while ((line = br.readLine()) != null) {
						System.out.println(line);
					}
				} catch (java.io.IOException e) {
				}
			}
		}).start();

		pw.write(server + "\r");
		pw.write("register dagan dagan\r");
		pw.write("login dagan dagan\r");
	}

}
