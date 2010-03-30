package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.exception.SessionException;
import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;

public class ProfessionPlayerDaoImpl implements ProfessionPlayerDao {

	private static final String QUERY_SQL = "SELECT * FROM profession_player WHERE team_id = ? ";

	public void save(ProfessionPlayer player) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(player);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}

	public Player load(long id) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		Player player = null;
		try {
			player = (Player) session.load(ProfessionPlayer.class, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return player;
	}

	public Player load(String no) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		Player player = null;
		try {
			player = (Player) session.load(ProfessionPlayer.class, "no='" + no + "'");
		} catch (Exception e) {
			e.printStackTrace();
		}
		return player;
	}

	@SuppressWarnings("unchecked")
	public List<Player> getPlayerWithTeamId(long teamId) {

		Session session = BottleUtil.currentSession();

		List<Player> list = null;
		try {
			list = (List<Player>) session.query(ProfessionPlayer.class, QUERY_SQL, new Object[] { teamId });
		} catch (SessionException e) {
			e.printStackTrace();
		}
		return list;
	}

	public static void main(String[] args) {
		Player player = new ProfessionPlayerDaoImpl().load("a58fc1d3ff1beffdc6bf51e17cf6ef47");
		System.out.println(player);
	}
}
