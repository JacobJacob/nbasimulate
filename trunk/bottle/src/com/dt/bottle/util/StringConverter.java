package com.dt.bottle.util;

public class StringConverter {

	public static String parmArray2String(Object[] parm) {

		StringBuffer sb = new StringBuffer();
		sb.append("{");
		for (Object obj : parm) {
			sb.append(obj);
			sb.append(",");
		}
		sb.deleteCharAt(sb.length() - 1);
		sb.append("}");
		return sb.toString();
	}

}
