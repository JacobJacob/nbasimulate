package com.ts.dt.dao.impl;

import org.hibernate.Query;
import org.hibernate.Session;

import com.ts.dt.dao.TacticalDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;
import com.ts.dt.util.HibernateUtil;

public class TacticalDaoImpl extends BaseDao implements TacticalDao {

	public TeamTactical loadTeamTactical(long teamId, int type) throws MatchException {

		// TODO Auto-generated method stub
		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from TeamTactical a where a.teamId = :teamId and a.type = :type");
		q.setLong("teamId", teamId);
		q.setInteger("type", type);
		TeamTactical teamTactical = (TeamTactical) q.uniqueResult();
		return teamTactical;
	}

	public TeamTacticalDetail loadTeamTacticalDetail(long id) throws MatchException {
		// TODO Auto-generated method stub
		TeamTacticalDetail teamTacticalDetail = (TeamTacticalDetail) super.load(TeamTacticalDetail.class, id);
		return teamTacticalDetail;
	}
}
