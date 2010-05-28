package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ErrorMatch;

public interface ErrorMatchDao {

	public void save(ErrorMatch errorMatch) throws MatchException;
}
