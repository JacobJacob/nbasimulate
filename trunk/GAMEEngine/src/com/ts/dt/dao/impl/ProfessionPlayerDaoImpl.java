package com.ts.dt.dao.impl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import jpersist.DatabaseManager;
import jpersist.JPersistException;

import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.DatabaseManagerUtil;

public class ProfessionPlayerDaoImpl implements ProfessionPlayerDao {

	public void save(ProfessionPlayer player) throws MatchException {
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
			player = dbm.loadObject(ProfessionPlayer.class, "where :id = ?", id);
			if (player == null) {
				throw new MatchException("球员不存在ID[" + id + "]");
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
			player = dbm.loadObject(ProfessionPlayer.class, "where :no =?", no);
			if (player == null) {
				throw new MatchException("球员不存在NO[" + no + "]");
			}
		} catch (JPersistException jp) {
			jp.printStackTrace();
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
		return player;
	}

	public List<Player> getPlayerWithTeamId(long teamId) throws MatchException {

		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		List<Player> list = new ArrayList<Player>();
		try {
			Collection<ProfessionPlayer> collection = dbm.loadObjects(new ArrayList<ProfessionPlayer>(), ProfessionPlayer.class, "where :teamId=?", teamId);
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

	public static void main(String[] args) throws MatchException {
		Player player = new ProfessionPlayerDaoImpl().load("");
		System.out.println(player);
	}
}
