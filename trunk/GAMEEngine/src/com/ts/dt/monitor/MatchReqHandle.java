package com.ts.dt.monitor;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.constants.MatchStatus;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.po.Matchs;
import com.ts.dt.pool.MatchReqPool;
import com.ts.dt.util.Logger;

public class MatchReqHandle extends Thread {

	private MatchEngine engine = new MatchEngineImpl();

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {

			try {
				Matchs match = MatchReqPool.get();
				match = engine.execute(match.getId());

				Session session = BottleUtil.currentSession();
				// req.setMatchId(matchId);
				match.setStatus(MatchStatus.FINISH);
				session.beginTransaction();
				try {
					match.save();
				} catch (Exception e) {
					e.printStackTrace();
				}
				session.endTransaction();
			} catch (Exception e) {
				Logger.logToDb("error", e.getMessage());
			}
		}

	}
}
