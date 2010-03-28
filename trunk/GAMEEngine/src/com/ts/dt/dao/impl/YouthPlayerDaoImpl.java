package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.exception.SessionException;
import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.YouthPlayerDao;
import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;

public class YouthPlayerDaoImpl implements YouthPlayerDao {

	private static final String QUERY_SQL = "SELECT * FROM youth_player WHERE team_id = ? ";

	public void save(Player player) {
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
			player = (Player) session.load(YouthPlayer.class, id);
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
			player = (Player) session.load(YouthPlayer.class, "no='" + no + "'");
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
			list = (List<Player>) session.query(YouthPlayer.class, QUERY_SQL, new Object[] { teamId });
		} catch (SessionException e) {
			e.printStackTrace();
		}
		return list;
	}
}
