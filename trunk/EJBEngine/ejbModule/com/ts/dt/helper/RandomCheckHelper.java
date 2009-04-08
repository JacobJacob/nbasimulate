package com.ts.dt.helper;

import java.util.Random;

public class RandomCheckHelper {

	private static final Random random = new Random();
	private static final int DEFAULT_RANDOM_NUMBER = 100;

	public static boolean defaultCheck(int percent) {

		boolean result = false;

		int i = random.nextInt(DEFAULT_RANDOM_NUMBER);

		if (i < percent) {
			result = true;
		}

		return result;
	}
}
