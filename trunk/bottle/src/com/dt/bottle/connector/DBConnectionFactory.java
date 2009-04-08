package com.dt.bottle.connector;

public class DBConnectionFactory {
	
	public static DBConnection CreateConnection() {
		DBConnection conn = null;
		try {
			conn = new DBConnection();
			conn.connect();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return conn;
	}
}
