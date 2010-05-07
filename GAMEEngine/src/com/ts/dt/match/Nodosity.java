package com.ts.dt.match;

import java.sql.Connection;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.Map;

import com.dt.bottle.db.ConnectionPool;
import com.ts.dt.constants.DefendTactical;
import com.ts.dt.constants.MatchConstant;
import com.ts.dt.constants.OffensiveTactical;
import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.PlayerDao;
import com.ts.dt.dao.TacticalDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.dao.impl.TacticalDaoImpl;
import com.ts.dt.dao.impl.YouthPlayerDaoImpl;
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
	private TeamTactical guestTeamTactical;

	TeamTacticalDetail homeTeamTacticalDetail;
	TeamTacticalDetail guestTeamTacticalDetail;

	private Hashtable<String, Controller> controllers;
	private MatchContext context;

	public void init() {

		if (context.getControllers() == null) {
			controllers = new Hashtable<String, Controller>();
			context.put(MatchConstant.CURRENT_CONTROLLERS, controllers);
		} else {
			controllers = context.getControllers();
		}

		context.put(MatchConstant.HAS_PASS_TIMES, 0);

		initDataFromDb();

		String currentControllerName = null;
		String currentDefenderName = null;

		if (nodosityNo == 1) {
			// ��һ�ڱ���,����
			currentControllerName = "CA";
			currentDefenderName = "CB";

			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SCRIMMAGE);

		} else if (nodosityNo <= 4) {
			// ������ǵ�һ��,���Ƿ���,�����A�ӵ����ҵ��Ľ�
			if (context.isHomeStart()) {
				if (nodosityNo == 4) {
					// ���ӷ���
					currentControllerName = "SGA";
					currentDefenderName = "SGB";

				} else {
					// �Ͷӷ���
					currentControllerName = "SGB";
					currentDefenderName = "SGA";
				}
			} else {
				if (nodosityNo == 4) {
					// �Ͷӷ���
					currentControllerName = "SGB";
					currentDefenderName = "SGA";
				} else {
					// ���ӷ���
					currentControllerName = "SGA";
					currentDefenderName = "SGB";
				}
			}
			context.setCurrentActionType(MatchConstant.ACTION_TYPE_SERVICE);
		} else {
			// ��ʱ��Ҳ������
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

	public void execute(MatchContext context) {

		int apoint = 0;
		int bpoint = 0;
		MatchNodosityMain main = new MatchNodosityMain();
		context.setNodosityMain(main);

		this.context = context;
		context.setSeq(nodosityNo);
		init();
		long currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		Logger.info("�� " + nodosityNo + "�ڱ�����ʼ.....");

		while (currentContinueTime < PER_NODOSITY_TIME || context.getFoulShootRemain() > 0) {

			NodosityEngine nodosityEngine = new NodosityEngine(context);
			nodosityEngine.execute();
			nodosityEngine.next();
			currentContinueTime = (Long) context.get(MatchConstant.CURRT_CONT_TIME);

		}
		Logger.info("��" + nodosityNo + "�ڱ�������.....");
		apoint = (Integer) context.get(MatchConstant.POINT_TEAM_A);
		bpoint = (Integer) context.get(MatchConstant.POINT_TEAM_B);

		long start = System.currentTimeMillis();
		logNodosityData(context);
		long end = System.currentTimeMillis();
		System.out.println("save nodosity data use time:" + (end - start));

		if (nodosityNo < 4 || (apoint == bpoint)) {
			hasNextNodosity = true;
			nextNodosity = new Nodosity();
			nextNodosity.setNodosityNo(++nodosityNo);
		} else {
			hasNextNodosity = false;
		}
	}

	private void logNodosityData(MatchContext context) {

		MatchNodosityMain main = context.getNodosityMain();
		main.setHomeOffsiveTactic(homeTeamTacticalDetail.getOffensive_tactical_type());
		main.setHomeDefendTactic(homeTeamTacticalDetail.getDefend_tactical_type());
		main.setGuestOffsiveTactic(guestTeamTacticalDetail.getOffensive_tactical_type());
		main.setGuestDefendTactic(guestTeamTacticalDetail.getDefend_tactical_type());
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
			detail.setPlayerNo(controller.getPlayer().getNo());
			detail.setPlayerName(controller.getPlayer().getName());
			detail.setPosition(key);

			main.addDetail(detail);
		}

		Connection conn = ConnectionPool.getInstance().connection();
		boolean autoCommit = true;
		try {
			autoCommit = conn.getAutoCommit();
			conn.setAutoCommit(false);
			main.save(conn);
			match.update(conn);
			conn.commit();
		} catch (Exception e) {
			if (conn != null) {
				try {
					conn.rollback();
				} catch (Exception ex) {
				}
			}
			e.printStackTrace();
		} finally {
			if (conn != null) {
				try {
					conn.setAutoCommit(autoCommit);
					conn.close();
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}

	}

	/*
	 * ÿ�ڱ�����ʼǰ��������,������,ս����
	 */
	private void initDataFromDb() {

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
			Logger.logToDb("error", "tactical not exist team id:" + context.getHomeTeamId());
		}
		if (guestTeamTactical == null) {
			Logger.logToDb("error", "tactical not exist team id:" + context.getVisitingTeamId());
		}

		long homeTeamPoint = context.getInt(MatchConstant.POINT_TEAM_A);
		long visitingTeamPoint = context.getInt(MatchConstant.POINT_TEAM_B);

		long homeTeamTacticalDetailId;
		long visitingTeamTacticalDetailId;
		// �ж��õڼ���ս��
		if ((homeTeamPoint - visitingTeamPoint) > 15) {// ����15��
			homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_7_id();
			visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_8_id();
		} else if ((homeTeamPoint - visitingTeamPoint) < -15) {// ���15��
			homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_8_id();
			visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_7_id();
		} else {
			switch (context.getSeq()) {
			case 1:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_1_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_1_id();
				break;
			case 2:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_2_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_2_id();
				break;
			case 3:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_3_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_3_id();
				break;
			case 4:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_4_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_4_id();
				break;
			case 5:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_5_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_5_id();
				break;
			case 6:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_6_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_6_id();
				break;
			default:
				homeTeamTacticalDetailId = homeTeamTactical.getTactical_detail_6_id();
				visitingTeamTacticalDetailId = guestTeamTactical.getTactical_detail_6_id();

			}
		}

		homeTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(homeTeamTacticalDetailId);
		guestTeamTacticalDetail = tacticsDao.loadTeamTacticalDetail(visitingTeamTacticalDetailId);

		if (homeTeamTacticalDetail == null || guestTeamTacticalDetail == null) {
			System.out.println("ERROR");
		}

		// ����ս��
		this.context.setHomeTeamOffensiveTactical(homeTeamTacticalDetail.getOffensive_tactical_type());
		this.context.setHomeTeamDefendTactical(homeTeamTacticalDetail.getDefend_tactical_type());
		this.context.setGuestTeamOffensiveTactical(guestTeamTacticalDetail.getOffensive_tactical_type());
		this.context.setGuestTeamDefendTactical(guestTeamTacticalDetail.getDefend_tactical_type());

		DebugUtil.debug(this.context.getHomeTeamId() + "ս��[" + OffensiveTactical.getOffensiveTacticalName(context.getHomeTeamOffensiveTactical()) + "]["
				+ DefendTactical.getDefendTacticalName(context.getHomeTeamDefendTactical()) + "]");
		DebugUtil.debug(this.context.getVisitingTeamId() + "ս��[" + OffensiveTactical.getOffensiveTacticalName(context.getGuestTeamOffensiveTactical()) + "]["
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
