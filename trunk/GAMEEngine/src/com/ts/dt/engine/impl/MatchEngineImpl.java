package com.ts.dt.engine.impl;

import java.util.Date;
import java.util.Iterator;
import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.TeamDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.TeamDaoImpl;
import com.ts.dt.dao.impl.YouthPlayerDaoImpl;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.match.Nodosity;
import com.ts.dt.po.MatchNotInPlayer;
import com.ts.dt.po.Matchs;
import com.ts.dt.po.Player;
import com.ts.dt.po.Team;
import com.ts.dt.util.Logger;

/*
 * 比赛引擎,实现比赛引擎接口
 */ 
public class MatchEngineImpl implements MatchEngine {

	public long execute(long matchid) {

		MatchContext context = new MatchContext();

		MatchDao matchDao = new MatchDaoImpl();
		Matchs match = matchDao.load(matchid);
		long homeTeamId = match.getHomeTeamId();
		long visitingTeamId = match.getGuestTeamId();

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

		// 保存未上场球员统计
		this.saveNotInPlayer(context, matchid);

		return match.getId();

	}

	// 保存未上场球员资料
	private void saveNotInPlayer(MatchContext context, long matchid) {

		List<Player> home_players = null;
		List<Player> guest_players = null;

		if (context.isYouth()) {

			home_players = new YouthPlayerDaoImpl().getPlayerWithTeamId(context.getHomeTeamId());
			guest_players = new YouthPlayerDaoImpl().getPlayerWithTeamId(context.getVisitingTeamId());

		} else {
			home_players = new ProfessionPlayerDaoImpl().getPlayerWithTeamId(context.getHomeTeamId());
			guest_players = new ProfessionPlayerDaoImpl().getPlayerWithTeamId(context.getVisitingTeamId());
		}

		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		Iterator<Player> iterator = home_players.iterator();
		while (iterator.hasNext()) {
			Player player = iterator.next();
			if (context.hadOnCourt(player.getNo())) {
				continue;
			}
			MatchNotInPlayer matchNotInPlayer = new MatchNotInPlayer();
			matchNotInPlayer.setMatchId(matchid);
			matchNotInPlayer.setTeamId(context.getHomeTeamId());
			matchNotInPlayer.setAbility(player.getAbility());
			matchNotInPlayer.setPlayerNo(player.getNo());
			matchNotInPlayer.save();
		}

		iterator = guest_players.iterator();
		while (iterator.hasNext()) {
			Player player = iterator.next();
			if (context.hadOnCourt(player.getNo())) {
				continue;
			}
			MatchNotInPlayer matchNotInPlayer = new MatchNotInPlayer();
			matchNotInPlayer.setMatchId(matchid);
			matchNotInPlayer.setTeamId(context.getVisitingTeamId());
			matchNotInPlayer.setAbility(player.getAbility());
			matchNotInPlayer.setPlayerNo(player.getNo());
			matchNotInPlayer.save();
		}
		session.endTransaction();

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
