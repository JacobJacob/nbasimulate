package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;

import com.ts.dt.dao.MatchDetailDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNodosityDetail;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchDetailDaoImpl implements MatchDetailDao {

	public void save(MatchNodosityDetail matchDetail) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			dbm.saveObject(matchDetail);
		} catch (Exception e) {
			throw new MatchException(e);
		}
	}

}
