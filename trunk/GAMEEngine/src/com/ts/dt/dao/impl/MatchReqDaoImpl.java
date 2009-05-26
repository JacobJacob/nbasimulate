package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.po.MatchReq;

public class MatchReqDaoImpl implements MatchReqDao {

	private static final String QUERY_NEW_REQ_SQL = "SELECT * FROM MATCH_REQ WHERE FLAG = ?";
	private static final Object[] QUERY_NEW_REQ_PARM = new Object[] { "N" };

	@SuppressWarnings("unchecked")
	public List<MatchReq> getAllNewReq() {

		Session session = BottleUtil.currentSession();
		List<MatchReq> list = null;
		try {
			list = session.query(MatchReq.class, QUERY_NEW_REQ_SQL, QUERY_NEW_REQ_PARM);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}

	public void remove(MatchReq matchReq) {
		// TODO Auto-generated method stub

	}

}
