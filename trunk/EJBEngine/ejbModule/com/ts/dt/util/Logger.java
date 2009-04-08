package com.ts.dt.util;

public class Logger {

	public static void log(String message) {
		org.apache.log4j.Logger logger = org.apache.log4j.Logger
				.getLogger("action");
		logger.info(message);
	}

	public static void error(String message) {
		org.apache.log4j.Logger logger = org.apache.log4j.Logger
				.getLogger("action");
		logger.info(message);
	}

}
