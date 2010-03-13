package com.ts.dt.dao.impl;

import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.TacticalDao;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;

public class TacticalDaoImpl implements TacticalDao {

	private static final String QUERY_TACTICS_SQL = "SELECT * FROM team_tactics WHERE team_id = ? AND flg =?";
	private static final String QUERY_TACTICS_DETAIL_SQL = "SELECT * FROM team_tactics_detail WHERE team_tactics_id = ? AND seq =?";

	@SuppressWarnings("unchecked")
	public TeamTactical loadTeamTactical(long teamId, String matchType) {

		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		List<TeamTactical> list = null;
		try {
			list = (List<TeamTactical>) session.query(TeamTactical.class,
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
	public TeamTacticalDetail loadTeamTacticalDetail(long tacticsId, int seq) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		List<TeamTacticalDetail> list = null;
		try {
			list = (List<TeamTacticalDetail>) session.query(
					TeamTacticalDetail.class, QUERY_TACTICS_DETAIL_SQL,
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
