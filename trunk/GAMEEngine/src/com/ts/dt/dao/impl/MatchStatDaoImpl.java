package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.List;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;

public class MatchStatDaoImpl extends BaseDao implements MatchStatDao {

	public static final String INSERT_SQL = "insert into match_stat(team_id, player_no, no, position, name, "
			+ "match_id, point1_shoot_times, point1_doom_times, point2_shoot_times, point2_doom_times, point3_shoot_times, "
			+ "point3_doom_times, offensive_rebound, defensive_rebound, foul, lapsus, assist, block, steals, is_main, "
			+ "created_time) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,now())";

	public void save(MatchStat matchStat) throws MatchException {
		// TODO Auto-generated method stub
		throw new MatchException("方法未实现");
	}

	public void saveMatachStats(List<MatchStat> matchStats) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(INSERT_SQL);
			Iterator<MatchStat> iterator = matchStats.iterator();
			while (iterator.hasNext()) {
				MatchStat matchStat = iterator.next();
				int i = 1;
				stmt.setLong(i++, matchStat.getTeamId());
				stmt.setString(i++, matchStat.getPlayerNo());
				stmt.setLong(i++, matchStat.getNo());
				stmt.setString(i++, matchStat.getPosition());
				stmt.setString(i++, matchStat.getName());
				stmt.setLong(i++, matchStat.getMatchId());
				stmt.setLong(i++, matchStat.getPoint1ShootTimes());
				stmt.setLong(i++, matchStat.getPoint1DoomTimes());
				stmt.setLong(i++, matchStat.getPoint2ShootTimes());
				stmt.setLong(i++, matchStat.getPoint2DoomTimes());
				stmt.setLong(i++, matchStat.getPoint3ShootTimes());
				stmt.setLong(i++, matchStat.getPoint3DoomTimes());
				stmt.setLong(i++, matchStat.getOffensiveRebound());
				stmt.setLong(i++, matchStat.getDefensiveRebound());
				stmt.setLong(i++, matchStat.getFoul());
				stmt.setLong(i++, matchStat.getLapsus());
				stmt.setLong(i++, matchStat.getAssist());
				stmt.setLong(i++, matchStat.getBlock());
				stmt.setLong(i++, matchStat.getSteals());
				stmt.setBoolean(i++, matchStat.getIsMain());
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
		MatchStatDao matchStatDao = new MatchStatDaoImpl();
		MatchStat matchStat = new MatchStat();
		// matchStatDao.save(matchStat);
	}
}
