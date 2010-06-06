package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import com.ts.dt.dao.YouthPlayerDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;
import com.ts.dt.util.PlayerUtil;

public class YouthPlayerDaoImpl extends BaseDao implements YouthPlayerDao {

	public static final String LOAD_BY_ID_SQL = "select * from youth_player where id=?";
	public static final String LOAD_BY_NO_SQL = "select * from youth_player where no=?";
	public static final String QUERY_BY_TEAM_ID_SQL = "select * from youth_player where team_id=?";
	public static final String UPDATE_SQL = "update youth_player set power=?, match_power=?, league_power=? where no=?";

	public void update(YouthPlayer player) throws MatchException {
		// TODO Auto-generated method stub
		throw new MatchException("方法未实现");
	}

	public Player load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		YouthPlayer player = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_BY_ID_SQL);
			stmt.setLong(1, id);
			rs = stmt.executeQuery();
			if (!rs.next()) {
				throw new MatchException("球员不存在[" + id + "]");
			}
			player = new YouthPlayer();
			PlayerUtil.result2Object(player, rs);

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			if (rs != null) {
				try {
					rs.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (stmt != null) {
				try {
					stmt.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (conn != null) {
				try {
					conn.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
		}
		return player;
	}

	public Player load(String no) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		YouthPlayer player = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_BY_NO_SQL);
			stmt.setString(1, no);
			rs = stmt.executeQuery();
			if (!rs.next()) {
				throw new MatchException("球员不存在[" + no + "]");
			}
			player = new YouthPlayer();
			PlayerUtil.result2Object(player, rs);

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			if (rs != null) {
				try {
					rs.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (stmt != null) {
				try {
					stmt.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (conn != null) {
				try {
					conn.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
		}
		return player;
	}

	public void update(List<Player> players) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(UPDATE_SQL);
			Iterator<Player> iterator = players.iterator();
			while (iterator.hasNext()) {
				Player player = iterator.next();
				int i = 1;
				stmt.setInt(i++, player.getPower());
				stmt.setInt(i++, player.getMatchPower());
				stmt.setInt(i++, player.getLeaguePower());
				stmt.setString(i++, player.getNo());
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

	public List<Player> getPlayerWithTeamId(long teamId) throws MatchException {

		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		List<Player> list = new ArrayList<Player>();
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(QUERY_BY_TEAM_ID_SQL);
			stmt.setLong(1, teamId);
			rs = stmt.executeQuery();
			while (rs.next()) {
				Player player = new YouthPlayer();
				PlayerUtil.result2Object(player, rs);
				list.add(player);
			}

		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			if (rs != null) {
				try {
					rs.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (stmt != null) {
				try {
					stmt.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
			if (conn != null) {
				try {
					conn.close();
				} catch (Exception ex) {
					throw new MatchException(ex);
				}
			}
		}
		return list;
	}
}
