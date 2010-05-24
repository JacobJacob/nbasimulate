package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNodosityMain;

public interface MatchNodosityMainDao {

	public void save(MatchNodosityMain matchNodosityMain) throws MatchException;
}
