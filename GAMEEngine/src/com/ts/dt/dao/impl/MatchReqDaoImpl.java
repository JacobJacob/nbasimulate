package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.po.Matchs;

public class MatchReqDaoImpl implements MatchReqDao {

	private static final String QUERY_NEW_REQ_SQL = "select * from matchs where status = ?";
	private static final Object[] QUERY_NEW_REQ_PARM = new Object[] { 0 };

	@SuppressWarnings("unchecked")
	public List<Matchs> getAllNewReq() {

		Session session = BottleUtil.currentSession();
		List<Matchs> list = null;
		try {
			list = session.query(Matchs.class, QUERY_NEW_REQ_SQL, QUERY_NEW_REQ_PARM);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public void remove(Matchs matchReq) {
		// TODO Auto-generated method stub

	}

}
