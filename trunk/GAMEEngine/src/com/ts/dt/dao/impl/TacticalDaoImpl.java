package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import com.ts.dt.dao.TacticalDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;

public class TacticalDaoImpl extends BaseDao implements TacticalDao {

	public static final String LOAD_SQL = "select * from team_tactical where team_id=? and type=?";
	public static final String LOAD_DETAIL_SQL = "select * from team_tactical_detail where id=?";

	public TeamTactical loadTeamTactical(long teamId, int type) throws MatchException {

		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		TeamTactical teamTactical = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_SQL);
			stmt.setLong(1, teamId);
			stmt.setInt(2, type);
			rs = stmt.executeQuery();

			if (!rs.next()) {
				throw new MatchException("战术不存在:[" + teamId + "][ " + type + "]");
			}

			teamTactical = new TeamTactical();
			teamTactical.setId(rs.getLong("id"));
			teamTactical.setTacticalDetail1Id(rs.getLong("tactical_detail_1_id"));
			teamTactical.setTacticalDetail2Id(rs.getLong("tactical_detail_2_id"));
			teamTactical.setTacticalDetail3Id(rs.getLong("tactical_detail_3_id"));
			teamTactical.setTacticalDetail4Id(rs.getLong("tactical_detail_4_id"));
			teamTactical.setTacticalDetail5Id(rs.getLong("tactical_detail_5_id"));
			teamTactical.setTacticalDetail6Id(rs.getLong("tactical_detail_6_id"));
			teamTactical.setTacticalDetail7Id(rs.getLong("tactical_detail_7_id"));
			teamTactical.setTacticalDetail8Id(rs.getLong("tactical_detail_8_id"));
			teamTactical.setTeamId(rs.getLong("team_id"));
			teamTactical.setType(rs.getInt("type"));

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
		return teamTactical;
	}

	public TeamTacticalDetail loadTeamTacticalDetail(long id) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		TeamTacticalDetail teamTacticalDetail = null;
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(LOAD_DETAIL_SQL);
			stmt.setLong(1, id);
			rs = stmt.executeQuery();

			if (!rs.next()) {
				throw new MatchException("战术详细不存在:[" + id + "]");
			}

			teamTacticalDetail = new TeamTacticalDetail();
			teamTacticalDetail.setCid(rs.getString("cid"));
			teamTacticalDetail.setName(rs.getString("name"));
			teamTacticalDetail.setPfid(rs.getString("pfid"));
			teamTacticalDetail.setSfid(rs.getString("sfid"));
			teamTacticalDetail.setSgid(rs.getString("sgid"));
			teamTacticalDetail.setPgid(rs.getString("pgid"));
			teamTacticalDetail.setOffensiveTacticalType(rs.getShort("offensive_tactical_type"));
			teamTacticalDetail.setDefendTacticalType(rs.getShort("defend_tactical_type"));
			teamTacticalDetail.setSeq(rs.getString("seq").charAt(0));
			teamTacticalDetail.setTeamId(rs.getLong("team_id"));

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
		return teamTacticalDetail;
	}
}
