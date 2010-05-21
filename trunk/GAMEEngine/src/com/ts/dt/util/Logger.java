package com.ts.dt.util;

import jpersist.DatabaseManager;

import com.ts.dt.po.ErrorLog;

public class Logger {

	private static org.apache.log4j.Logger logger = org.apache.log4j.Logger.getLogger("action");

	public static void info(Object msg) {
		logger.info(msg);
	}

	public static void error(Object msg) {
		logger.error(msg);
	}

	public static void logToDb(String type, Object msg) {

		ErrorLog log = new ErrorLog();
		log.setType(type);
		if (msg != null) {
			log.setLog(msg.toString());
		} else {
			log.setLog("unknown error");
		}
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();

		try {
			dbm.saveObject(log);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
