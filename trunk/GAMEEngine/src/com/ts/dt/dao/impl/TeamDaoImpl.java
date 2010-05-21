package com.ts.dt.dao.impl;

import com.ts.dt.dao.TeamDao;
import com.ts.dt.po.Team;

public class TeamDaoImpl implements TeamDao {

	public Team load(long id) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		Team team = null;
		try {
			team = (Team) session.load(Team.class, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return team;
	}
}
