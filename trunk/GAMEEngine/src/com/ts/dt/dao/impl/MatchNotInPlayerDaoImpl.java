package com.ts.dt.dao.impl;

import java.util.ArrayList;
import java.util.List;

import com.ts.dt.dao.MatchNotInPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNotInPlayer;
import com.ts.dt.po.Player;

public class MatchNotInPlayerDaoImpl extends BaseDao implements MatchNotInPlayerDao {

	public void saveMatchNotInPlayers(final List<MatchNotInPlayer> notInPlayers) throws MatchException {
		// TODO Auto-generated method stub
		super.saveMany(notInPlayers);
	}

	public static void main(String[] args) throws MatchException {
		List<MatchNotInPlayer> list = new ArrayList<MatchNotInPlayer>();
		for (int i = 0; i < 5; i++) {
			MatchNotInPlayer player = new MatchNotInPlayer();
			player.setTeamId(12345);
			player.setPlayerNo("12113213213" + i);
			player.setAbility(new Double(123));
			player.setMatchId(123445);
			list.add(player);
		}
		new MatchNotInPlayerDaoImpl().saveMatchNotInPlayers(list);
	}

}
