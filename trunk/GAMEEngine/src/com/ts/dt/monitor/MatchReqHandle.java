package com.ts.dt.monitor;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.po.Matchs;
import com.ts.dt.pool.MatchReqPool;

public class MatchReqHandle extends Thread {

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			Matchs match = MatchReqPool.get();
			long matchId = new MatchEngineImpl().execute(match.getId());

			Session session = BottleUtil.currentSession();
			// req.setMatchId(matchId);
			match.setStatus(2);
			session.beginTransaction();
			try {
				match.save();
			} catch (Exception e) {
				e.printStackTrace();
			}
			session.endTransaction();

		}

	}

}
