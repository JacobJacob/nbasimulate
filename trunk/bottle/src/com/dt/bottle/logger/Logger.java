package com.dt.bottle.logger;

import java.util.Date;

public class Logger {
  
	public static void logger(String msg){
		
		System.out.println(new Date().toString() + " " + msg);
	}
}
