package edu.forum.server.tests;

import java.rmi.RemoteException;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

import junit.framework.Assert;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import edu.forum.server.domain.Controller;
import edu.forum.server.security.SecurityUtils;
import edu.forum.shared.AdminExistException;
import edu.forum.shared.User;

public class ServerSanityTest {

	Controller server;

	@Before
	public void setUp() throws Exception {
		Runtime.getRuntime().exec("cmd.exe start rmiregistry");
		server = new Controller();
	}

	@After
	public void tearDown() throws Exception {
	}

	@Test
	public final void test() {
		User admin = new User("admin", "admin");
		try {
			server.setAdmin(User.createAdmin("admin", "admin"));
			Assert.assertEquals("Checking admin is set correctly", admin,
					server.getAdmin());

			server.enter();

			ConcurrentMap<String, User> users = new ConcurrentHashMap<String, User>();
			for (int i = 0; i < 10; i++) {
				User user = new User("User" + i, "pass");
				users.put("User" + i, user);
				server.register(user);
			}

			Assert.assertEquals("Checking users were registered successfully",
					users, server.getUsers());

			for (User user : users.values()) {
				server.login(user);
			}

			for (User user : users.values()) {
				Assert.assertTrue("Checking users were logged in",
						SecurityUtils.isLoggedIn(server, user));
			}

			for (User user : users.values()) {
				server.logout(user);
			}

			for (User user : users.values()) {
				Assert.assertFalse("Checking users were logged out",
						SecurityUtils.isLoggedIn(server, user));
			}

			
			
		} catch (AdminExistException e) {
			Assert.fail();
			e.printStackTrace();
		} catch (RemoteException e) {
			Assert.fail();
			e.printStackTrace();
		}

	}
}
