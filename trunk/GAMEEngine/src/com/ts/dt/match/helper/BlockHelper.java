package com.ts.dt.match.helper;

import com.ts.dt.po.ProfessionPlayer;

/**
 * check the block power and vs block power
 * 
 * @author Administrator
 * 
 */
public class BlockHelper {

	public static double checkBlockPower(ProfessionPlayer player) {

		double power = 0;
		double block = player.getBlocked();
		double stature = player.getStature();
		double bounce = player.getBounce();
		double strength = player.getStrength();
		power += (stature * 1);
		power += (block * 1);
		power += (bounce * 1);
		power += (strength * 1);
		return power;
	}

	public static double checkVsBlockPower(ProfessionPlayer player) {

		double power = 0;
		double stature = player.getStature();
		double bounce = player.getBounce();
		double strength = player.getStrength();
		power += (stature * 1);
		power += (bounce * 1);
		power += (strength * 1);
		return power;
	}
}
