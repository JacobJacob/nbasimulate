package com.ts.dt.dao.impl;

import com.ts.dt.dao.MatchDetailDao;
import com.ts.dt.po.MatchNodosityDetail;

public class MatchDetailDaoImpl implements MatchDetailDao {

	public void save(MatchNodosityDetail matchDetail) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(matchDetail);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			session.endTransaction();
		}
	}

}
