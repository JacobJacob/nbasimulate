package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;
import jpersist.JPersistException;

import com.ts.dt.dao.TeamDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Team;
import com.ts.dt.util.DatabaseManagerUtil;

public class TeamDaoImpl implements TeamDao {

	public Team load(long id) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		Team team = null;
		try {
			team = dbm.loadObject(Team.class, "where :id = ?", id);
		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
		return team;
	}
}
