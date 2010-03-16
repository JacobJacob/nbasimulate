package com.ts.dt.match.helper;

import java.util.Iterator;
import java.util.List;

import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.match.Controller;
import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.Logger;

public class SubstitutionHelper {

	public static void FoulOutSubstitution(MatchContext context) {

		Logger.info("start to substitution....");
		Controller foutOutController = context.getFoulOutController();
		String position = foutOutController.getPlayer().getPosition();
		long teamId = foutOutController.getPlayer().getTeamid();

		ProfessionPlayerDao playerDao = new ProfessionPlayerDaoImpl();

		List<ProfessionPlayer> list = playerDao.getPlayerWithTeamId(teamId);

		Iterator<ProfessionPlayer> iterator = list.iterator();

		ProfessionPlayer onCourtPlayer = null;

		while (iterator.hasNext()) {
			ProfessionPlayer player = iterator.next();
			if (context.hasOnCourt(player.getNo())) {
				continue;
			} else {
				onCourtPlayer = choosePlayer(position, onCourtPlayer, player);
			}
		}
		foutOutController.setPlayer(onCourtPlayer);
		System.out.println(onCourtPlayer.getName() + "Ìæ»»ÉÏ³¡");
	}

	// check whether the player is better than onCourtPlayer to on Court
	private static ProfessionPlayer choosePlayer(String position, ProfessionPlayer onCourtPlayer, ProfessionPlayer player) {

		if (onCourtPlayer == null) {
			onCourtPlayer = player;
		} else {
			if (position.equals(player.getPosition())) {
				if (onCourtPlayer.getPosition() != position) {
					onCourtPlayer = player;
				} else if (player.getColligate() > onCourtPlayer.getColligate()) {
					onCourtPlayer = player;
				}
			} else if (onCourtPlayer.getPosition() != position && player.getColligate() > onCourtPlayer.getColligate()) {
				onCourtPlayer = player;
			}
		}

		return onCourtPlayer;
	}
}
