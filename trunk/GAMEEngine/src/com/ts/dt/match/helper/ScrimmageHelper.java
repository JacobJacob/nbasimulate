package com.ts.dt.match.helper;

import com.ts.dt.po.ProfessionPlayer;

public class ScrimmageHelper {

	public static int checkScrimmageResult(ProfessionPlayer player, ProfessionPlayer defender) {

		int percent = 50;

		percent += (checkScrimaagePower(player) - checkScrimaagePower(defender));

		return percent;
	}

	public static int checkScrimaagePower(ProfessionPlayer player) {

		int power = -1;

		power = player.getStature();

		return power;
	}

}
