package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import com.ts.dt.dao.ErrorMatchDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ErrorMatch;

public class ErrorMatchDaoImpl extends BaseDao implements ErrorMatchDao {

	public static final String INSERT_SQL = "insert into error_match(match_id, type, remark, client, created_time) values(?,?,?,?,now())";

	public void save(ErrorMatch errorMatch) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(INSERT_SQL);
			int i = 1;
			stmt.setLong(i++, errorMatch.getMatchId());
			stmt.setInt(i++, errorMatch.getType());
			stmt.setString(i++, errorMatch.getRemark());
			stmt.setString(i++, errorMatch.getClient());
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

}
