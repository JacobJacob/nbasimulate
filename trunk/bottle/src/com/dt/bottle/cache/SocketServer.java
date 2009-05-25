package com.dt.bottle.cache;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;

public class SocketServer extends Thread {

	private ServerSocket socket;
	private Socket client;
	private SocketCache cache;

	public SocketServer() {
		try {
			socket = new ServerSocket(1300);
			cache = new SocketCache();
		} catch (Exception e) {

		}
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			try {
				client = socket.accept();
			} catch (Exception e) {
				e.printStackTrace();
			}
			new ClientRequestService(client).run();
			client = null;
		}
	}

	class ClientRequestService extends Thread {

		private Socket socket;
		private InputStream in;
		private OutputStream out;
		private boolean exist;

		private ClientRequestService(Socket socket) {
			this.socket = socket;
		}

		@Override
		public void run() {
			// TODO Auto-generated method stub
			byte[] buf = new byte[1024];
			while (!exist) {
				try {
					in = socket.getInputStream();

				} catch (Exception e) {
					e.printStackTrace();
					exist = true;
				}
			}
		}

	}

}
