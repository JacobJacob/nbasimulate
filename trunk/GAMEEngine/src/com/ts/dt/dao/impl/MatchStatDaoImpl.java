package com.ts.dt.dao.impl;

import java.util.List;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;

public class MatchStatDaoImpl extends BaseDao implements MatchStatDao {

	public void save(MatchStat matchStat) throws MatchException {
		// TODO Auto-generated method stub
		super.save(matchStat);
	}

	public void saveMatachStats(List<MatchStat> matchStats) throws MatchException {
		// TODO Auto-generated method stub
		super.saveMany(matchStats);
	}
}
