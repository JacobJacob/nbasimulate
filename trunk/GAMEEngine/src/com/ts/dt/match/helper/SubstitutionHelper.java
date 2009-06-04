package com.ts.dt.match.helper;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.Controller;
import com.ts.dt.util.Logger;

public class SubstitutionHelper {

	public static void FoulOutSubstitution(MatchContext context) {
		Logger.info("start to substitution....");
		Controller foutOutController = context.getFoulOutController();
		long teamId = foutOutController.getPlayer().getTeamid();
		
		List<Player> list = Player.
	}
}
