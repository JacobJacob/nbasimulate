package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Iterator;

import com.ts.dt.dao.MatchNodosityMainDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNodosityDetail;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.MatchNodosityTacticalDetail;

public class MatchNodosityMainDaoImpl extends BaseDao implements MatchNodosityMainDao {

	public static final String INSERT_SQL = "insert into match_nodosity_main(seq, match_id, home_offensive_tactic, home_defend_tactic, guest_offensive_tactic, guest_defend_tactic,point, created_time) values(?,?,?,?,?,?,?,now())";
	public static final String GET_LAST_ID = "select LAST_INSERT_ID() as id from match_nodosity_main limit 1";
	public static final String INSERT_LIST_SQL = "insert into match_nodosity_tactical_detail (match_nodosity_main_id,position, player_no, player_name, ability, power,age,stature,avoirdupois,no,created_time) values(?,?,?,?,?,?,?,?,?,?,now())";
	public static final String INSERT_DETAIL_SQL = "insert into match_nodosity_detail (match_id, seq, description, time_msg, point_msg, match_nodosity_main_id, created_time, is_new_line) values(?,?,?,?,?,?,now(),?)";

	public void save(MatchNodosityMain matchNodosityMain) throws MatchException {
		// TODO Auto-generated method stub
		// super.save(matchNodosityMain);
		Connection conn = null;
		PreparedStatement stmt = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			conn.setAutoCommit(false);
			stmt = conn.prepareStatement(INSERT_SQL);

			int i = 1;
			stmt.setInt(i++, matchNodosityMain.getSeq());
			stmt.setLong(i++, matchNodosityMain.getMatchId());
			stmt.setShort(i++, matchNodosityMain.getHomeOffensiveTactic());
			stmt.setShort(i++, matchNodosityMain.getHomeDefendTactic());
			stmt.setShort(i++, matchNodosityMain.getGuestOffensiveTactic());
			stmt.setShort(i++, matchNodosityMain.getGuestDefendTactic());
			stmt.setString(i++, matchNodosityMain.getPoint());
			stmt.execute();

			stmt = conn.prepareStatement(GET_LAST_ID);
			ResultSet rs = stmt.executeQuery();
			if (!rs.next()) {
				throw new MatchException("error occor while get last id");
			}
			Long id = rs.getLong("id");

			if (matchNodosityMain.getList().size() > 0) {
				stmt = conn.prepareStatement(INSERT_LIST_SQL);
				Iterator<MatchNodosityTacticalDetail> iterator = matchNodosityMain.getList().iterator();
				while (iterator.hasNext()) {
					MatchNodosityTacticalDetail detail = iterator.next();
					i = 1;
					stmt.setLong(i++, id);
					stmt.setString(i++, detail.getPosition());
					stmt.setString(i++, detail.getPlayerNo());
					stmt.setString(i++, detail.getPlayerName());
					stmt.setFloat(i++, detail.getAbility());
					stmt.setInt(i++, detail.getPower());
					stmt.setInt(i++, detail.getAge());
					stmt.setInt(i++, detail.getStature());
					stmt.setInt(i++, detail.getAvoirdupois());
					stmt.setInt(i++, detail.getNo());
					stmt.addBatch();
				}
				stmt.executeBatch();
			}

			if (matchNodosityMain.getDetail().size() > 0) {
				stmt = conn.prepareStatement(INSERT_DETAIL_SQL);
				Iterator<MatchNodosityDetail> iterator = matchNodosityMain.getDetail().iterator();
				while (iterator.hasNext()) {
					MatchNodosityDetail detail = iterator.next();
					i = 1;
					stmt.setLong(i++, detail.getMatchId());
					stmt.setLong(i++, detail.getSeq());
					stmt.setString(i++, detail.getDescription());
					stmt.setString(i++, detail.getTimeMsg());
					stmt.setString(i++, detail.getPointMsg());
					stmt.setFloat(i++, id);
					stmt.setBoolean(i++, detail.getIsNewLine());
					stmt.addBatch();
				}
				stmt.executeBatch();
			}
			conn.commit();

		} catch (Exception e) {
			try {
				conn.rollback();
			} catch (SQLException sqlex) {
				throw new MatchException(sqlex);
			}
			e.printStackTrace();
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
		MatchNodosityMainDao matchNodosityMainDao = new MatchNodosityMainDaoImpl();
		MatchNodosityMain main = new MatchNodosityMain();
		MatchNodosityDetail detail = new MatchNodosityDetail();
		MatchNodosityTacticalDetail d = new MatchNodosityTacticalDetail();
		main.addMatchDetailLog(detail);
		main.addDetail(d);
		matchNodosityMainDao.save(main);

	}
}
