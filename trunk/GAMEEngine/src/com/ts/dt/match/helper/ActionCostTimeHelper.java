package com.ts.dt.match.helper;

import java.util.Random;

import com.ts.dt.po.Player;

public class ActionCostTimeHelper {

	// 传球所用时间
	public static long passCostTime(Player player) {
		long costTime = 0;
		costTime += randomGetCostTime() * 5;
		return costTime;
	}

	// 随机当前投篮以后所剩的时间
	public static long shootRemainTime(Player player) {
		long remainTime = 70;
		remainTime += randomGetCostTime() * 10;
		return remainTime;
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
