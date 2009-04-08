package com.ts.dt.util;

public class StringUtil {

	public static String className2ShortName(Object obj) {

		String className = obj.getClass().getName();
		className = className.substring(className.lastIndexOf(".") + 1,
				className.length());
		return className;
	}

	public static String className2ShortNameWithHigherPackage(Object obj) {

		String className = obj.getClass().getName();
		String classNameTemp = className.substring(0, className
				.lastIndexOf("."));
		String shortNameWithHigerPackage = classNameTemp
				.substring(classNameTemp.lastIndexOf(".") + 1);
		shortNameWithHigerPackage += ".";
		shortNameWithHigerPackage += className.substring(className
				.lastIndexOf(".") + 1);
		return shortNameWithHigerPackage;
	}

}
