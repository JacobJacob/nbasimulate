package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Team;

public interface TeamDao {
	public Team load(long id) throws MatchException;
}
