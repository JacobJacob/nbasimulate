package com.ts.dt.engine.impl;

import java.util.Date;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.TeamDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.TeamDaoImpl;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.match.Nodosity;
import com.ts.dt.po.Matchs;
import com.ts.dt.po.Team;
import com.ts.dt.util.Logger;

/*
 * 比赛引擎,实现比赛引擎接口
 */
public class MatchEngineImpl implements MatchEngine {

	/**
	 * @param homeTeamId
	 *            the home team id
	 * @param visitingTeamId
	 *            the visiting team id
	 * @param matchType
	 *            the match type
	 */
	public long execute(long matchid) {

		MatchContext context = new MatchContext();

		MatchDao matchDao = new MatchDaoImpl();
		Matchs match = matchDao.load(matchid);
		long homeTeamId = match.getHomeTeamId();
		long visitingTeamId = match.getGuestTeamId();

		TeamDao teamDao = new TeamDaoImpl();
		Team homeTeam = teamDao.load(homeTeamId);
		Team visitingTeam = teamDao.load(visitingTeamId);

		match.setStartTime(new Date());
		match.setGuestTeamId(visitingTeamId);

		match.setHomeTeamId(homeTeamId);
		matchDao.save(match);

		context.setMatchId(match.getId());
		context.setMatchType(match.getType());
		context.setHomeTeamId(homeTeamId);
		context.setVisitingTeamId(visitingTeamId);

		// TODO Auto-generated method stub
		Nodosity nodosity = new Nodosity();
		nodosity.setNodosityNo(1);

		boolean go = true;
		Logger.info("match start......");
		while (go) {
			context.put(MatchConstant.CURRT_CONT_TIME, 0L);
			int nodosityNo = nodosity.getNodosityNo();
			Logger.info("The" + nodosityNo + "nodosity start...");
			nodosity.execute(context);
			Logger.info("The" + nodosityNo + "nodosity end");
			go = nodosity.hasNextNodosity();

			nodosity = nodosity.getNextNodosity();
		}

		context.outPutMatchMessage();
		context.saveStatToDB();

		match.setPoint(context.currentScore());
		matchDao.save(match);

		// TestDataFactory.saveTestDateToDB();

		return match.getId();

	}

	public static void main(String[] args) {

		for (int i = 0; i < 1; i++) {
			new Test().start();
		}
	}

	static class Test extends Thread {

		@Override
		public void run() {
			// TODO Auto-generated method stub
			for (int i = 0; i < 1; i++) {
				
			}
		}

	}

}
