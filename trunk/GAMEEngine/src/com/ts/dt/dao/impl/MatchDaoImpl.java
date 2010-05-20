package com.ts.dt.dao.impl;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.po.Matchs;

public class MatchDaoImpl implements MatchDao {

	public void save(Matchs match) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			if (match.getId() > 0) {
				session.update(match);
			} else {
				session.save(match);
			}

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			session.endTransaction();
		}
	}

	public Matchs load(long id) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		try {
			return (Matchs) session.load(Matchs.class, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

}
