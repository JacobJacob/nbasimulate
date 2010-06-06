package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import com.ts.dt.dao.MatchNotInPlayerDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNotInPlayer;

public class MatchNotInPlayerDaoImpl extends BaseDao implements MatchNotInPlayerDao {

	public static final String INSERT_SQL = "insert into match_not_in_player(team_id, match_id, no, player_no, position, name, ability, created_time) "
			+ "values(?,?,?,?,?,?,?,now())";

	public void saveMatchNotInPlayers(final List<MatchNotInPlayer> notInPlayers) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(INSERT_SQL);
			Iterator<MatchNotInPlayer> iterator = notInPlayers.iterator();
			while (iterator.hasNext()) {
				MatchNotInPlayer player = iterator.next();
				int i = 1;
				stmt.setLong(i++, player.getTeamId());
				stmt.setLong(i++, player.getMatchId());
				stmt.setInt(i++, player.getNo());
				stmt.setString(i++, player.getPlayerNo());
				stmt.setString(i++, player.getPosition());
				stmt.setString(i++, player.getName());
				stmt.setDouble(i++, player.getAbility());
				stmt.addBatch();
			}
			stmt.executeBatch();
			conn.commit();
		} catch (Exception e) {
			try {
				conn.rollback();
			} catch (SQLException sqlex) {
				throw new MatchException(sqlex);
			}
			throw new MatchException(e);
		} finally {
			if (stmt != null) {
				try {
					stmt.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (conn != null) {
				try {
					conn.setAutoCommit(true);
					conn.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
		}
	}

	public static void main(String[] args) throws MatchException {
		List<MatchNotInPlayer> list = new ArrayList<MatchNotInPlayer>();
		for (int i = 0; i < 5; i++) {
			MatchNotInPlayer player = new MatchNotInPlayer();
			player.setTeamId(12345);
			player.setPlayerNo("12113213213" + i);
			player.setAbility(new Double(123));
			player.setPosition("");
			player.setNo(i);
			player.setName("");
			player.setMatchId(123445);
			list.add(player);
		}
		new MatchNotInPlayerDaoImpl().saveMatchNotInPlayers(list);
	}

}
