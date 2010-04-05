package com.ts.dt.util;

public class TacticalUtil {

	public static int matchType2Tactical(int matchType) {

		if (matchType == 5) {
			return 1;
		} else if (matchType == 1) {
			return 3;
		} else {
			return 3;
		}
	}
}
