package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNodosityDetail;

public interface MatchDetailDao {
	public void save(MatchNodosityDetail matchDetail) throws MatchException;
}
