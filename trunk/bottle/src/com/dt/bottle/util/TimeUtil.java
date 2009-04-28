package com.dt.bottle.util;

import java.util.Calendar;
import java.util.Date;

public class TimeUtil {
	public static Date getLocalDate() {
		
		Date date = Calendar.getInstance().getTime();

		return date;
	}
}
