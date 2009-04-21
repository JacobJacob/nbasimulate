package com.ts.dt.util;

public class TimeUtil {

	public static String timeMillis2Time(long timeMillis) {
		int min = (int) (timeMillis / 600);
		int point = (int) (timeMillis - (600 * min)) / 10;
		return min + ":" + point;
	}

	public static String timeMillis2TimeFormat(long timeMillis) {

		return "[" + timeMillis2Time(timeMillis) + "]";
	}
}
