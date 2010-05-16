package com.ts.dt.engine;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;

public interface MatchEngine {

	public Matchs execute(long matchid) throws MatchException;

}
