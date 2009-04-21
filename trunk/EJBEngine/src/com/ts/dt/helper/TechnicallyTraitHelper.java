package com.ts.dt.helper;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.po.Player;
import com.ts.dt.util.Logger;

public class TechnicallyTraitHelper {

	public static final int TECHNICALLY_TRAIT_OUT = 1; // 外线攻击型
	public static final int TECHNICALLY_TRAIT_INT = 2; // 内线突破型
	public static final int TECHNICALLY_TRAIT_MID = 3; // 中投型
	public static final int TECHNICALLY_TRAIT_SWIPE = 4; // 内线强打型
	public static final int TECHNICALLY_TRAIT_BREAK = 5; // 突破型

	public static int check(Player player) {

		int trait = -1;

		int[] percents = { 0, 0, 0, 0, 0 };

		int location = changePosition2Int(player.getPosition());
		switch (location) {
		case MatchConstant.POSITION_PG:
			percents[0] = 60;
			percents[1] = 40;
			percents[2] = 40;
			percents[3] = 10;
			percents[4] = 30;
			break;
		case MatchConstant.POSITION_SG:
			percents[0] = 60;
			percents[1] = 40;
			percents[2] = 40;
			percents[3] = 10;
			percents[4] = 50;
			break;
		case MatchConstant.POSITION_SF:
			percents[0] = 40;
			percents[1] = 40;
			percents[2] = 40;
			percents[3] = 20;
			percents[4] = 40;
			break;
		case MatchConstant.POSITION_PF:
			percents[0] = 20;
			percents[1] = 30;
			percents[2] = 40;
			percents[3] = 60;
			percents[4] = 10;
			break;
		case MatchConstant.POSITION_C:
			percents[0] = 10;
			percents[1] = 10;
			percents[2] = 30;
			percents[3] = 60;
			percents[4] = 20;
			break;
		default:
			Logger.error("position error ");

		}
		Random random = new Random();
		int total = percents[0] + percents[1] + percents[2] + percents[3]
				+ percents[4];
		int a = random.nextInt(total);
		if (a < percents[0]) {
			trait = TECHNICALLY_TRAIT_OUT;
		} else if (a < (percents[0] + percents[1])) {
			trait = TECHNICALLY_TRAIT_INT;
		} else if (a < (percents[0] + percents[1] + percents[2])) {
			trait = TECHNICALLY_TRAIT_MID;
		} else if (a < (percents[0] + percents[1] + percents[2] + percents[3])) {
			trait = TECHNICALLY_TRAIT_SWIPE;
		} else {
			trait = TECHNICALLY_TRAIT_BREAK;
		}

		return trait;
	}

	public static int changePosition2Int(String position) {

		if (position == MatchConstant.LOCATION_C) {
			return MatchConstant.POSITION_C;
		} else if (position == MatchConstant.LOCATION_PF) {
			return MatchConstant.POSITION_PF;
		} else if (position == MatchConstant.LOCATION_SF) {
			return MatchConstant.POSITION_SF;
		} else if (position == MatchConstant.LOCATION_SG) {
			return MatchConstant.POSITION_SG;
		} else {
			return MatchConstant.POSITION_PG;
		}
	}
}
