package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.ts.dt.dao.EngineStatusDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;

public class EngineStatusDaoImpl extends BaseDao implements EngineStatusDao {

	public static final String INSERT_SQL = "insert into engine_status(name, status, cmd, info, created_time) values(?,?,?,?,now())";
	public static final String UPDATE_SQL = "update engine_status set status=?,cmd=?, info=? where name=?";
	public static final String LOAD_SQL = "select * from engine_status where name=?";

	public void save(EngineStatus engineStatus) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(INSERT_SQL);
			int i = 1;
			stmt.setString(i++, engineStatus.getName());
			stmt.setInt(i++, engineStatus.getStatus());
			stmt.setString(i++, engineStatus.getCmd());
			stmt.setString(i++, engineStatus.getInfo());
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

	public void update(EngineStatus engineStatus) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(UPDATE_SQL);
			int i = 1;
			stmt.setInt(i++, engineStatus.getStatus());
			stmt.setString(i++, engineStatus.getCmd());
			stmt.setString(i++, engineStatus.getInfo());
			stmt.setString(i++, engineStatus.getName());
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

	public EngineStatus load(String name) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		EngineStatus engineStatus = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_SQL);
			stmt.setString(1, name);
			rs = stmt.executeQuery();

			if (rs.next()) {
				engineStatus = new EngineStatus();
				engineStatus.setId(rs.getLong("id"));
				engineStatus.setName(rs.getString("name"));
				engineStatus.setCmd(rs.getString("cmd"));
				engineStatus.setInfo(rs.getString("info"));
				engineStatus.setStatus(rs.getInt("status"));
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
		return engineStatus;

	}

}
