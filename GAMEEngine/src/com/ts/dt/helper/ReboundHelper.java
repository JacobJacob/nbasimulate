package com.ts.dt.helper;

import java.util.Hashtable;

import com.ts.dt.match.Controller;
import com.ts.dt.po.Player;

public class ReboundHelper {

	//�жϳ�����Ա�������������
	public static int[] checkPercentForGetRebound(
			Hashtable<String, Controller> controllers, boolean isHomeTeam) {
		int[] percent = new int[] { 5, 15, 25, 40, 60 };

		String teamFlg = "";
		if (isHomeTeam) {
			teamFlg = "A";
		} else {
			teamFlg = "B";
		}
		String[] positions = { "PG", "SG", "SF", "PF", "C" };

		for (int i = 0; i < 5; i++) {
			int power = checkReboundPower(controllers.get(
					positions[i] + teamFlg).getPlayer());
			percent[i] += power;
		}

		return percent;
	}

	public static int checkReboundPower(Player player) {
		return 0;
	}
}
