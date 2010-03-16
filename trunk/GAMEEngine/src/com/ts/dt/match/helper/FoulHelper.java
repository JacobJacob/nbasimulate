package com.ts.dt.match.helper;

import com.ts.dt.context.MatchContext;
import com.ts.dt.po.ProfessionPlayer;

public class FoulHelper {

	// check the foul possibility
	public static int checkDefensiveFoulAfterShoot(MatchContext context) {

		int percent = 10;

		ProfessionPlayer player = context.getCurrentController().getPlayer();
		ProfessionPlayer defender = context.getCurrentDefender().getPlayer();

		percent += (int) ((checkOffensivePower(player) - checkDefensivePower(defender)) / 10);

		return percent;
	}
    /**
     * implement this to check offensive power
     * @param player
     * @return
     */
	private static int checkOffensivePower(ProfessionPlayer player) {
		int power = 0;

		power += player.getAvoirdupois();
		power += player.getStature();

		return power;
	}
    /**
     * implement this to check defensive power
     * @param player
     * @return
     */
	private static int checkDefensivePower(ProfessionPlayer player) {
		int power = 0;
		power += player.getAvoirdupois();
		power += player.getStature();

		return power;
	}
}
