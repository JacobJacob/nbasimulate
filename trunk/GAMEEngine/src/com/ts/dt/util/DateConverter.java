package com.ts.dt.util;

import java.sql.Timestamp;
import java.util.Date;

public class DateConverter {

	public static Timestamp utilDate2Timestamp(Date date) {
		return new Timestamp(date.getTime());
	}

	public static java.sql.Date utilDate2SqlDate(Date date) {
		if (date == null) {
			return null;
		}
		return new java.sql.Date(date.getTime());
	}
}
