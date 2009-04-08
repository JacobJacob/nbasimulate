package com.ts.dt.dao.imp;

import com.dt.bottle.session.Session;
import com.ts.dt.dao.TeamDao;
import com.ts.dt.po.Team;

public class TeamDaoImpl implements TeamDao {

	@Override
	public Team load(long id) {
		// TODO Auto-generated method stub
		Session session = Session.getInstance();
		Team team = null;
		try {
			//team = (Team) session.load(Team.class, id);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return team;
	}
}
