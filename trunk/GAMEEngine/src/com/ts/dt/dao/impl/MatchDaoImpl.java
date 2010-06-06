package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.ts.dt.dao.MatchDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.DateConverter;

public class MatchDaoImpl extends BaseDao implements MatchDao {

	public static final String LOAD_SQL = "select * from matchs where id=?";
	public static final String UPDATE_SQL = "update matchs set status=?,sub_status=?, point=?, overtime=?, start_time=?, client=? where id=?";

	public void update(Matchs match) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(UPDATE_SQL);
			int i = 1;
			stmt.setInt(i++, match.getStatus());
			stmt.setInt(i++, match.getSubStatus());
			stmt.setString(i++, match.getPoint());
			stmt.setInt(i++, match.getOvertime());
			stmt.setDate(i++, DateConverter.utilDate2SqlDate(match.getStartTime()));
			stmt.setString(i++, match.getClient());
			stmt.setLong(i++, match.getId());
			stmt.execute();

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

	public Matchs load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		Matchs match = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_SQL);
			stmt.setLong(1, id);
			rs = stmt.executeQuery();
			if (!rs.next()) {
				throw new MatchException("比赛不存在[" + id + "]");
			}
			match = new Matchs();
			match.setId(id);
			match.setHomeTeamId(rs.getLong("home_team_id"));
			match.setClient(rs.getString("client"));
			match.setGuestTeamId(rs.getLong("guest_team_id"));
			match.setIsYouth(rs.getBoolean("is_youth"));
			match.setOvertime(rs.getInt("overtime"));
			match.setPoint(rs.getString("point"));
			match.setStartTime(rs.getDate("start_time"));
			match.setStatus(rs.getInt("status"));
			match.setSubStatus(rs.getInt("sub_status"));
			match.setType(rs.getInt("type"));

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
		return match;
	}

	public static void main(String[] args) throws MatchException {
		MatchDao matchDao = new MatchDaoImpl();
		Matchs match = matchDao.load(100);
		match.setStatus(1);
		System.out.println(match.getClient());
		matchDao.update(match);
	}
}
