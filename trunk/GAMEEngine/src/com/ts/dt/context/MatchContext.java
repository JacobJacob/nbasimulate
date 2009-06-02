package com.ts.dt.context;

import java.util.Hashtable;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.match.Controller;
import com.ts.dt.match.action.Action;
import com.ts.dt.po.Player;
import com.ts.dt.stat.DataStat;

public class MatchContext {

	private long matchId;

	private Hashtable<String, Object> data;

	private DataStat dataStat;

	private long homeTeamId;
	private long visitingTeamId;

	private String matchType; // match type
	private int seq;

	private int currentOffensiveCostTime = 0; // 当前进攻所使用了的时间,以毫秒算

	private int currentTeam = MatchConstant.CURRENT_TEAM_A; // 0代表主队控球

	private int previousActionAffect = 0;// 上一动作对本次动作成功与否的影响

	private int tacticsAffect = 0; // 战术对动作成功的影响

	private boolean isOutside = false; // whether outside

	private boolean isFoul = false; // whether foul

	private boolean isBlock = false; // whether block;

	private int foulShootRemain = -1;
	private int foulShootType;

	private boolean justStart = true;

	private boolean isOffensiveRebound = false; //

	private boolean isDefensiveRebound = false;

	private int totalFBackboardA = 0;//

	private int totalBBackboardA = 0;//

	private int totalFBackboardB = 0;//

	private int totalBBackboardB = 0;

	private long currentSeq = 0;

	public MatchContext() {
		data = new Hashtable<String, Object>();
		init();
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

	public void setCurrentController(Controller controller) {
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
	 * exchange the ball right
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

	public int getTotalFBackboardA() {
		return totalFBackboardA;
	}

	public void setTotalFBackboardA(int totalFBackboardA) {
		this.totalFBackboardA = totalFBackboardA;
	}

	public int getTotalBBackboardA() {
		return totalBBackboardA;
	}

	public void setTotalBBackboardA(int totalBBackboardA) {
		this.totalBBackboardA = totalBBackboardA;
	}

	public int getTotalFBackboardB() {
		return totalFBackboardB;
	}

	public void setTotalFBackboardB(int totalFBackboardB) {
		this.totalFBackboardB = totalFBackboardB;
	}

	public int getTotalBBackboardB() {
		return totalBBackboardB;
	}

	public void setTotalBBackboardB(int totalBBackboardB) {
		this.totalBBackboardB = totalBBackboardB;
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

	public void playerShootTimesInc(int point, Player player, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			dataStat.playerPoint1ShootTimesInc(player, isHomeTeam);
		} else if (point == MatchConstant.INC_TWO_POINT) {
			dataStat.playerPoint2ShootTimesInc(player, isHomeTeam);
		} else {
			dataStat.playerPoint3ShootTimesInc(player, isHomeTeam);
		}

	}

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

	public void playerDoomTimesInc(int point, Player player, boolean isHomeTeam) {

		if (point == MatchConstant.INC_ONE_POINT) {
			dataStat.playerPoint1DoomTimesInc(player, isHomeTeam);
		} else if (point == MatchConstant.INC_TWO_POINT) {
			dataStat.playerPoint2DoomTimesInc(player, isHomeTeam);
		} else {
			dataStat.playerPoint3DoomTimesInc(player, isHomeTeam);
		}

	}

	public void playerAssitTimesInc(Player player, boolean isHomeTeam) {
		dataStat.playerAssitTimesInc(player, isHomeTeam);
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

	public void saveStatToDB() {
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

	public String getMatchType() {
		return matchType;
	}

	public void setMatchType(String matchType) {
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

	public void setBlock(boolean isBlock) {
		this.isBlock = isBlock;
	}

}
