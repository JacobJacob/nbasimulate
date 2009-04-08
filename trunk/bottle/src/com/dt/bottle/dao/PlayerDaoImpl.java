package com.dt.bottle.dao;

import com.dt.bottle.session.Session;
import com.dt.bottle.test.Player;

public class PlayerDaoImpl   {

	public void save(Player player) {
		// TODO Auto-generated method stub
		Session session = Session.getInstance();
		session.beginTransaction();
		try {
			session.save(player);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}
}
