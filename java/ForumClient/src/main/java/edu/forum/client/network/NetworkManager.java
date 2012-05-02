package edu.forum.client.network;

import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

import edu.forum.shared.Constants;
import edu.forum.shared.RemoteController;

public class NetworkManager {

	public static RemoteController lookupServer(String serverIP) throws RemoteException, NotBoundException {
			Registry registry = LocateRegistry.getRegistry(serverIP);
			RemoteController controller = (RemoteController) registry.lookup(Constants.CONTROLLER_SERVICE_NAME);
			return controller;
	}

}