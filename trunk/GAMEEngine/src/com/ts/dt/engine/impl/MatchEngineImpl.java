package com.ts.dt.engine.impl;

import java.sql.Date;
import java.util.Iterator;
import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.constants.MatchStatus;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.YouthPlayerDaoImpl;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.match.Nodosity;
import com.ts.dt.po.MatchNotInPlayer;
import com.ts.dt.po.Matchs;
import com.ts.dt.po.Player;
import com.ts.dt.util.Logger;

/*
 * 比赛引擎,实现比赛引擎接口
 */
public class MatchEngineImpl implements MatchEngine {

	MatchContext context = new MatchContext();

	public Matchs execute(long matchid) {

		MatchDao matchDao = new MatchDaoImpl();
		Matchs match = matchDao.load(matchid);
		long homeTeamId = match.getHomeTeamId();
		long visitingTeamId = match.getGuestTeamId();

		match.setStartTime(new Date(new java.util.Date().getTime()));
		match.setGuestTeamId(visitingTeamId);

		match.setHomeTeamId(homeTeamId);
		matchDao.save(match);

		context.setMatchId(match.getId());
		context.isYouth(match.getIsYouth());
		context.setMatchType(match.getType());
		context.setHomeTeamId(homeTeamId);
		context.setVisitingTeamId(visitingTeamId);

		// TODO Auto-generated method stub
		Nodosity nodosity = new Nodosity();
		nodosity.setNodosityNo(1);

		boolean go = true;
		Logger.info("比赛开始......");
		while (go) {

			context.put(MatchConstant.CURRT_CONT_TIME, 0L);
			nodosity.execute(context);
			go = nodosity.hasNextNodosity();
			nodosity = nodosity.getNextNodosity();

		}

		context.outPutMatchMessage();
		long start = System.currentTimeMillis();
		context.saveStatToDB();
		long end = System.currentTimeMillis();
		System.out.println("save stat use times:" + (end - start));

		match.setPoint(context.currentScore());
		match.setStatus(MatchStatus.FINISH);
		matchDao.save(match);

		// 保存未上场球员统计
		this.saveNotInPlayer(context, matchid);

		// 清除状态
		context.clear();

		return match;

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
		long start = System.currentTimeMillis();
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
		long end = System.currentTimeMillis();
		System.out.println("save not in player user times:" + (end - start));
	}

}
