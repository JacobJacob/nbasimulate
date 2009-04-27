package com.ts.dt.dao.impl;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchDetailDao;
import com.ts.dt.po.MatchDetail;

public class MatchDetailDaoImpl implements MatchDetailDao {

	public void save(MatchDetail matchDetail) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(matchDetail);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}

}
