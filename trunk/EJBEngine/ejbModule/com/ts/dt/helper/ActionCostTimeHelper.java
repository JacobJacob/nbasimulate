package com.ts.dt.helper;

import java.util.Random;

import com.ts.dt.po.Player;

public class ActionCostTimeHelper {

	public static long passCostTime(Player player) {
		long costTime = 0;
		costTime += randomGetCostTime() * 8;
		return costTime;
	}

	public static long shootCostTime(Player player) {
		long costTime = 10;
		costTime += randomGetCostTime() * 10;
		return costTime;
	}

	public static long shootRemainTime(Player player) {
		long costTime = 10;
		costTime += randomGetCostTime() * 10;
		return costTime;
	}

	public static long randomGetCostTime() {
		long randomTime = -1;
		Random random = new Random();
		randomTime = random.nextInt(6);
		if (randomTime == 0) {
			randomTime = randomGetCostTime();
		}
		return randomTime;
	}
}
