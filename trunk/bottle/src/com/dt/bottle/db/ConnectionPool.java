package com.dt.bottle.db;

import java.sql.Connection;

public class ConnectionPool {

	public static final String URL = "jdbc:mysql://192.168.1.158/gba?characterEncoding=utf-8";
	public static final String USERNAME = "gba";
	public static final String PASSWORD = "gba123";

	// public static final String URL =
	// "jdbc:mysql://192.168.1.158/gbadtgame?characterEncoding=utf-8";
	// public static final String USERNAME = "gbagameuser";
	// public static final String PASSWORD = "gba@g1a2m3euser";

	private static ConnectionPool pool = null;
	private snaq.db.ConnectionPool connPool = null;
	private boolean inited = false;

	private ConnectionPool() {
	}

	public static ConnectionPool getInstance() {
		if (pool == null) {
			pool = new ConnectionPool();
		}
		return pool;
	}

	private void init() {
		try {
			Class.forName("com.mysql.jdbc.Driver").newInstance();
			connPool = new snaq.db.ConnectionPool("local", 20, 30, 180000, URL, USERNAME, PASSWORD);
			inited = true;
		} catch (Exception e) {
			e.printStackTrace();
			inited = false;
		}
	}

	public Connection connection() {
		if (!inited) {
			init();
		}
		Connection conn = null;
		while (true) {
			try {
				conn = connPool.getConnection();
			} catch (Exception e) {
				e.printStackTrace();
			}
			if (conn == null) {
				try {
					wait(60);
				} catch (Exception e) {
					e.printStackTrace();
				}
			} else {
				return conn;
			}

		}
	}
}
