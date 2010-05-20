package com.ts.dt.util;

import jpersist.DatabaseManager;

public class DatabaseManagerUtil {

	public static final String URL = "jdbc:mysql://192.168.1.158/gba?characterEncoding=utf-8";
	public static final String USERNAME = "gba";
	public static final String PASSWORD = "gba123";
	public static final String DATABASE = "gba";
	public static final int POOLSIZE = 5;
	public static final String DRIVER = "com.mysql.jdbc.Driver";

	public static DatabaseManager getDatabaseManager() {
		try {
			Class.forName("com.mysql.jdbc.Driver").newInstance();
			DatabaseManager dbm = DatabaseManager.getUrlDefinedDatabaseManager(DATABASE, POOLSIZE, DRIVER, URL, null, null, USERNAME, PASSWORD);
			return dbm;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}
}
