package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ErrorLog;

public interface ErrorLogDao {

	public void save(ErrorLog errorLog) throws MatchException;
}
