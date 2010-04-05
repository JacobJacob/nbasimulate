package com.ts.dt.match;

import java.util.Hashtable;
import java.util.Iterator;
import java.util.Map;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.constants.DefendTactical;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.constants.OffensiveTactical;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.TacticalDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.TacticalDaoImpl;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.MatchNodosityTacticalDetail;
import com.ts.dt.po.Matchs;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;
import com.ts.dt.util.DebugUtil;
import com.ts.dt.util.Logger;
import com.ts.dt.util.TacticalUtil;

public class Nodosity {

	public static final long PER_NODOSITY_TIME = 7200;

	private int nodosityNo;

	private boolean hasNextNodosity = true;
	private Nodosity nextNodosity;

	private TeamTactical homeTeamTactical;
	private TeamTactical visitingTeamTactical;

	private Hashtable<String, Controller> controllers;
	private MatchContext context;

	public void init() {

		if (context.getControllers() == null) {
			controllers = new Hashtable<String, Controller>();
			context.put(MatchConstant.CURRENT_CONTROLLERS, controllers);
		} else {
			controllers = context.getControllers();
		}
		context.put(MatchConstant.CURRENT_CONTROLLER_NAME, "CA");
		context.put(MatchConstant.HAS_PASS_TIMES, 0);

		initDataFromDb();
		Controller currentController = controllers.get(context.get(MatchConstant.CURRENT_CONTROLLER_NAME));
		Controller currentDefender = controllers.get("CB");
		context.setCurrentController(currentController);
		if (nodosityNo == 1) {
			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SCRIMMAGE);
			context.setCurrentController(currentController);
			context.setCurrentDefender(currentDefender);
		}

	}

	public void execute(MatchContext context) {

		int apoint = 0;
		int bpoint = 0;
		MatchNodosityMain main = new MatchNodosityMain();
		context.setNodosityMain(main);

		this.context = context;
		context.setSeq(nodosityNo);

		init();

		long currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		Logger.info("%%%%%%%%%%%%%%The " + nodosityNo + "Start.....");

		while (currentContinueTime < PER_NODOSITY_TIME) {

			NodosityEngine nodosityEngine = new NodosityEngine(context);
			nodosityEngine.execute();
			nodosityEngine.next();

			try {
				// sleep(1000);
				// Thread.sleep(500);
			} catch (Exception e) {

			}

			currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		}
		Logger.info("%%%%%%%%%%%%%%The " + nodosityNo + "End.....");
		apoint = (Integer) context.get(MatchConstant.POINT_TEAM_A);
		bpoint = (Integer) context.get(MatchConstant.POINT_TEAM_B);

		// log nodosity data
		logNodosityData(context);

		if (nodosityNo < 4 || (apoint == bpoint)) {
			hasNextNodosity = true;
			nextNodosity = new Nodosity();
			nextNodosity.setNodosityNo(++nodosityNo);
		} else {
			hasNextNodosity = false;
			// context.outPutMatchMessage();

		}
	}

	private void logNodosityData(MatchContext context) {

		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		MatchNodosityMain main = context.getNodosityMain();
		main.setHomeTacticId(homeTeamTactical.getId());
		main.setVisitingTacticId(visitingTeamTactical.getId());
		main.setPoint(context.currentScore());
		main.setSeq(context.getSeq());
		main.setMatchId(context.getMatchId());

		MatchNodosityTacticalDetail detail = null;

		Map<String, Controller> map = context.getControllers();

		Matchs match = new MatchDaoImpl().load(context.getMatchId());
		if (context.getSeq() > 0) {
			match.setSubStatus(context.getSeq());
		}
		match.setPoint(context.currentScore());

		Iterator<String> iterator = map.keySet().iterator();
		while (iterator.hasNext()) {

			String key = iterator.next();
			Controller controller = map.get(key);
			detail = new MatchNodosityTacticalDetail();
			detail.setPlayerId(controller.getPlayer().getId());
			detail.setPlayerName(controller.getPlayer().getName());
			detail.setPosition(key);

			main.addDetail(detail);
		}
		main.save();
		match.save();
		session.endTransaction();
	}

	/*
	 * 每节比赛开始前加载数据,如阵容,战术等
	 */
	private void initDataFromDb() {

		TacticalDao tacticsDao = new TacticalDaoImpl();
		ProfessionPlayerDao playerDao = new ProfessionPlayerDaoImpl();

		int matchType = context.getMatchType();
		int tacticalType = TacticalUtil.matchType2Tactical(matchType);

		homeTeamTactical = tacticsDao.loadTeamTactical(context.getHomeTeamId(), tacticalType);
		visitingTeamTactical = tacticsDao.loadTeamTactical(context.getVisitingTeamId(), tacticalType);

		if (homeTeamTactical == null) {
			Logger.logToDb("error", "tactical not exist team id:" + context.getHomeTeamId());
		}
		if (visitingTeamTactical == null) {
			Logger.logToDb("error", "tactical not exist team id:" + context.getVisitingTeamId());
		}

		long homeTeamPoint = context.getInt(MatchConstant.POINT_TEAM_A);
		long visitingTeamPoint = context.getInt(MatchConstant.POINT_TEAM_B);

		long homeTeamTacticalDetailId;
		long visitingTeamTacticalDetailId;
		// 判断用第几节战术
		if ((homeTeamPoint - visitingTeamPoint) > 15) {// 领先15分
			homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_7_id();
			visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_8_id();
		} else if ((homeTeamPoint - visitingTeamPoint) < -15) {// 落后15分
			homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_8_id();
			visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_7_id();
		} else {
			switch (context.getSeq()) {
			case 1:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_1_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_1_id();
				break;
			case 2:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_2_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_2_id();
				break;
			case 3:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_3_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_3_id();
				break;
			case 4:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_4_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_4_id();
				break;
			case 5:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_5_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_5_id();
				break;
			case 6:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_6_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_6_id();
				break;
			default:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_6_id();
				visitingTeamTacticalDetailId = visitingTeamTactical.getTactical_detail_6_id();

			}
		}

		TeamTacticalDetail homeTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(homeTeamTacticalDetailId);
		TeamTacticalDetail visitingTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(visitingTeamTacticalDetailId);

		if (homeTeamTacticalDetail == null || visitingTeamTacticalDetail == null) {
			System.out.println("ERROR");
		}

		// 设置战术
		this.context.setHomeTeamOffensiveTactical(homeTeamTacticalDetail.getOffensive_tactical_type());
		this.context.setHomeTeamDefendTactical(homeTeamTacticalDetail.getDefend_tactical_type());
		this.context.setGuestTeamOffensiveTactical(visitingTeamTacticalDetail.getOffensive_tactical_type());
		this.context.setGuestTeamDefendTactical(visitingTeamTacticalDetail.getDefend_tactical_type());

		DebugUtil.debug(this.context.getHomeTeamId() + "战术[" + OffensiveTactical.getOffensiveTacticalName(context.getHomeTeamOffensiveTactical()) + "]["
				+ DefendTactical.getDefendTacticalName(context.getHomeTeamDefendTactical()) + "]");
		DebugUtil.debug(this.context.getVisitingTeamId() + "战术[" + OffensiveTactical.getOffensiveTacticalName(context.getGuestTeamOffensiveTactical()) + "]["
				+ DefendTactical.getDefendTacticalName(context.getGuestTeamDefendTactical()) + "]");

		Controller controller_ca = new Controller();
		controller_ca.setTeamFlg("A");
		controller_ca.setControllerName("CA");
		controller_ca.setPlayer(playerDao.load(homeTeamTacticalDetail.getCid()));
		context.putController(controller_ca);

		Controller controller_pfa = new Controller();
		controller_pfa.setTeamFlg("A");
		controller_pfa.setControllerName("PFA");
		controller_pfa.setPlayer(playerDao.load(homeTeamTacticalDetail.getPfid()));
		context.putController(controller_pfa);

		Controller controller_sfa = new Controller();
		controller_sfa.setTeamFlg("A");
		controller_sfa.setControllerName("SFA");
		controller_sfa.setPlayer(playerDao.load(homeTeamTacticalDetail.getSfid()));
		context.putController(controller_sfa);

		Controller controller_sga = new Controller();
		controller_sga.setTeamFlg("A");
		controller_sga.setControllerName("SGA");
		controller_sga.setPlayer(playerDao.load(homeTeamTacticalDetail.getSgid()));
		context.putController(controller_sga);

		Controller controller_pga = new Controller();
		controller_pga.setTeamFlg("A");
		controller_pga.setControllerName("PGA");
		controller_pga.setPlayer(playerDao.load(homeTeamTacticalDetail.getPgid()));
		context.putController(controller_pga);

		Controller controller_cb = new Controller();
		controller_cb.setTeamFlg("B");
		controller_cb.setControllerName("CB");
		controller_cb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getCid()));
		context.putController(controller_cb);

		Controller controller_pfb = new Controller();
		controller_pfb.setTeamFlg("B");
		controller_pfb.setControllerName("PFB");
		controller_pfb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getPfid()));
		context.putController(controller_pfb);

		Controller controller_sfb = new Controller();
		controller_sfb.setTeamFlg("B");
		controller_sfb.setControllerName("SFB");
		controller_sfb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getSfid()));
		context.putController(controller_sfb);

		Controller controller_sgb = new Controller();
		controller_sgb.setTeamFlg("B");
		controller_sgb.setControllerName("SGB");
		controller_sgb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getSgid()));
		context.putController(controller_sgb);

		Controller controller_pgb = new Controller();
		controller_pgb.setTeamFlg("B");
		controller_pgb.setControllerName("PGB");
		controller_pgb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getPgid()));
		context.putController(controller_pgb);

	}

	public int getNodosityNo() {
		return nodosityNo;
	}

	public void setNodosityNo(int nodosityNo) {
		this.nodosityNo = nodosityNo;
	}

	public boolean hasNextNodosity() {
		return hasNextNodosity;
	}

	public Nodosity getNextNodosity() {
		return nextNodosity;
	}

}
