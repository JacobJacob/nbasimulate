package com.dt.bottle.pool;

import java.util.ArrayList;

import com.dt.bottle.connector.DBConnection;

public class ConnectionPool {

	public final static int MAX_CONNECTION = 5;

	private static ConnectionPool connPool;

	private ArrayList<DBConnection> pools;

	private ConnectionPool() {
		pools = new ArrayList<DBConnection>();
	}

	public static ConnectionPool instance() {
		if (connPool == null) {
			connPool = new ConnectionPool();
			connPool.init();
		}
		return connPool;
	}

	public void init() {

		for (int i = 0; i < MAX_CONNECTION; i++) {
			DBConnection conn = new DBConnection();
			conn.connect();
			pools.add(conn);
		}

	}

	public synchronized DBConnection connection() {

		DBConnection conn = null;
		while (pools.size() <= 0) {

			try {
				System.out.println("waiting...............");
				wait(10000);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		conn = pools.remove(pools.size() - 1);
		return conn;
	}

	public synchronized void disConnection(DBConnection conn) {

		pools.add(conn);
	}

}
