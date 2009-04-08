package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.po.MatchMain;

public class MatchDaoImpl implements MatchDao {

	public void save(MatchMain match) {
		// TODO Auto-generated method stub
		Session session = new Session();
		session.beginTransaction();
		try {
			if (match.getId() > 0) {
				//session.update(match);
			} else {
				session.save(match);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}

}
