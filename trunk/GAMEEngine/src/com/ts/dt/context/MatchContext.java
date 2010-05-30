package com.ts.dt.context;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.Controller;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.helper.ReboundHelper;
import com.ts.dt.match.helper.TacticalHelper;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.Player;
import com.ts.dt.stat.DataStat;

public class MatchContext {

	private long matchId;

	private Hashtable<String, Object> data;

	private DataStat dataStat;

	private boolean isNewLine = true; // 是不是新的一轮进攻

	private boolean notStick = false; // 三不沾

	private List<String> onCourtPlayers = new ArrayList<String>(); // 当前在场上的球员
	private List<String> hadOnCourtPlayers = new ArrayList<String>(); // 当前比赛已经上过场的球员

	private long homeTeamId; // 主队id
	private long visitingTeamId; // 客队id

	private int matchType; // 比赛类型
	private int seq; // 节数

	private int homeTeamOffensiveTactical = -1; // 主队进攻战术
	private int homeTeamDefendTactical = -1; // 主队防守战术
	private int guestTeamOffensiveTactical = -1; // 客队进攻战术
	private int guestTeamDefendTactical = -1; // 客队防守战术

	private int homeTeamOffensiveTacticalPoint = -1;// 主队进攻战术克制分值, 以100分算
	private int guestTeamOffensiveTacticalPoint = -1;// 克队进攻战术克制分值, 以100分算

	private int currentOffensiveCostTime = 0; // 当前进攻所使用了的时间,以毫秒算

	private int currentTeam = MatchConstant.CURRENT_TEAM_A; // 0代表主队控球

	private int previousActionAffect = 0;// 上一动作对本次动作成功与否的影响

	private int tacticsAffect = 0; // 战术对动作成功的影响

	private boolean isOutside = false; // 是否出界

	private boolean isFoul = false; // 是否犯规

	private boolean isBlock = false; // 是否封盖

	private boolean isAssist = false;// 是否助攻

	private boolean isHomeStart = false; // 是否是主队获得第一次争球

	private int foulShootRemain = -1;
	private int foulShootType;

	private boolean justStart = true;

	private boolean isOffensiveRebound = false;

	private boolean isDefensiveRebound = false;

	private int totalHomeBackboard = 0;// 主队整体篮板能力

	private int totalGuestBackboard = 0;// 客队整体篮板能力

	private long currentSeq = 0;

	private boolean isYouth = false; // 是否是年轻球队

	private MatchNodosityMain nodosityMain;

	public MatchContext() {
		data = new Hashtable<String, Object>();
		init();
	}

	// 清除所有数据,重用,不要每次都开一个
	public void clear() {

		this.matchId = 0;
		this.data.clear();
		this.dataStat.clear();
		this.onCourtPlayers.clear();
		this.hadOnCourtPlayers.clear();
		this.homeTeamId = 0;
		this.visitingTeamId = 0;
		this.matchType = 0;
		this.seq = 0;
		this.homeTeamOffensiveTactical = -1;
		this.homeTeamDefendTactical = -1;
		this.guestTeamOffensiveTactical = -1;
		this.guestTeamDefendTactical = -1;
		this.currentOffensiveCostTime = 0;
		this.currentTeam = MatchConstant.CURRENT_TEAM_A;
		this.previousActionAffect = 0;
		this.tacticsAffect = 0;
		this.isOutside = false;
		this.isFoul = false;
		this.isBlock = false;
		this.isAssist = false;
		this.foulShootRemain = -1;
		this.foulShootType = 0;
		this.justStart = true;
		this.isOffensiveRebound = false;
		this.isDefensiveRebound = false;
		this.totalHomeBackboard = 0;
		this.totalGuestBackboard = 0;
		this.currentSeq = 0;
		this.isYouth = false;
		this.init();
	}

	private void init() {

		put(MatchConstant.POINT_TEAM_A, 0);
		put(MatchConstant.POINT_TEAM_B, 0);
		put(MatchConstant.CURRT_CONT_TIME, 0L);
		dataStat = new DataStat();
	}

	public void put(String key, Object value) {
		data.put(key, value);
	}

	// 设置场上控制者
	@SuppressWarnings("unchecked")
	public void putController(Controller controller) {

		// 设置场上球员时,把球员加到已经上过场的名单中, 当前上场球员列表中
		String controllerName = controller.getControllerName();

		Hashtable<String, Controller> controllers = (Hashtable<String, Controller>) this.get(MatchConstant.CURRENT_CONTROLLERS);

		String playerNo = controller.getPlayer().getNo();
		if (!this.hadOnCourt(playerNo)) {
			this.addHadOnCourtPlayer(playerNo);
		}
		// 如果该位置旧的球员存在,则清掉状态
		Controller old_controller = controllers.get(controllerName);
		if (old_controller != null) {
			if (this.onCourt(playerNo)) {
				this.onCourtPlayers.remove(playerNo);
			}
			// 把旧的能力减掉
			int reboundPower = ReboundHelper.checkReboundPower(old_controller.getPlayer());
			// 把整队篮板能力加起来
			if (old_controller.getTeamFlg().equals("A")) {
				this.totalHomeBackboard -= reboundPower;
			} else {
				this.totalGuestBackboard -= reboundPower;
			}
		}
		if (!this.onCourt(playerNo)) {
			int reboundPower = ReboundHelper.checkReboundPower(controller.getPlayer());
			// 把整队篮板能力加起来
			if (controller.getTeamFlg().equals("A")) {
				this.totalHomeBackboard += reboundPower;
			} else {
				this.totalGuestBackboard += reboundPower;
			}

			this.addOnCourtPlayer(playerNo);
		}
		// 如果是第一节,则设置该球员为主力
		if (this.seq == 1) {
			this.playerIsMain(controller.getPlayer(), controllerName.endsWith("A"));
		}

		controllers.put(controllerName, controller);

	}

	public Object get(String key) {
		return data.get(key);
	}

	public Integer getInt(String key) {
		return (Integer) get(key);
	}

	@SuppressWarnings("unchecked")
	public Hashtable<String, Controller> getControllers() {
		return (Hashtable<String, Controller>) get(MatchConstant.CURRENT_CONTROLLERS);
	}

	public void setNextDefender(Controller nextDefender) {

		put(MatchConstant.NEXT_DEFENDER, nextDefender);
	}

	public Controller getNextDefender() {

		return (Controller) get(MatchConstant.NEXT_DEFENDER);
	}

	public void setCurrentDefender(Controller controller) {
		put(MatchConstant.CURRENT_DEFENDER, controller);
	}

	public Controller getCurrentDefender() {
		return (Controller) get(MatchConstant.CURRENT_DEFENDER);
	}

	public void setFoutOutController(Controller controller) {
		put(MatchConstant.FOUL_OUT_CONTROLLER, controller);
	}

	public Controller getFoulOutController() {
		return (Controller) get(MatchConstant.FOUL_OUT_CONTROLLER);
	}

	public void setCurrentController(Controller controller) {
		put(MatchConstant.CURRENT_CONTROLLER_NAME, controller.getControllerName());
		put(MatchConstant.CURRENT_CONTROLLER, controller);
	}

	public Controller getCurrentController() {
		return (Controller) get(MatchConstant.CURRENT_CONTROLLER);
	}

	public void setPreviousController(Controller controller) {
		put(MatchConstant.PREVIOUS_CONTROLLER, controller);
	}

	public Controller getPreviousController() {
		return (Controller) get(MatchConstant.PREVIOUS_CONTROLLER);
	}

	public void setNextController(Controller controller) {
		put(MatchConstant.NEXT_CONTROLLER, controller);
	}

	public Controller getNextController() {
		return (Controller) get(MatchConstant.NEXT_CONTROLLER);
	}

	public Action getNextAction() {
		return (Action) get(MatchConstant.NEXT_ACTION);
	}

	public void removeNextAction() {
		data.remove(MatchConstant.NEXT_ACTION);
	}

	public void setNextAction(Action action) {
		put(MatchConstant.NEXT_ACTION, action);
	}

	@SuppressWarnings("unchecked")
	public Player getCurrentControllerPlayer() {
		String currtContrNm = (String) get(MatchConstant.CURRENT_CONTROLLER_NAME);
		Hashtable<String, Controller> controllers = (Hashtable<String, Controller>) get(MatchConstant.CURRENT_CONTROLLERS);
		return ((Controller) controllers.get(currtContrNm)).getPlayer();
	}

	public void pointInc(int addPoint, boolean isHomeTeam) {

		String teamName = null;
		if (isHomeTeam) {
			teamName = MatchConstant.POINT_TEAM_A;
		} else {
			teamName = MatchConstant.POINT_TEAM_B;
		}
		int point = getInt(teamName);
		point += addPoint;
		put(teamName, point);
	}

	public String currentScore() {
		return "[" + getInt(MatchConstant.POINT_TEAM_A) + " : " + getInt(MatchConstant.POINT_TEAM_B) + "]";
	}

	public boolean isHomeTeamController(String controllerName) {
		return controllerName.endsWith("A");
	}

	public boolean isHomeTeam() {
		return isHomeTeamController(getCurrentController().getControllerName());
	}

	/**
	 * setting the ball right in which team
	 */
	public void setBallRight() {
		Controller controller = getCurrentController();
		if (controller.getControllerName().endsWith("A")) {
			currentTeam = MatchConstant.CURRENT_TEAM_A;
		} else {
			currentTeam = MatchConstant.CURRENT_TEAM_B;
		}

	}

	/**
	 * 
	 */
	public void exchangeBallRight() {
		if (currentTeam == MatchConstant.CURRENT_TEAM_A) {
			currentTeam = MatchConstant.CURRENT_TEAM_B;
		} else {
			currentTeam = MatchConstant.CURRENT_TEAM_A;
		}
	}

	public int getCurrentTeam() {
		return currentTeam;
	}

	/**
	 * get current action
	 */
	public Action getCurrentAction() {
		return (Action) get(MatchConstant.CURRENT_ACTION);
	}

	/**
	 * set current action
	 * 
	 * @param action
	 */
	public void setCurrentAction(Action action) {
		put(MatchConstant.CURRENT_ACTION, action);
	}

	public void setShootActionResult(String result) {
		put(MatchConstant.SHOOT_ACTION_RESULT, result);
	}

	public void setPassActionResult(String result) {
		put(MatchConstant.PASS_ACTION_RESULT, result);
	}

	public String getPassActionResult() {
		return (String) get(MatchConstant.PASS_ACTION_RESULT);
	}

	public boolean isSuccess() {
		Object shootResult = get(MatchConstant.SHOOT_ACTION_RESULT);
		return MatchConstant.RESULT_SUCCESS.equals(shootResult);
	}

	public String getShootActionResult() {
		return (String) get(MatchConstant.SHOOT_ACTION_RESULT);
	}

	public long getContinueTime() {
		return (Long) get(MatchConstant.CURRT_CONT_TIME);
	}

	public int getPreviousActionAffect() {
		return previousActionAffect;
	}

	public void setPreviousActionAffect(int previousActionAffect) {
		this.previousActionAffect = previousActionAffect;
	}

	public int getTacticsAffect() {
		return tacticsAffect;
	}

	public void setTacticsAffect(int tacticsAffect) {
		this.tacticsAffect = tacticsAffect;
	}

	public boolean isOutside() {
		return isOutside;
	}

	public void setOutside(boolean isOutside) {
		this.isOutside = isOutside;
	}

	public boolean isFoul() {
		return isFoul;
	}

	public void setFoul(boolean isFoul) {
		this.isFoul = isFoul;
	}

	public boolean isOffensiveRebound() {
		return isOffensiveRebound;
	}

	public void setOffensiveRebound(boolean isOffensiveRebound) {
		this.isOffensiveRebound = isOffensiveRebound;
	}

	public boolean isDefensiveRebound() {
		return isDefensiveRebound;
	}

	public void setDefensiveRebound(boolean isDefensiveRebound) {
		this.isDefensiveRebound = isDefensiveRebound;
	}

	public void setNextActionType(int actionType) {
		put(MatchConstant.NEXT_ACTION_TYPE, actionType);
	}

	public int getNextActionType() {
		Object obj = get(MatchConstant.NEXT_ACTION_TYPE);
		if (obj != null) {
			return (Integer) obj;
		}
		return MatchConstant.NULL_INTEGER;
	}

	// set previous action type
	public void setPreviousActionType(int actionType) {
		put(MatchConstant.PREVIOUS_ACTION_TYPE, actionType);
	}

	// get previous action type
	public int getPreviousActionType() {
		Object obj = get(MatchConstant.PREVIOUS_ACTION_TYPE);
		if (obj != null) {
			return (Integer) obj;
		}
		return MatchConstant.NULL_INTEGER;
	}

	public void setCurrentActionType(int actionType) {
		put(MatchConstant.CURRENT_ACTION_TYPE, actionType);
	}

	public int getCurrentActionType() {
		Object obj = get(MatchConstant.CURRENT_ACTION_TYPE);
		if (obj != null) {
			return (Integer) obj;
		}
		return MatchConstant.NULL_INTEGER;
	}

	public void shootTimesInc(int point, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			if (isHomeTeam) {
				dataStat.homeTeam1ShootTimesInc();
			} else {
				dataStat.visiting1TeamShootTimesInc();
			}
		} else if (point == MatchConstant.INC_TWO_POINT) {
			if (isHomeTeam) {
				dataStat.homeTeam2ShootTimesInc();
			} else {
				dataStat.visiting2TeamShootTimesInc();
			}
		} else {
			if (isHomeTeam) {
				dataStat.homeTeam3ShootTimesInc();
			} else {
				dataStat.visiting3TeamShootTimesInc();
			}
		}

	}

	// 球员投篮次断加1
	public void playerShootTimesInc(int point, Player player, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			dataStat.playerPoint1ShootTimesInc(player, isHomeTeam);
		} else if (point == MatchConstant.INC_TWO_POINT) {
			dataStat.playerPoint2ShootTimesInc(player, isHomeTeam);
		} else {
			dataStat.playerPoint3ShootTimesInc(player, isHomeTeam);
		}

	}

	// 球员队命中加1
	public void doomTimesInc(int point, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			if (isHomeTeam) {
				dataStat.homeTeam1DoomTimesInc();
			} else {
				dataStat.visiting1TeamDoomTimesInc();
			}
		} else if (point == MatchConstant.INC_TWO_POINT) {
			if (isHomeTeam) {
				dataStat.homeTeam2DoomTimesInc();
			} else {
				dataStat.visiting2TeamDoomTimesInc();
			}
		} else {
			if (isHomeTeam) {
				dataStat.homeTeam3DoomTimesInc();
			} else {
				dataStat.visiting3TeamDoomTimesInc();
			}
		}

	}

	// 球员命中加1
	public void playerDoomTimesInc(int point, Player player, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			dataStat.playerPoint1DoomTimesInc(player, isHomeTeam);
		} else if (point == MatchConstant.INC_TWO_POINT) {
			dataStat.playerPoint2DoomTimesInc(player, isHomeTeam);
		} else {
			dataStat.playerPoint3DoomTimesInc(player, isHomeTeam);
		}

	}

	// 球员助攻加1
	public void playerAssitTimesInc(Player player, boolean isHomeTeam) {
		dataStat.playerAssitTimesInc(player, isHomeTeam);
	}

	// 球员封盖加1
	public void playerBlockTimesInc(Player player, boolean isHomeTeam) {
		dataStat.playerBlockTimesInc(player, isHomeTeam);
	}

	// 球员抢断加1
	public void playerStealsTimesInc(Player player, boolean isHomeTeam) {
		dataStat.playerStealsTimesInc(player, isHomeTeam);
	}

	// 球员失误加1
	public void playerLapsusTimesInc(Player player, boolean isHomeTeam) {
		dataStat.playerLapsusTimesInc(player, isHomeTeam);
	}

	public void addOffensiveRebound(boolean isHomeTeam) {

		if (isHomeTeam) {
			dataStat.homeTeamOffensiveReboundInc();
		} else {
			dataStat.visitingTeamOffensiveReboundInc();
		}

	}

	public void playerAddOffensiveRebound(Player player, boolean isHomeTeam) {

		dataStat.playerOffensiveReboundInc(player, isHomeTeam);

	}

	public void playerAddFoulTimes(Player player, boolean isHomeTeam) {
		dataStat.playerFoulTimesInc(player, isHomeTeam);
	}

	/**
	 * check foul out
	 * 
	 * @param player
	 * @param isHomeTeam
	 * @return
	 */
	public boolean checkFoulOut(Player player, boolean isHomeTeam) {
		return dataStat.checkFoulOut(player, isHomeTeam);
	}

	public void addDefensiveRebound(boolean isHomeTeam) {

		if (isHomeTeam) {
			dataStat.homeTeamDefensiveReboundInc();
		} else {
			dataStat.visitingTeamDefensiveReboundInc();
		}

	}

	public void playerAddDefensiveRebound(Player player, boolean isHomeTeam) {
		dataStat.playerDefensiveReboundInc(player, isHomeTeam);
	}

	// 设置球员是否为主力
	public void playerIsMain(Player player, boolean isHomeTeam) {
		dataStat.playerIsMain(player, isHomeTeam);
	}

	public void outPutMatchMessage() {
		// Logger.log(dataStat.toString());
	}

	public int getFoulShootRemain() {
		return foulShootRemain;
	}

	public void setFoulShootRemain(int foulShootRemain) {
		this.foulShootRemain = foulShootRemain;
	}

	public boolean isJustStart() {
		return justStart;
	}

	public void setJustStart(boolean justStart) {
		this.justStart = justStart;
	}

	public void setScrimmageResult(String result) {
		put(MatchConstant.SCRIMMAGE_ACTION_RESULT, result);
	}

	public String getScrimmageResult() {
		return (String) get(MatchConstant.SCRIMMAGE_ACTION_RESULT);
	}

	public Controller getControllerWithIndx(int indx) throws ArrayIndexOutOfBoundsException {

		Hashtable<String, Controller> controllers = this.getControllers();
		if (indx > 10 || indx < 1) {
			throw new ArrayIndexOutOfBoundsException();
		}
		switch (indx) {
		case MatchConstant.PG_A:
			return controllers.get("PGA");
		case MatchConstant.SG_A:
			return controllers.get("SGA");
		case MatchConstant.SF_A:
			return controllers.get("SFA");
		case MatchConstant.PF_A:
			return controllers.get("PFA");
		case MatchConstant.C_A:
			return controllers.get("CA");
		case MatchConstant.PG_B:
			return controllers.get("PGB");
		case MatchConstant.SG_B:
			return controllers.get("SGB");
		case MatchConstant.SF_B:
			return controllers.get("SFB");
		case MatchConstant.PF_B:
			return controllers.get("PFB");
		case MatchConstant.C_B:
			return controllers.get("CB");
		default:
			return null;
		}

	}

	public void foulShootRemainDec() {
		foulShootRemain--;
	}

	public long getCurrentOffensiveCostTime() {
		return currentOffensiveCostTime;
	}

	public void currentOffensiveCostTimeAdd(long incTime) {
		currentOffensiveCostTime += incTime;
	}

	public boolean currentOffensiveTimeOut() {
		return currentOffensiveCostTime >= 240;
	}

	public void currentOffensiveCostTimeReset() {
		currentOffensiveCostTime = 0;
	}

	public void nodosityCostTimeAdd(long incTime) {
		long costTime = incTime;
		long currentContinueTime = (Long) get(MatchConstant.CURRT_CONT_TIME);
		currentContinueTime += costTime;
		put(MatchConstant.CURRT_CONT_TIME, currentContinueTime);
	}

	public int getFoulShootType() {
		return foulShootType;
	}

	public void setFoulShootType(int foulShootType) {
		this.foulShootType = foulShootType;
	}

	public long getCurrentSeq() {
		long seq = currentSeq;
		currentSeq++;
		return seq;
	}

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

	public void saveStatToDB() throws MatchException {
		dataStat.saveToDB(matchId);
	}

	public long getHomeTeamId() {
		return homeTeamId;
	}

	public void setHomeTeamId(long homeTeamId) {
		this.homeTeamId = homeTeamId;
	}

	public long getVisitingTeamId() {
		return visitingTeamId;
	}

	public void setVisitingTeamId(long visitingTeamId) {
		this.visitingTeamId = visitingTeamId;
	}

	public int getMatchType() {
		return matchType;
	}

	public void setMatchType(int matchType) {
		this.matchType = matchType;
	}

	public int getSeq() {
		return seq;
	}

	public void setSeq(int seq) {
		this.seq = seq;
	}

	public boolean isBlock() {
		return isBlock;
	}

	public boolean isAssist() {
		return isAssist;
	}

	public void setAssist(boolean isAssist) {
		this.isAssist = isAssist;
	}

	public void setBlock(boolean isBlock) {
		this.isBlock = isBlock;
	}

	public MatchNodosityMain getNodosityMain() {
		return nodosityMain;
	}

	public void setNodosityMain(MatchNodosityMain nodosityMain) {
		this.nodosityMain = nodosityMain;
	}

	// 判断是否曾经上过场
	public boolean hadOnCourt(String playerNo) {
		if (hadOnCourtPlayers.contains(playerNo)) {
			return true;
		}
		return false;
	}

	// 添加到已经上过场球员列表
	public void addHadOnCourtPlayer(String playerNo) {
		if (hadOnCourtPlayers.contains(playerNo)) {
			return;
		}
		hadOnCourtPlayers.add(playerNo);
	}

	public boolean isYouth() {
		return this.isYouth;
	}

	public void isYouth(boolean isYouth) {
		this.isYouth = isYouth;
	}

	// 判断当前是否在场
	public boolean onCourt(String playerNo) {
		if (onCourtPlayers.contains(playerNo)) {
			return true;
		}
		return false;
	}

	// 添加一个球员到当前场上球员列表
	public void addOnCourtPlayer(String playerNo) {
		if (onCourtPlayers.contains(playerNo)) {
			return;
		}
		onCourtPlayers.add(playerNo);
	}

	// 置空场上球员列表
	public void clearAllOnCourtPlayer() {
		onCourtPlayers.clear();
	}

	public int getTotalHomeBackboard() {
		return totalHomeBackboard;
	}

	public int getTotalGuestBackboard() {
		return totalGuestBackboard;
	}

	public int getHomeTeamOffensiveTactical() {
		return homeTeamOffensiveTactical;
	}

	public void setHomeTeamOffensiveTactical(int homeTeamOffensiveTactical) {
		this.homeTeamOffensiveTactical = homeTeamOffensiveTactical;
	}

	public int getHomeTeamDefendTactical() {
		return homeTeamDefendTactical;
	}

	public void setHomeTeamDefendTactical(int homeTeamDefendTactical) {
		this.homeTeamDefendTactical = homeTeamDefendTactical;
	}

	public int getGuestTeamOffensiveTactical() {
		return guestTeamOffensiveTactical;
	}

	public void setGuestTeamOffensiveTactical(int guestTeamOffensiveTactical) {
		this.guestTeamOffensiveTactical = guestTeamOffensiveTactical;
	}

	public int getGuestTeamDefendTactical() {
		return guestTeamDefendTactical;
	}

	public boolean isNotStick() {
		return notStick;
	}

	public void setNotStick(boolean notStick) {
		this.notStick = notStick;
	}

	public void setGuestTeamDefendTactical(int guestTeamDefendTactical) {
		this.guestTeamDefendTactical = guestTeamDefendTactical;
	}

	public boolean isNewLine() {
		return isNewLine;
	}

	public void setNewLine(boolean isNewLine) {
		this.isNewLine = isNewLine;
	}

	public boolean isHomeStart() {
		return isHomeStart;
	}

	public void setHomeStart(boolean isHomeStart) {
		this.isHomeStart = isHomeStart;
	}

	public int getHomeTeamOffensiveTacticalPoint() {
		return homeTeamOffensiveTacticalPoint;
	}

	public int getGuestTeamOffensiveTacticalPoint() {
		return guestTeamOffensiveTacticalPoint;
	}

	public void initTacticalPoint() {
		this.homeTeamOffensiveTacticalPoint = TacticalHelper.checkOffensivePoint(this.homeTeamOffensiveTactical, this.guestTeamDefendTactical);
		this.guestTeamOffensiveTacticalPoint = TacticalHelper.checkOffensivePoint(this.guestTeamOffensiveTactical, this.homeTeamDefendTactical);
	}

}
