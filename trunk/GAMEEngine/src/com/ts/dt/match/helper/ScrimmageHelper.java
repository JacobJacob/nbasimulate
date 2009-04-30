package com.ts.dt.match.helper;

import com.ts.dt.po.Player;

public class ScrimmageHelper {

	public static int checkScrimmageResult(Player player, Player defender) {

		int percent = 50;

		percent += (checkScrimaagePower(player) - checkScrimaagePower(defender));

		return percent;
	}

	public static int checkScrimaagePower(Player player) {

		int power = -1;

		power = player.getStature();

		return power;
	}

}
