package com.ts.dt.match;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.ts.dt.constants.DefendTactical;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.constants.OffensiveTactical;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.MatchNodosityMainDao;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.TacticalDao;
import com.ts.dt.dao.YouthPlayerDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.MatchNodosityMainDaoImpl;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.TacticalDaoImpl;
import com.ts.dt.dao.impl.YouthPlayerDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.helper.PowerHelper;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.MatchNodosityTacticalDetail;
import com.ts.dt.po.Matchs;
import com.ts.dt.po.Player;
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
	private TeamTactical guestTeamTactical;

	TeamTacticalDetail homeTeamTacticalDetail;
	TeamTacticalDetail guestTeamTacticalDetail;

	private Hashtable<String, Controller> controllers;
	private MatchContext context;

	public void execute(MatchContext context) throws MatchException {

		int apoint = 0;
		int bpoint = 0;
		MatchNodosityMain main = new MatchNodosityMain();
		context.setNodosityMain(main);

		this.context = context;
		context.setSeq(nodosityNo);
		this.init();
		long currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		while (currentContinueTime < PER_NODOSITY_TIME || context.getFoulShootRemain() > 0) {

			NodosityEngine nodosityEngine = new NodosityEngine(context);
			nodosityEngine.execute();
			nodosityEngine.next();
			currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		}
		apoint = (Integer) context.get(MatchConstant.POINT_TEAM_A);
		bpoint = (Integer) context.get(MatchConstant.POINT_TEAM_B);

		// 体力计算
		Iterator<Controller> iterator = context.getControllers().values().iterator();
		while (iterator.hasNext()) {
			Controller controller = iterator.next();
			Player player = controller.getPlayer();
			int cost = PowerHelper.checkPowerCost(controller, 0);
			int newMatchPower = player.getMatchPower() - cost;
			player.setMatchPower(newMatchPower >= 30 ? newMatchPower : 30);
			if (context.getMatchType() == 5) {
				int newLeaguePower = player.getLeaguePower() - cost;
				player.setLeaguePower(newLeaguePower >= 30 ? newLeaguePower : 30);
			}
		}
		this.updatePlayers(context);

		long start = System.currentTimeMillis();
		logNodosityData(context);
		long end = System.currentTimeMillis();
		Logger.info("log nodosity data use:" + (end - start));

		if (nodosityNo < 4 || (apoint == bpoint)) {
			hasNextNodosity = true;
			nextNodosity = new Nodosity();
			nextNodosity.setNodosityNo(++nodosityNo);
		} else {
			hasNextNodosity = false;
		}
	}

	/*
	 * 每节的数据初始化
	 */
	public void init() throws MatchException {

		if (context.getControllers() == null) {
			controllers = new Hashtable<String, Controller>();
			context.put(MatchConstant.CURRENT_CONTROLLERS, controllers);
		} else {
			controllers = context.getControllers();
		}

		context.put(MatchConstant.HAS_PASS_TIMES, 0);

		initDataFromDb();
		context.initTacticalPoint();

		String currentControllerName = null;
		String currentDefenderName = null;

		if (nodosityNo == 1) {
			// 第一节比赛,争球
			currentControllerName = "CA";
			currentDefenderName = "CB";

			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SCRIMMAGE);

			// 第一节的时候, 把体力值复制到比赛体力,如果是联赛,则同时复制到联赛体力上
			Iterator<Controller> iterator = context.getControllers().values().iterator();
			while (iterator.hasNext()) {
				Controller controller = iterator.next();
				Player player = controller.getPlayer();
				player.setMatchPower(player.getPower());
				if (context.getMatchType() == 5) {
					player.setLeaguePower(player.getPower());
				}
			}
			this.updatePlayers(context);

		} else if (nodosityNo <= 4) {
			// 如果不是第一节,则是发球,如果是A队得球并且第四节
			if (context.isHomeStart()) {
				if (nodosityNo == 4) {
					// 主队发球
					currentControllerName = "SGA";
					currentDefenderName = "SGB";

				} else {
					// 客队发球
					currentControllerName = "SGB";
					currentDefenderName = "SGA";
				}
			} else {
				if (nodosityNo == 4) {
					// 客队发球
					currentControllerName = "SGB";
					currentDefenderName = "SGA";
				} else {
					// 主队发球
					currentControllerName = "SGA";
					currentDefenderName = "SGB";
				}
			}
			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SERVICE);
		} else {
			// 加时赛也是争球
			currentControllerName = "CA";
			currentDefenderName = "CB";
			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SCRIMMAGE);
		}

		context.put(MatchConstant.CURRENT_CONTROLLER_NAME, currentControllerName);
		Controller currentController = controllers.get(currentControllerName);
		context.setCurrentController(currentController);
		Controller currentDefender = controllers.get(currentDefenderName);
		context.setCurrentDefender(currentDefender);

	}

	// 更新场上球员信息
	private void updatePlayers(MatchContext context) throws MatchException {
		List<Player> players = new ArrayList<Player>();
		Iterator<Controller> iterator = context.getControllers().values().iterator();
		while (iterator.hasNext()) {
			Controller controller = iterator.next();
			Player player = controller.getPlayer();
			players.add(player);
		}

		if (context.isYouth()) {
			YouthPlayerDao youthPlayerDao = new YouthPlayerDaoImpl();
			youthPlayerDao.update(players);
		} else {
			ProfessionPlayerDao professionPlayerDao = new ProfessionPlayerDaoImpl();
			professionPlayerDao.update(players);
		}

	}

	private void logNodosityData(MatchContext context) throws MatchException {

		MatchNodosityMain matchNodosityMain = context.getNodosityMain();
		matchNodosityMain.setHomeOffsiveTactic(homeTeamTacticalDetail.getOffensiveTacticalType());
		matchNodosityMain.setHomeDefendTactic(homeTeamTacticalDetail.getDefendTacticalType());
		matchNodosityMain.setGuestOffsiveTactic(guestTeamTacticalDetail.getOffensiveTacticalType());
		matchNodosityMain.setGuestDefendTactic(guestTeamTacticalDetail.getDefendTacticalType());
		matchNodosityMain.setPoint(context.currentScore());
		matchNodosityMain.setSeq(context.getSeq());
		matchNodosityMain.setMatchId(context.getMatchId());

		MatchNodosityTacticalDetail detail = null;

		Map<String, Controller> map = context.getControllers();

		Matchs match = new MatchDaoImpl().load(context.getMatchId());
		if (context.getSeq() > 0) {
			match.setSubStatus(context.getSeq());
		}
		match.setPoint(context.currentScore());
		if (context.getSeq() > 4) {
			match.setOvertime(context.getSeq() - 4);
		}

		Iterator<String> iterator = map.keySet().iterator();
		while (iterator.hasNext()) {

			String key = iterator.next();
			Controller controller = map.get(key);
			Player player = controller.getPlayer();
			detail = new MatchNodosityTacticalDetail();
			detail.setPlayerNo(player.getNo());
			detail.setPlayerName(player.getName());
			detail.setPosition(key);
			detail.setAge(player.getAge());
			detail.setAvoirdupois(player.getAvoirdupois());
			detail.setStature(player.getStature());
			detail.setNo(player.getPlayerNo());
			detail.setPower(player.getMatchPower());
			detail.setAbility(player.getAbility());

			matchNodosityMain.addDetail(detail);
		}

		MatchNodosityMainDao matchNodosityMainDao = new MatchNodosityMainDaoImpl();
		MatchDao matchDao = new MatchDaoImpl();
		long start = System.currentTimeMillis();
		matchNodosityMainDao.save(matchNodosityMain);
		long end = System.currentTimeMillis();
		Logger.info("save match nodosity main use:" + (end - start));
		matchDao.update(match);

	}

	/*
	 * 每节比赛开始前加载数据,如阵容,战术等
	 */
	private void initDataFromDb() throws MatchException {

		TacticalDao tacticsDao = new TacticalDaoImpl();
		PlayerDao playerDao = null;
		if (context.isYouth()) {
			playerDao = new YouthPlayerDaoImpl();
		} else {
			playerDao = new ProfessionPlayerDaoImpl();
		}
		int matchType = context.getMatchType();
		int tacticalType = TacticalUtil.matchType2Tactical(matchType, context.isYouth());

		homeTeamTactical = tacticsDao.loadTeamTactical(context.getHomeTeamId(), tacticalType);
		guestTeamTactical = tacticsDao.loadTeamTactical(context.getVisitingTeamId(), tacticalType);

		if (homeTeamTactical == null) {
			throw new MatchException("tactical not exist team id:" + context.getHomeTeamId());
		}
		if (guestTeamTactical == null) {
			throw new MatchException("tactical not exist team id:" + context.getVisitingTeamId());
		}

		long homeTeamPoint = context.getInt(MatchConstant.POINT_TEAM_A);
		long visitingTeamPoint = context.getInt(MatchConstant.POINT_TEAM_B);

		long homeTeamTacticalDetailId;
		long visitingTeamTacticalDetailId;
		// 判断用第几节战术
		if ((homeTeamPoint - visitingTeamPoint) > 15) {// 领先15分
			homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail7Id();
			visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail8Id();
		} else if ((homeTeamPoint - visitingTeamPoint) < -15) {// 落后15分
			homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail8Id();
			visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail7Id();
		} else {
			switch (context.getSeq()) {
			case 1:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail1Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail1Id();
				break;
			case 2:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail2Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail2Id();
				break;
			case 3:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail3Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail3Id();
				break;
			case 4:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail4Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail4Id();
				break;
			case 5:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail5Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail5Id();
				break;
			case 6:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail6Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail6Id();
				break;
			default:
				homeTeamTacticalDetailId = homeTeamTactical.getTacticalDetail6Id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTacticalDetail6Id();

			}
		}

		homeTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(homeTeamTacticalDetailId);
		guestTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(visitingTeamTacticalDetailId);

		if (homeTeamTacticalDetail == null || guestTeamTacticalDetail == null) {
			throw new MatchException("战术详细为空");
		}

		// 设置战术
		this.context.setHomeTeamOffensiveTactical(homeTeamTacticalDetail.getOffensiveTacticalType());
		this.context.setHomeTeamDefendTactical(homeTeamTacticalDetail.getDefendTacticalType());
		this.context.setGuestTeamOffensiveTactical(guestTeamTacticalDetail.getOffensiveTacticalType());
		this.context.setGuestTeamDefendTactical(guestTeamTacticalDetail.getDefendTacticalType());

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
		controller_cb.setPlayer(playerDao.load(guestTeamTacticalDetail.getCid()));
		context.putController(controller_cb);

		Controller controller_pfb = new Controller();
		controller_pfb.setTeamFlg("B");
		controller_pfb.setControllerName("PFB");
		controller_pfb.setPlayer(playerDao.load(guestTeamTacticalDetail.getPfid()));
		context.putController(controller_pfb);

		Controller controller_sfb = new Controller();
		controller_sfb.setTeamFlg("B");
		controller_sfb.setControllerName("SFB");
		controller_sfb.setPlayer(playerDao.load(guestTeamTacticalDetail.getSfid()));
		context.putController(controller_sfb);

		Controller controller_sgb = new Controller();
		controller_sgb.setTeamFlg("B");
		controller_sgb.setControllerName("SGB");
		controller_sgb.setPlayer(playerDao.load(guestTeamTacticalDetail.getSgid()));
		context.putController(controller_sgb);

		Controller controller_pgb = new Controller();
		controller_pgb.setTeamFlg("B");
		controller_pgb.setControllerName("PGB");
		controller_pgb.setPlayer(playerDao.load(guestTeamTacticalDetail.getPgid()));
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
