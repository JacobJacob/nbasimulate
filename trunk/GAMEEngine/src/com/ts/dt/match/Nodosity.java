package com.ts.dt.match;

import java.util.Hashtable;

import com.dt.bottle.logger.Logger;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.dao.TacticsDao;
import com.ts.dt.dao.impl.PlayerDaoImpl;
import com.ts.dt.dao.impl.TacticsDaoImpl;
import com.ts.dt.match.test.TestDataFactory;
import com.ts.dt.po.TeamTactics;
import com.ts.dt.po.TeamTacticsDetail;

public class Nodosity {

	public static final long PER_NODOSITY_TIME = 7200;

	private int nodosityNo;

	private boolean hasNextNodosity = true;
	private Nodosity nextNodosity;

	private Hashtable<String, Controller> controllers;
	private MatchContext context;

	public void init() {

		controllers = new Hashtable<String, Controller>();
		context.put(MatchConstant.CURRENT_CONTROLLER_NAME, "CA");
		context.put(MatchConstant.HAS_PASS_TIMES, 0);
		context.put(MatchConstant.CURRENT_CONTROLLERS, controllers);
		// loadControllers();
		loadControllersFromDb();
		Controller currentController = controllers.get(context
				.get(MatchConstant.CURRENT_CONTROLLER_NAME));
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

		this.context = context;
		context.setSeq(nodosityNo);

		init();

		long currentContinueTime = (Long) context
				.get(MatchConstant.CURRT_CONT_TIME);

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

			currentContinueTime = (Long) context
					.get(MatchConstant.CURRT_CONT_TIME);

		}
		Logger.logger("%%%%%%%%%%%%%%The " + nodosityNo + "End.....");
		apoint = (Integer) context.get(MatchConstant.POINT_TEAM_A);
		bpoint = (Integer) context.get(MatchConstant.POINT_TEAM_B);

		if (nodosityNo < 4 || (apoint == bpoint)) {
			hasNextNodosity = true;
			nextNodosity = new Nodosity();
			nextNodosity.setNodosityNo(++nodosityNo);
		} else {
			hasNextNodosity = false;
			// context.outPutMatchMessage();

		}
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

		TacticsDao tacticsDao = new TacticsDaoImpl();
		PlayerDao playerDao = new PlayerDaoImpl();

		TeamTactics homeTeamTactics = tacticsDao.loadTeamTactics(context
				.getHomeTeamId(), context.getMatchType());
		TeamTactics visitingTeamTactics = tacticsDao.loadTeamTactics(context
				.getVisitingTeamId(), context.getMatchType());

		TeamTacticsDetail homeTeamTacticsDetail = tacticsDao
				.loadTeamTacticsDetail(homeTeamTactics.getId(), context
						.getSeq());
		TeamTacticsDetail visitingTeamTacticsDetail = tacticsDao
				.loadTeamTacticsDetail(visitingTeamTactics.getId(), context
						.getSeq());

		if (homeTeamTacticsDetail == null || visitingTeamTacticsDetail == null) {
			System.out.println("ERROR");
		}

		Controller controller_ca = new Controller();
		controller_ca.setTeamFlg("A");
		controller_ca.setControllerName("CA");
		controller_ca.setPlayer(playerDao.load(homeTeamTacticsDetail.getCid()));
		controllers.put("CA", controller_ca);

		Controller controller_pfa = new Controller();
		controller_pfa.setTeamFlg("A");
		controller_pfa.setControllerName("PFA");
		controller_pfa.setPlayer(playerDao
				.load(homeTeamTacticsDetail.getPfid()));
		controllers.put("PFA", controller_pfa);

		Controller controller_sfa = new Controller();
		controller_sfa.setTeamFlg("A");
		controller_sfa.setControllerName("SFA");
		controller_sfa.setPlayer(playerDao
				.load(homeTeamTacticsDetail.getSfid()));
		controllers.put("SFA", controller_sfa);

		Controller controller_sga = new Controller();
		controller_sga.setTeamFlg("A");
		controller_sga.setControllerName("SGA");
		controller_sga.setPlayer(playerDao
				.load(homeTeamTacticsDetail.getSgid()));
		controllers.put("SGA", controller_sga);

		Controller controller_pga = new Controller();
		controller_pga.setTeamFlg("A");
		controller_pga.setControllerName("PGA");
		controller_pga.setPlayer(playerDao
				.load(homeTeamTacticsDetail.getPgid()));
		controllers.put("PGA", controller_pga);

		Controller controller_cb = new Controller();
		controller_cb.setTeamFlg("B");
		controller_cb.setControllerName("CB");
		controller_cb.setPlayer(playerDao.load(visitingTeamTacticsDetail
				.getCid()));
		controllers.put("CB", controller_cb);

		Controller controller_pfb = new Controller();
		controller_pfb.setTeamFlg("B");
		controller_pfb.setControllerName("PFB");
		controller_pfb.setPlayer(playerDao.load(visitingTeamTacticsDetail
				.getPfid()));
		controllers.put("PFB", controller_pfb);

		Controller controller_sfb = new Controller();
		controller_sfb.setTeamFlg("B");
		controller_sfb.setControllerName("SFB");
		controller_sfb.setPlayer(playerDao.load(visitingTeamTacticsDetail
				.getSfid()));
		controllers.put("SFB", controller_sfb);

		Controller controller_sgb = new Controller();
		controller_sgb.setTeamFlg("B");
		controller_sgb.setControllerName("SGB");
		controller_sgb.setPlayer(playerDao.load(visitingTeamTacticsDetail
				.getSgid()));
		controllers.put("SGB", controller_sgb);

		Controller controller_pgb = new Controller();
		controller_pgb.setTeamFlg("B");
		controller_pgb.setControllerName("PGB");
		controller_pgb.setPlayer(playerDao.load(visitingTeamTacticsDetail
				.getPgid()));
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
