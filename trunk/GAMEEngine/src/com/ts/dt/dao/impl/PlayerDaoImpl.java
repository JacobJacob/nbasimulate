package com.ts.dt.dao.impl;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.po.Player;

public class PlayerDaoImpl implements PlayerDao {


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
			player = (Player) session.load(Player.class, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return player;
	}
}
