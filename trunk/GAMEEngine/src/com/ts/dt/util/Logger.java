package com.ts.dt.util;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
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
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(log);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}
}
