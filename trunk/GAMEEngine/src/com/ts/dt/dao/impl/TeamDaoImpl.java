package com.ts.dt.dao.impl;

import com.ts.dt.dao.TeamDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Team;

public class TeamDaoImpl extends BaseDao implements TeamDao {

	public Team load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Team team = (Team) super.load(Team.class, id);
		return team;
	}
}
