package com.dt.bottle.util;

import java.text.*;
 

 

/**
 * <p>Title: </p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2003</p>
 * <p>Company: </p>
 * @author not attributable
 * @version 1.0
 */

public class FormatDate {

    private static java.util.Date currentDate = null;
    private static SimpleDateFormat smpDateFormat = null;
    
	 
		  
    public FormatDate() {
		 
        currentDate = new java.util.Date();
    }

    /**
     * @return String (format:yy-mm)
     */
    public static String rigorToMonth() {
        smpDateFormat = new SimpleDateFormat("yyyy-MM");
        return smpDateFormat.format(currentDate);
    }

    public static String  DateStringFormat(java.util.Date useDate)  {
        smpDateFormat = new SimpleDateFormat("yyyy-MM-dd");
        return smpDateFormat.format(useDate);
    }

    public static String  DateStringFormatB(java.util.Date useDate)  {
        smpDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        return smpDateFormat.format(useDate);
    }

    public static String  DateStringFormatFile(java.util.Date useDate)  {
        smpDateFormat = new SimpleDateFormat("yyyyMMddHHmmss");
        return smpDateFormat.format(useDate);
    }

    /**
     * @return String (format:yy-mm-dd)
     */
    public static String rigorToDay() {
        smpDateFormat = new SimpleDateFormat("yyyy-MM-dd");
        return smpDateFormat.format(currentDate);
    }

    /**
     * @return String (format:yy-mm-dd hh:mm)
     */
    public static String rigorToMin() {
        smpDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
        return smpDateFormat.format(currentDate);
    }

    /**
     * @return String (format:yy-mm-dd hh:mm:ss)
     */
    public static String rigorToSec() {
        smpDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        return smpDateFormat.format(currentDate);
    }

    /**
     * @return String (format:YYMM)
     */
    public static String tightToMon() {
        smpDateFormat = new SimpleDateFormat("yyMM");
        return smpDateFormat.format(currentDate);
    }
	public static String tightToYear() {
	   smpDateFormat = new SimpleDateFormat("yy");
	   return smpDateFormat.format(currentDate);
	}
	public static String tightYears() {
	   smpDateFormat = new SimpleDateFormat("yyyy");
	   return smpDateFormat.format(currentDate);
	}
	public static String tightMonth() {
	  smpDateFormat = new SimpleDateFormat("MM");
	  return smpDateFormat.format(currentDate);
	}
    /**
     * @return String (format:YYMM)
     */
    public static String tightToMon(String aDate) {
        try {

            smpDateFormat = new SimpleDateFormat();
            smpDateFormat.applyPattern("yyyy-MM-dd");
            smpDateFormat.setLenient(false);
            java.util.Date date = smpDateFormat.parse(aDate);

            smpDateFormat = new SimpleDateFormat("yyMM");
            return smpDateFormat.format(date);
        } catch (Exception e) {
            return null;
        }

    }
    
    public static boolean checkDate(String aDate) {
        try {

            smpDateFormat = new SimpleDateFormat();
            smpDateFormat.applyPattern("yyyy-MM-dd");
            smpDateFormat.setLenient(false);
            java.util.Date date = smpDateFormat.parse(aDate);

            return true;
        } catch (Exception e) {
            return false;
        }

    }

    /**
     * @return String (format:YYMMDD)
     */
    public static String tightToDay() {
        smpDateFormat = new SimpleDateFormat("yyMMdd");
        return smpDateFormat.format(currentDate);
    }

    /**
     * @return String (format:YYMMDD)
     */
    public static String tightToDay(String aDate) {
        //smpDateFormat = new SimpleDateFormat("yyMMdd");
        try {
            smpDateFormat = new SimpleDateFormat();
            smpDateFormat.applyPattern("yyyy-MM-dd");
            smpDateFormat.setLenient(false);
            java.util.Date date = smpDateFormat.parse(aDate);
            smpDateFormat = new SimpleDateFormat("yyMMdd");
            return smpDateFormat.format(date);
        } catch (Exception e) {
            return null;
        }
    }

    /**
     * @return java.sql.Timestamp (format:yy-MM-dd hh:ss)
     */
    public static java.sql.Timestamp toTimestamp() {
        return new java.sql.Timestamp(new java.util.Date().getTime());

    }

    /**
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static java.sql.Date toDate() {
        return new java.sql.Date(new java.util.Date().getTime());
    }

    public static java.util.Date getDateTime(){
    	
		  java.util.Date newDate = new java.util.Date(); 
		  String strDate = DateStringFormatB(newDate);
		  	  
		  java.util.Date timeDates = FormatDate.toDateTime(strDate);
		   
          return timeDates;
    }

    /**
     * @param String
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static java.sql.Date toDate(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat();
        try {
            dateFormat.applyPattern("yyyy-MM-dd");
            java.util.Date vDate = dateFormat.parse(date);
            return new java.sql.Date(vDate.getTime());
        } catch (ParseException e) {
            return null;
        }
    }
	/**
	   * @param String
	   * @return java.sql.Date (format:yy-MM-dd HH:mm:ss)
	   */

	public static java.sql.Date toDateTime(String date) {
	   SimpleDateFormat dateFormat = new SimpleDateFormat();
	   try {
		   dateFormat.applyPattern("yyyy-MM-dd HH:mm:ss");
		   java.util.Date vDate = dateFormat.parse(date);
		   return new java.sql.Date(vDate.getTime());
	   } catch (ParseException e) {
		   return null;
	   }
	   }
	   
    /**
     * @param String
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static java.sql.Date toMonth(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat();
        try {
            dateFormat.applyPattern("yyyy-MM");
            java.util.Date vDate = dateFormat.parse(date);
            return new java.sql.Date(vDate.getTime());
        } catch (ParseException e) {
            return null;
        }
    }

    /**
     * @param String
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static String rigorMonth(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat();
        try {
            dateFormat.applyPattern("yyyy-MM-dd HH:mm");
            java.util.Date vDate = dateFormat.parse(date);
            return new SimpleDateFormat("yyyy-MM").format(vDate);
        } catch (ParseException e) {
            return null;
        }
    }
	 

    /**
     * @param String
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static String rigorMin(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat();
        try {
            dateFormat.applyPattern("yyyy-MM-dd HH:mm");
            java.util.Date vDate = dateFormat.parse(date);
            return new SimpleDateFormat("yyyy-MM-dd HH:mm").format(vDate);
        } catch (ParseException e) {
            return null;
        }
    }

    /**
     * @param String
     * @return java.sql.Date (format:yy-MM-dd)
     */
    public static String rigorSec(String date) {
        SimpleDateFormat dateFormat = new SimpleDateFormat();
        try {
            dateFormat.applyPattern("yyyy-MM-dd HH:mm:ss");
            java.util.Date vDate = dateFormat.parse(date);
            return new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(vDate);
        } catch (ParseException e) {
            return null;
        }
    }

    public static void main(String[] args) {

        FormatDate formatDate1 = new FormatDate();
         
       

    }

}
