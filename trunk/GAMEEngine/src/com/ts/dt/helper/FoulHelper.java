package com.ts.dt.helper;

import com.ts.dt.context.MatchContext;
import com.ts.dt.po.Player;

public class FoulHelper {

	// check the foul possibility
	public static int checkDefensiveFoulAfterShoot(MatchContext context) {

		int percent = 5;

		Player player = context.getCurrentController().getPlayer();
		Player defender = context.getCurrentDefender().getPlayer();

		percent += (int) ((checkOffensivePower(player) - checkDefensivePower(defender)) / 10);

		return percent;
	}
    /**
     * implement this to check offensive power
     * @param player
     * @return
     */
	private static int checkOffensivePower(Player player) {
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
	private static int checkDefensivePower(Player player) {
		int power = 0;
		power += player.getAvoirdupois();
		power += player.getStature();

		return power;
	}
}
