package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.po.Player;

public class PlayerDaoImpl implements PlayerDao {

	public void save(Player player) {
		// TODO Auto-generated method stub
		Session session = new Session();
		session.beginTransaction();
		try {
			session.save(player);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}
}
