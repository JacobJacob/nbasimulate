package com.ts.dt.dao.impl;

import com.ts.dt.dao.MatchDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;

public class MatchDaoImpl extends BaseDao implements MatchDao {

	public void save(Matchs match) throws MatchException {
		// TODO Auto-generated method stub
		super.save(match);
	}

	public Matchs load(long id) throws MatchException {
		// TODO Auto-generated method stub
		return (Matchs) super.load(Matchs.class, id);

	}
}
