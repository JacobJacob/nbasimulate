package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.TacticsDao;
import com.ts.dt.po.TeamTactics;
import com.ts.dt.po.TeamTacticsDetail;

public class TacticsDaoImpl implements TacticsDao {

	private static final String QUERY_TACTICS_SQL = "SELECT * FROM Team_Tactics WHERE team_id = ? AND flg =?";
	private static final String QUERY_TACTICS_DETAIL_SQL = "SELECT * FROM Team_Tactics_Detail WHERE team_tactics_id = ? AND seq =?";

	@SuppressWarnings("unchecked")
	public TeamTactics loadTeamTactics(long teamId, String matchType) {

		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		List<TeamTactics> list = null;
		try {
			list = (List<TeamTactics>) session.query(TeamTactics.class,
					QUERY_TACTICS_SQL, new Object[] { teamId, matchType });
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (list.size() > 0) {
			return list.get(0);
		} else {
			return null;
		}
	}

	@SuppressWarnings("unchecked")
	public TeamTacticsDetail loadTeamTacticsDetail(long tacticsId, int seq) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		List<TeamTacticsDetail> list = null;
		try {
			list = (List<TeamTacticsDetail>) session.query(
					TeamTacticsDetail.class, QUERY_TACTICS_DETAIL_SQL,
					new Object[] { tacticsId, seq });
		} catch (Exception e) {
			e.printStackTrace();
		}
		if (list.size() > 0) {
			return list.get(0);
		} else {
			return null;
		}
	}

}
