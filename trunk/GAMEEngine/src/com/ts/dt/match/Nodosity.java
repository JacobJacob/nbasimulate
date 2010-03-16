package com.ts.dt.match;

import java.util.Hashtable;
import java.util.Iterator;
import java.util.Map;

import com.dt.bottle.logger.Logger;
import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.TacticalDao;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.TacticalDaoImpl;
import com.ts.dt.match.test.TestDataFactory;
import com.ts.dt.po.MatchNodosityDetail;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;

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

		controllers = new Hashtable<String, Controller>();
		context.put(MatchConstant.CURRENT_CONTROLLER_NAME, "CA");
		context.put(MatchConstant.HAS_PASS_TIMES, 0);
		context.put(MatchConstant.CURRENT_CONTROLLERS, controllers);
		// loadControllers();
		loadControllersFromDb();
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

		Logger.logger("%%%%%%%%%%%%%%The " + nodosityNo + "Start.....");

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
		Logger.logger("%%%%%%%%%%%%%%The " + nodosityNo + "End.....");
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

		MatchNodosityDetail detail = null;

		Map<String, Controller> map = context.getControllers();

		Iterator<String> iterator = map.keySet().iterator();
		while (iterator.hasNext()) {

			String key = iterator.next();
			Controller controller = map.get(key);
			detail = new MatchNodosityDetail();
			detail.setPlayerId(controller.getPlayer().getId());
			detail.setPlayerName(controller.getPlayer().getName());
			detail.setPosition(key);

			main.addDetail(detail);
		}
		main.save();
		session.endTransaction();
	}

	public void loadControllers() {

		Controller controller_ca = new Controller();
		controller_ca.setTeamFlg("A");
		controller_ca.setControllerName("CA");
		controller_ca.setPlayer(TestDataFactory.players[0]);
		controllers.put("CA", controller_ca);

		Controller controller_pfa = new Controller();
		controller_pfa.setTeamFlg("A");
		controller_pfa.setControllerName("PFA");
		controller_pfa.setPlayer(TestDataFactory.players[1]);
		controllers.put("PFA", controller_pfa);

		Controller controller_sfa = new Controller();
		controller_sfa.setTeamFlg("A");
		controller_sfa.setControllerName("SFA");
		controller_sfa.setPlayer(TestDataFactory.players[2]);
		controllers.put("SFA", controller_sfa);

		Controller controller_sga = new Controller();
		controller_sga.setTeamFlg("A");
		controller_sga.setControllerName("SGA");
		controller_sga.setPlayer(TestDataFactory.players[3]);
		controllers.put("SGA", controller_sga);

		Controller controller_pga = new Controller();
		controller_pga.setTeamFlg("A");
		controller_pga.setControllerName("PGA");
		controller_pga.setPlayer(TestDataFactory.players[4]);
		controllers.put("PGA", controller_pga);

		Controller controller_cb = new Controller();
		controller_cb.setTeamFlg("B");
		controller_cb.setControllerName("CB");
		controller_cb.setPlayer(TestDataFactory.players[5]);
		controllers.put("CB", controller_cb);

		Controller controller_pfb = new Controller();
		controller_pfb.setTeamFlg("B");
		controller_pfb.setControllerName("PFB");
		controller_pfb.setPlayer(TestDataFactory.players[6]);
		controllers.put("PFB", controller_pfb);

		Controller controller_sfb = new Controller();
		controller_sfb.setTeamFlg("B");
		controller_sfb.setControllerName("SFB");
		controller_sfb.setPlayer(TestDataFactory.players[7]);
		controllers.put("SFB", controller_sfb);

		Controller controller_sgb = new Controller();
		controller_sgb.setTeamFlg("B");
		controller_sgb.setControllerName("SGB");
		controller_sgb.setPlayer(TestDataFactory.players[8]);
		controllers.put("SGB", controller_sgb);

		Controller controller_pgb = new Controller();
		controller_pgb.setTeamFlg("B");
		controller_pgb.setControllerName("PGB");
		controller_pgb.setPlayer(TestDataFactory.players[9]);
		controllers.put("PGB", controller_pgb);
	}

	private void loadControllersFromDb() {

		TacticalDao tacticsDao = new TacticalDaoImpl();
		ProfessionPlayerDao playerDao = new ProfessionPlayerDaoImpl();

		homeTeamTactical = tacticsDao.loadTeamTactical(context.getHomeTeamId(), context.getMatchType());
		visitingTeamTactical = tacticsDao.loadTeamTactical(context.getVisitingTeamId(), context.getMatchType());

		if (homeTeamTactical == null || visitingTeamTactical == null) {
			System.out.println("ERROR");
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

		Controller controller_ca = new Controller();
		controller_ca.setTeamFlg("A");
		controller_ca.setControllerName("CA");
		controller_ca.setPlayer(playerDao.load(homeTeamTacticalDetail.getCid()));
		controllers.put("CA", controller_ca);

		Controller controller_pfa = new Controller();
		controller_pfa.setTeamFlg("A");
		controller_pfa.setControllerName("PFA");
		controller_pfa.setPlayer(playerDao.load(homeTeamTacticalDetail.getPfid()));
		controllers.put("PFA", controller_pfa);

		Controller controller_sfa = new Controller();
		controller_sfa.setTeamFlg("A");
		controller_sfa.setControllerName("SFA");
		controller_sfa.setPlayer(playerDao.load(homeTeamTacticalDetail.getSfid()));
		controllers.put("SFA", controller_sfa);

		Controller controller_sga = new Controller();
		controller_sga.setTeamFlg("A");
		controller_sga.setControllerName("SGA");
		controller_sga.setPlayer(playerDao.load(homeTeamTacticalDetail.getSgid()));
		controllers.put("SGA", controller_sga);

		Controller controller_pga = new Controller();
		controller_pga.setTeamFlg("A");
		controller_pga.setControllerName("PGA");
		controller_pga.setPlayer(playerDao.load(homeTeamTacticalDetail.getPgid()));
		controllers.put("PGA", controller_pga);

		Controller controller_cb = new Controller();
		controller_cb.setTeamFlg("B");
		controller_cb.setControllerName("CB");
		controller_cb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getCid()));
		controllers.put("CB", controller_cb);

		Controller controller_pfb = new Controller();
		controller_pfb.setTeamFlg("B");
		controller_pfb.setControllerName("PFB");
		controller_pfb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getPfid()));
		controllers.put("PFB", controller_pfb);

		Controller controller_sfb = new Controller();
		controller_sfb.setTeamFlg("B");
		controller_sfb.setControllerName("SFB");
		controller_sfb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getSfid()));
		controllers.put("SFB", controller_sfb);

		Controller controller_sgb = new Controller();
		controller_sgb.setTeamFlg("B");
		controller_sgb.setControllerName("SGB");
		controller_sgb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getSgid()));
		controllers.put("SGB", controller_sgb);

		Controller controller_pgb = new Controller();
		controller_pgb.setTeamFlg("B");
		controller_pgb.setControllerName("PGB");
		controller_pgb.setPlayer(playerDao.load(visitingTeamTacticalDetail.getPgid()));
		controllers.put("PGB", controller_pgb);

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
