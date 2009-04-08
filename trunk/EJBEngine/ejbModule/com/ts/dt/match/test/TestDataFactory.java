package com.ts.dt.match.test;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.dao.imp.PlayerDaoImpl;
import com.ts.dt.po.Player;

public class TestDataFactory {
	public static final Player[] players = new Player[10];

	static {

		Player player = new Player();
		player.setId(1);
		player.setShooting(80);
		player.setAvoirdupois(212);
		player.setName("姚明    ");
		player.setPosition(MatchConstant.POSITION_C);
		player.setTeamId(2);
		players[0] = player;

		player = new Player();
		player.setId(2);
		player.setShooting(80);
		player.setName("期科拉  ");
		player.setPosition(MatchConstant.POSITION_PF);
		player.setAvoirdupois(210);
		player.setTeamId(2);
		players[1] = player;

		player = new Player();
		player.setId(3);
		player.setShooting(80);
		player.setName("阿泰斯特");
		player.setPosition(MatchConstant.POSITION_SF);
		player.setAvoirdupois(200);
		player.setTeamId(2);
		players[2] = player;

		player = new Player();
		player.setId(4);
		player.setAvoirdupois(190);
		player.setPosition(MatchConstant.POSITION_SG);
		player.setName("麦迪    ");
		player.setShooting(80);
		player.setTeamId(2);
		players[3] = player;

		player = new Player();
		player.setId(5);
		player.setShooting(80);
		player.setName("阿尔斯通");
		player.setPosition(MatchConstant.POSITION_PG);
		player.setAvoirdupois(180);
		player.setTeamId(2);
		players[4] = player;

		player = new Player();
		player.setId(6);
		player.setShooting(70);
		player.setName("布泽尔  ");
		player.setPosition(MatchConstant.POSITION_C);
		player.setAvoirdupois(210);
		player.setTeamId(1);
		players[5] = player;

		player = new Player();
		player.setId(7);
		player.setShooting(70);
		player.setName("小奥尼尔");
		player.setPosition(MatchConstant.POSITION_PF);
		player.setAvoirdupois(200);
		player.setTeamId(1);
		players[6] = player;

		player = new Player();
		player.setId(8);
		player.setShooting(70);
		player.setPosition(MatchConstant.POSITION_SF);
		player.setName("安东尼  ");
		player.setAvoirdupois(190);
		player.setTeamId(1);
		players[7] = player;

		player = new Player();
		player.setId(9);
		player.setShooting(70);
		player.setName("科比    ");
		player.setPosition(MatchConstant.POSITION_SG);
		player.setAvoirdupois(180);
		player.setTeamId(1);
		players[8] = player;

		player = new Player();
		player.setId(10);
		player.setShooting(70);
		player.setName("保罗    ");
		player.setPosition(MatchConstant.POSITION_PG);
		player.setAvoirdupois(180);
		player.setTeamId(1);
		players[9] = player;

	}

	public static void saveTestDateToDB() {
		PlayerDao playerDao = new PlayerDaoImpl();

		for (int i = 0; i < players.length; i++) {
			players[i].setId(0);
			playerDao.save(players[i]);
		}

	}
}
