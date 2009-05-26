package com.ts.dt.monitor;

import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.po.MatchReq;
import com.ts.dt.pool.MatchReqPool;

public class MatchReqHandle extends Thread {

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			MatchReq req = MatchReqPool.get();
			new MatchEngineImpl().execute(req.getHomeTeamId(), req.getVisitingTeamId(), "CAR");

		}

	}

}
