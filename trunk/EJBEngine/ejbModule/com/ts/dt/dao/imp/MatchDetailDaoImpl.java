package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.ts.dt.dao.MatchDetailDao;
import com.ts.dt.po.MatchDetail;

public class MatchDetailDaoImpl implements MatchDetailDao {

	public void save(MatchDetail matchDetail) {
		// TODO Auto-generated method stub
		Session session = new Session();
		session.beginTransaction();
		try {
			session.save(matchDetail);
		} catch (Exception e) {
			e.printStackTrace();
		}
		session.endTransaction();
	}

}
