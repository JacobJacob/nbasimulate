package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;

import com.ts.dt.dao.MatchDao;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchDaoImpl implements MatchDao {

	public void save(Matchs match) {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();

		try {
			dbm.saveObject(match);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public Matchs load(long id) {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			Matchs matchs = new Matchs();
			matchs.set
			return (Matchs) dbm.loadObject(cs)
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

}
