package com.ts.dt.dao.impl;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.po.MatchStat;

public class MatchStatDaoImpl implements MatchStatDao {

	public void save(MatchStat matchStat) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(matchStat);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			session.endTransaction();
		}
	}
}
