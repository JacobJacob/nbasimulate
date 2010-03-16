package com.ts.dt.dao;

import com.ts.dt.po.Matchs;

public interface MatchDao {

	public void save(Matchs match);

	public Matchs load(long id);

}
