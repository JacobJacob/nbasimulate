package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;

public interface MatchDao {

	public void save(Matchs match) throws MatchException;

	public Matchs load(long id) throws MatchException;

}
