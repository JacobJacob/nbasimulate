package com.ts.dt.monitor;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.EngineStatusDao;
import com.ts.dt.dao.ErrorMatchDao;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.impl.EngineStatusDaoImpl;
import com.ts.dt.dao.impl.ErrorMatchDaoImpl;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;
import com.ts.dt.po.ErrorMatch;
import com.ts.dt.po.Matchs;
import com.ts.dt.pool.MatchReqPool;
import com.ts.dt.util.HibernateUtil;
import com.ts.dt.util.Logger;

public class MatchReqHandle extends Thread {

	private String name;
	private int finishCount = 0;
	private boolean go = true;

	public MatchReqHandle(String name) {
		this.name = name;
	}

	private MatchEngine engine = new MatchEngineImpl();

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (go) {
			Matchs match = null;
			try {
				this.reportStatus("start to get task");
				match = MatchReqPool.get();
				this.reportStatus("start to execute match");
				match = engine.execute(match.getId());
				this.finishCount++;
				this.reportStatus("finish execute match");

				match.setStatus(MatchStatus.FINISH);
				MatchDao matchDao = new MatchDaoImpl();
				matchDao.save(match);

			} catch (MatchException me) {
				Logger.logToDb("error", me.getMessage());
				me.printStackTrace();
				ErrorMatch errorMatch = new ErrorMatch();
				errorMatch.setMatchId(match.getId());
				errorMatch.setRemark(me.getMessage());
				errorMatch.setType(match.getType());

				ErrorMatchDao errorMatchDao = new ErrorMatchDaoImpl();
				try {
					errorMatchDao.save(errorMatch);
				} catch (MatchException e) {
					e.printStackTrace();
				}
			} catch (Exception e) {
				e.printStackTrace();
			}

		}

	}

	private void reportStatus(String status) throws MatchException {
		EngineStatusDao engineStatusDao = new EngineStatusDaoImpl();
		EngineStatus engineStatus = null;
		boolean update = true;
		engineStatus = engineStatusDao.load(this.name);
		if (engineStatus == null) {
			engineStatus = new EngineStatus();
			engineStatus.setName(this.name);
			engineStatus.setStatus(status + "[handle total:" + this.finishCount + "]");
			update = false;
		}
		if (update) {
			engineStatusDao.update(engineStatus);
		} else {
			engineStatusDao.save(engineStatus);
		}

	}
}
