package com.ts.dt.service.impl;

import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.service.MatchService;

public class MatchServiceImpl implements MatchService {

	public void execute(RequestContext reqCxt, ResponseContext resCxt) {
		// TODO Auto-generated method stub
		long homeTeamId = 1;
		long visistingTeamId = 2;
		MatchEngine matchEngine = new MatchEngineImpl();
		matchEngine.execute(homeTeamId, visistingTeamId);
	}

}
