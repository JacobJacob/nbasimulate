package com.ts.dt.util;

import java.util.logging.Level;

import jpersist.DatabaseManager;

public class DatabaseManagerUtil {

	public static final String DATABASE_NAME = "gba";

	private static DatabaseManager dbm;

	public static DatabaseManager getDatabaseManager() {
		if (dbm == null) {
			try {
				DatabaseManager.setLogLevel(Level.WARNING);
				dbm = DatabaseManager.getXmlDefinedDatabaseManager(DATABASE_NAME);

			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return dbm;
	}

}
