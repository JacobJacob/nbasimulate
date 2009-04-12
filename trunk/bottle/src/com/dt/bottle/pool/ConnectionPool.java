package com.dt.bottle.pool;

import java.util.ArrayList;

import com.dt.bottle.connector.DBConnection;

public class ConnectionPool {

	public final static int MAX_CONNECTION = 5;

	private static ConnectionPool connPool;

	private ArrayList<DBConnection> pools;

	private int getTimes = 0;
	private int putTimes = 0;

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
		System.out.println("a connection out !..............");
		conn = pools.remove(pools.size() - 1);
		if (conn == null) {
			System.out.println("conn is null.............");
		}
		getTimes++;
		System.out.println("has get times " + getTimes);
		return conn;
	}

	public synchronized void disConnection(DBConnection conn) {

		if (conn == null) {
			System.out.println("add a null conn!");
		} else {
			conn.destory();
		}
		putTimes++;
		System.out.println("has put times " + putTimes);

		System.out.println("a connection in !..............");
		pools.add(conn);
	}

}
