package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;

public interface MatchStatDao {
	public void save(MatchStat matchStat) throws MatchException;
}
