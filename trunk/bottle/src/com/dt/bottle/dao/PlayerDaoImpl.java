package com.dt.bottle.dao;

import com.dt.bottle.session.Session;
import com.dt.bottle.test.Player;
import com.dt.bottle.util.BottleUtil;

public class PlayerDaoImpl {

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
}
