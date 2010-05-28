package com.ts.dt.match.test;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ProfessionPlayer;

public class TestDataFactory {
	public static ProfessionPlayer[] players = new ProfessionPlayer[10];

	static {

		ProfessionPlayer player = new ProfessionPlayer();
		player.setId(1);
		player.setShooting(80);
		player.setAvoirdupois(212);
		player.setName("姚明    ");
		player.setPosition(MatchConstant.LOCATION_C);
		player.setTeamid(2);
		players[0] = player;

		player = new ProfessionPlayer();
		player.setId(2);
		player.setShooting(80);
		player.setName("期科拉  ");
		player.setPosition(MatchConstant.LOCATION_PF);
		player.setAvoirdupois(210);
		player.setTeamid(2);
		players[1] = player;

		player = new ProfessionPlayer();
		player.setId(3);
		player.setShooting(80);
		player.setName("阿泰斯特");
		player.setPosition(MatchConstant.LOCATION_SF);
		player.setAvoirdupois(200);
		player.setTeamid(2);
		players[2] = player;

		player = new ProfessionPlayer();
		player.setId(4);
		player.setAvoirdupois(190);
		player.setPosition(MatchConstant.LOCATION_SG);
		player.setName("麦迪    ");
		player.setShooting(80);
		player.setTeamid(2);
		players[3] = player;

		player = new ProfessionPlayer();
		player.setId(5);
		player.setShooting(80);
		player.setName("阿尔斯通");
		player.setPosition(MatchConstant.LOCATION_PG);
		player.setAvoirdupois(180);
		player.setTeamid(2);
		players[4] = player;

		player = new ProfessionPlayer();
		player.setId(6);
		player.setShooting(70);
		player.setName("布泽尔  ");
		player.setPosition(MatchConstant.LOCATION_C);
		player.setAvoirdupois(210);
		player.setTeamid(1);
		players[5] = player;

		player = new ProfessionPlayer();
		player.setId(7);
		player.setShooting(70);
		player.setName("小奥尼尔");
		player.setPosition(MatchConstant.LOCATION_PF);
		player.setAvoirdupois(200);
		player.setTeamid(1);
		players[6] = player;

		player = new ProfessionPlayer();
		player.setId(8);
		player.setShooting(70);
		player.setPosition(MatchConstant.LOCATION_SF);
		player.setName("安东尼  ");
		player.setAvoirdupois(190);
		player.setTeamid(1);
		players[7] = player;

		player = new ProfessionPlayer();
		player.setId(9);
		player.setShooting(70);
		player.setName("科比    ");
		player.setPosition(MatchConstant.LOCATION_SG);
		player.setAvoirdupois(180);
		player.setTeamid(1);
		players[8] = player;

		player = new ProfessionPlayer();
		player.setId(10);
		player.setShooting(70);
		player.setName("保罗    ");
		player.setPosition(MatchConstant.LOCATION_PG);
		player.setAvoirdupois(180);
		player.setTeamid(1);
		players[9] = player;

	}

	public static void saveTestDateToDB() throws MatchException {
		ProfessionPlayerDao playerDao = new ProfessionPlayerDaoImpl();

		for (int i = 0; i < players.length; i++) {
			players[i].setId(0);
			playerDao.update(players[i]);
		}

	}
}
