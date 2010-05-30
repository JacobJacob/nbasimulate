package com.ts.dt.dao.impl;

import com.ts.dt.dao.MatchDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;

public class MatchDaoImpl extends BaseDao implements MatchDao {

	public void update(Matchs match) throws MatchException {
		// TODO Auto-generated method stub
		super.update(match);
	}

	public Matchs load(long id) throws MatchException {
		// TODO Auto-generated method stub
		return (Matchs) super.load(Matchs.class, id);
	}

	public static void main(String[] args) throws MatchException {
		MatchDao matchDao = new MatchDaoImpl();
		Matchs match = matchDao.load(100);
		match.setStatus(1);
		matchDao.update(match);
	}
}
