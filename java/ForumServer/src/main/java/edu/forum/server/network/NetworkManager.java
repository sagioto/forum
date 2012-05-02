package edu.forum.server.network;

import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;

import org.apache.log4j.Logger;

import edu.forum.shared.Constants;
import edu.forum.shared.RemoteController;

public class NetworkManager {
	static Logger log = Logger.getLogger(NetworkManager.class.getName());
	
	public static void bind(RemoteController controller) throws RemoteException {
			log.info("trying to bind service...");
			System.setProperty("java.rmi.server.codebase",Constants.CODE_BASE);
			RemoteController stub =
                (RemoteController) UnicastRemoteObject.exportObject(controller, 0);
            Registry registry = LocateRegistry.getRegistry();
            registry.rebind(Constants.CONTROLLER_SERVICE_NAME, stub);
	}
	
}
