package com.ts.dt.dao.impl;

import java.util.Date;
import java.util.List;

import jpersist.DatabaseManager;
import jpersist.JPersistException;
import jpersist.UpdateManager;

import com.ts.dt.dao.MatchNotInPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNotInPlayer;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchNotInPlayerDaoImpl implements MatchNotInPlayerDao {

	public void saveMatchNotInPlayers(final List<MatchNotInPlayer> notInPlayers) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			new UpdateManager(dbm) {
				public void run() throws JPersistException {
					for (MatchNotInPlayer player : notInPlayers) {
						player.setCreatedTime(new Date());
						saveObject(player);
					}
				}
			}.executeBatchUpdates();
		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
	}

}
