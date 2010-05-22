package com.ts.dt.dao.impl;

import jpersist.DatabaseManager;
import jpersist.JPersistException;

import com.ts.dt.dao.TacticalDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;
import com.ts.dt.util.DatabaseManagerUtil;

public class TacticalDaoImpl implements TacticalDao {

	public TeamTactical loadTeamTactical(long teamId, int type) throws MatchException {

		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		TeamTactical teamTactical = null;
		try {
			teamTactical = dbm.loadObject(TeamTactical.class, "where :teamId=? and :type=?", teamId, type);
			if (teamTactical == null) {
				throw new MatchException("球队战术为空球队ID[" + teamId + "]");
			}

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}

		return teamTactical;
	}

	public TeamTacticalDetail loadTeamTacticalDetail(long id) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		TeamTacticalDetail teamTacticalDetail = null;
		try {
			teamTacticalDetail = dbm.loadObject(TeamTacticalDetail.class, "where :id = ?", id);

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
		return teamTacticalDetail;
	}

}
