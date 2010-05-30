package com.ts.dt.match.helper;

import java.util.Random;

import com.ts.dt.match.Controller;

public class PowerHelper {

	// 计算每节的体力消耗
	public static int checkPowerCost(Controller controller, int tactical) {

		int[] positionCost = { 16, 18, 20, 22, 24 }; // C -> PF -> SF -> SG ->
		// PG
		double stamina = controller.getPlayer().getStamina();
		Random random = new Random();

		int cost = 0;
		if (controller.getControllerName().startsWith("C")) {
			cost = positionCost[0];
		} else if (controller.getControllerName().startsWith("PF")) {
			cost = positionCost[1];
		} else if (controller.getControllerName().startsWith("SF")) {
			cost = positionCost[2];
		} else if (controller.getControllerName().startsWith("SG")) {
			cost = positionCost[3];
		} else {
			cost = positionCost[4];
		}
		cost = (cost - (int) stamina / 10) + random.nextInt(5);
		return cost;
	}
}
