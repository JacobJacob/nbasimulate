package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.po.MatchStat;

public class MatchStatDaoImpl implements MatchStatDao {

	public void save(MatchStat matchStat) {
		// TODO Auto-generated method stub
		Session session = new Session();
		session.beginTransaction();
		try {
			session.save(matchStat);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}

}
