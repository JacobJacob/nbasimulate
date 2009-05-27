package com.ts.dt.util;

public class Logger {

	private static org.apache.log4j.Logger logger = org.apache.log4j.Logger.getLogger("action");

	public static void info(Object msg) {
		logger.info(msg);
	}

	public static void error(Object msg) {
		logger.error(msg);
	}
}
