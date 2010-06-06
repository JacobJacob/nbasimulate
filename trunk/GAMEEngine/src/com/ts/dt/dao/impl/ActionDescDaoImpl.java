package com.ts.dt.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;

import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.db.ConnectionManager;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ActionDesc;

public class ActionDescDaoImpl extends BaseDao implements ActionDescDao {

	public static final String QUERY_SQL_ONE = "select * from action_desc where result=? and action_name=? and flg=?";

	public List<ActionDesc> findWithActionAndResultAndFlg(String actionNm, String result, String flg) throws MatchException {
		// TODO Auto-generated method stub
		Connection conn = null;
		PreparedStatement stmt = null;
		ResultSet rs = null;
		List<ActionDesc> list = new ArrayList<ActionDesc>();
		try {
			conn = ConnectionManager.getInstance().getConnection();
			stmt = conn.prepareStatement(QUERY_SQL_ONE);
			int i = 1;
			stmt.setString(i++, result);
			stmt.setString(i++, actionNm);
			stmt.setString(i++, flg);
			rs = stmt.executeQuery();
			while (rs.next()) {
				ActionDesc actionDesc = new ActionDesc();
				actionDesc.setActionDesc(rs.getString("action_desc"));
				actionDesc.setActionName(rs.getString("action_name"));
				actionDesc.setFlg(rs.getString("flg"));
				actionDesc.setId(rs.getInt("id"));
				actionDesc.setIsAssist(rs.getBoolean("is_assist"));
				actionDesc.setNotStick(rs.getBoolean("not_stick"));
				actionDesc.setPercent(rs.getInt("percent"));
				actionDesc.setResult(rs.getString("result"));
				list.add(actionDesc);
			}

			if (list.isEmpty()) {
				throw new MatchException("出现比赛描述为空的情况[" + actionNm + "][" + result + "][" + flg + "]");
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

	public static void main(String[] args) throws MatchException {

		long start = System.currentTimeMillis();
		for (int i = 0; i < 10000; i++) {
			List<ActionDesc> list = new ActionDescDaoImpl().findWithActionAndResultAndFlg("ShortShoot", "success", "not_foul");
			System.out.println(list.size());
		}
		long end = System.currentTimeMillis();
		long useTime = end - start;
		System.out.println("user time[" + useTime + "]");
	}
}
