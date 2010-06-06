package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchReq;

public class MatchReqDaoImpl extends BaseDao implements MatchReqDao {

	public static final String UPDATE_SQL = "update matchs set status=?, point=? where id=?";
	public static final String QUERY_SQL = "select id, status, point from matchs where status=? limit 5";

	public List<MatchReq> getAllNewReq() throws MatchException {

		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		List<MatchReq> list = new ArrayList<MatchReq>();
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(QUERY_SQL);
			stmt.setInt(1, MatchStatus.ACCP);
			rs = stmt.executeQuery();
			while (rs.next()) {
				MatchReq matchReq = new MatchReq();
				matchReq.setId(rs.getLong("id"));
				matchReq.setPoint(rs.getString("point"));
				matchReq.setStatus(rs.getInt("status"));
				list.add(matchReq);
			}
		} catch (Exception e) {
			throw new MatchException(e);
		} finally {
			try {
				conn.close();
			} catch (Exception ex) {

			}
		}

		return list;
	}

	public MatchReq getOneNewReq() throws MatchException {
		return null;
	}

	public void update(MatchReq matchReq) throws MatchException {
		// TODO Auto-generated method stub
		super.update(matchReq);
	}

	public void update(List<MatchReq> matchReqs) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(UPDATE_SQL);
			Iterator<MatchReq> iterator = matchReqs.iterator();
			while (iterator.hasNext()) {
				MatchReq matchReq = iterator.next();
				stmt.setInt(1, matchReq.getStatus());
				stmt.setString(2, matchReq.getPoint());
				stmt.setLong(3, matchReq.getId());
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
		MatchReqDaoImpl matchReqDaoImpl = new MatchReqDaoImpl();
		// Matchs match = matchReqDaoImpl.getOneNewReq();
		// System.out.println(match);
	}
}
