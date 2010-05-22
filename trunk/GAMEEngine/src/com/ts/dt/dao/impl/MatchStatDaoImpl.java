package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchStatDaoImpl implements MatchStatDao {

	public void save(MatchStat matchStat) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			dbm.saveObject(matchStat);
		} catch (Exception e) {
			throw new MatchException(e);
		}
	}
}
