package com.ts.dt.dao.impl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import jpersist.DatabaseManager;

import com.ts.dt.dao.YouthPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;
import com.ts.dt.util.DatabaseManagerUtil;

public class YouthPlayerDaoImpl implements YouthPlayerDao {

	public void save(YouthPlayer player) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			dbm.saveObject(player);
		} catch (Exception e) {
			throw new MatchException(e);
		}
	}

	public Player load(long id) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		Player player = null;
		try {
			player = dbm.loadObject(YouthPlayer.class, "where :id = ?", id);
			if (player == null) {
				throw new MatchException("«Ú‘±≤ª¥Ê‘⁄ID[" + id + "]");
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
		return player;
	}

	public Player load(String no) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		Player player = null;
		try {
			player = dbm.loadObject(YouthPlayer.class, "where :no = ?", no);

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
		return player;
	}

	public List<Player> getPlayerWithTeamId(long teamId) {

		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		List<Player> list = new ArrayList<Player>();
		try {
			Collection<YouthPlayer> collection = dbm.loadObjects(new ArrayList<YouthPlayer>(), YouthPlayer.class, "where :teamId=?", teamId);
			list.addAll(collection);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}

		return list;
	}
}
