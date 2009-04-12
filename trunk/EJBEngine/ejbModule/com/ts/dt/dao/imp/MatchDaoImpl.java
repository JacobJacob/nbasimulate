package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.po.MatchMain;

public class MatchDaoImpl implements MatchDao {

	public void save(MatchMain match) {
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
		}
		session.endTransaction();
	}

}
