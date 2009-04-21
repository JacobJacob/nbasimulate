package com.ts.dt.util;

import java.sql.Timestamp;
import java.util.Date;

public class DateConverter {
	
   public static Timestamp utilDate2Timestamp(Date date){
	   return new Timestamp(date.getTime());
   }
}
