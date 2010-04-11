package com.dt.bottle.pool;

import java.util.ArrayList;

import com.dt.bottle.connector.DBConnection;

public class ConnectionPool {

	public final static int MAX_CONNECTION = 50;

	private static ConnectionPool connPool;

	private ArrayList<DBConnection> pools;

	private Object lock = new Object();

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

	public DBConnection connection() {
       
		//System.out.println("start to get conn");
		DBConnection conn = null;
		synchronized (lock) {
			while (conn == null) {
				if (pools.size() > 0) {

					conn = pools.remove(pools.size() - 1);
					lock.notifyAll();

				} else {
					try {
						lock.wait();
					} catch (InterruptedException ie) {
						ie.printStackTrace();
					}
				}
			}
		}
		//System.out.println("success get conn");
		return conn;
	}

	public void disConnection(DBConnection conn) {

		synchronized (lock) {
			if (conn == null) {
				System.out.println("add a null conn!");
			} else {
				conn.destory();
			}
			pools.add(conn);
			lock.notifyAll();
		}

	}

}
