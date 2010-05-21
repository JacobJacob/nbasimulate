package com.ts.dt.dao.impl;

import java.util.List;

import jpersist.DatabaseManager;
import jpersist.JPersistException;

import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.DatabaseManagerUtil;

public class ProfessionPlayerDaoImpl implements ProfessionPlayerDao {

	private static final String QUERY_SQL = "SELECT * FROM profession_player WHERE team_id = ? ";

	public void save(ProfessionPlayer player) {
		// TODO Auto-generated method stub
		// DatabaseManager.getUrlDefinedDatabaseManager(databaseName, poolSize,
		// driver, url, null, null, username, password)
		// player.save(database)
	}

	public Player load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		Player player = null;
		try {
			player = (Player) session.load(ProfessionPlayer.class, id);
		} catch (ObjectNotFoundException ne) {
			throw new MatchException("«Ú‘±≤ª¥Ê‘⁄ID[" + id + "]");
		} catch (Exception e) {
			throw new MatchException(e);
		}
		return player;
	}

	public Player load(String no) throws MatchException {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		Player player = null;
		try {
			player = new ProfessionPlayer();
			player.setNo(no);
			player = dbm.loadObject(player);
		} catch (JPersistException jp) {
			jp.printStackTrace();
		}
		return player;
	}

	@SuppressWarnings("unchecked")
	public List<Player> getPlayerWithTeamId(long teamId) {

		// Session session = BottleUtil.currentSession();
		//
		// List<Player> list = null;
		// try {
		// list = (List<Player>) session.query(ProfessionPlayer.class,
		// QUERY_SQL, new Object[] { teamId });
		// } catch (SessionException e) {
		// e.printStackTrace();
		// }
		// return list;
		return null;
	}

	public static void main(String[] args) throws MatchException {
		Player player = new ProfessionPlayerDaoImpl().load("");
		System.out.println(player);
	}
}
