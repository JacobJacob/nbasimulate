package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;

import com.ts.dt.dao.MatchDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchDaoImpl extends BaseDao implements MatchDao {

	public void save(Matchs match) throws MatchException {
		// TODO Auto-generated method stub
		super.save(match);
	}

	public Matchs load(long id) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		Matchs matchs = null;
		try {
			matchs = dbm.loadObject(Matchs.class, "where :id = ?", id);
		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
		return matchs;
	}
}
