package com.ts.dt.stat;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Hashtable;
import java.util.List;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.dao.impl.MatchStatDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;
import com.ts.dt.po.Player;

public class DataStat {

	private Hashtable<Long, PlayerDataStat> homeTeamPlayersDataStat;
	private Hashtable<Long, PlayerDataStat> visitingTeamPlayersDataStat;

	private int homeTeam1ShootTimes = 0;
	private int visitingTeam1ShootTimes = 0;

	private int homeTeam2ShootTimes = 0;
	private int visitingTeam2ShootTimes = 0;

	private int homeTeam3ShootTimes = 0;
	private int visitingTeam3ShootTimes = 0;

	private int homeTeamOffensiveRebound = 0;
	private int visitingTeamOffensiveRebound = 0;

	private int homeTeam1DoomTimes = 0;
	private int visitingTeam1DoomTimes = 0;

	private int homeTeam2DoomTimes = 0;
	private int visitingTeam2DoomTimes = 0;

	private int homeTeam3DoomTimes = 0;
	private int visitingTeam3DoomTimes = 0;

	private int homeTeamDefensiveRebound = 0;
	private int visitingTeamDefensiveRebound = 0;

	public DataStat() {
		homeTeamPlayersDataStat = new Hashtable<Long, PlayerDataStat>();
		visitingTeamPlayersDataStat = new Hashtable<Long, PlayerDataStat>();
	}

	// 除除状态
	public void clear() {
		this.homeTeamPlayersDataStat.clear();
		this.visitingTeamPlayersDataStat.clear();
		this.homeTeam1ShootTimes = 0;
		this.visitingTeam1ShootTimes = 0;
		this.homeTeam2ShootTimes = 0;
		this.visitingTeam2ShootTimes = 0;
		this.homeTeam3ShootTimes = 0;
		this.visitingTeam3ShootTimes = 0;
		this.homeTeamOffensiveRebound = 0;
		this.visitingTeamOffensiveRebound = 0;
		this.homeTeam1DoomTimes = 0;
		this.visitingTeam1DoomTimes = 0;
		this.homeTeam2DoomTimes = 0;
		this.visitingTeam2DoomTimes = 0;
		this.homeTeam3DoomTimes = 0;
		this.visitingTeam3DoomTimes = 0;
		this.homeTeamDefensiveRebound = 0;
		this.visitingTeamDefensiveRebound = 0;
	}

	public void homeTeam1ShootTimesInc() {
		homeTeam1ShootTimes++;
	}

	public void visiting1TeamShootTimesInc() {
		visitingTeam1ShootTimes++;
	}

	public void homeTeam2ShootTimesInc() {
		homeTeam2ShootTimes++;
	}

	public void visiting2TeamShootTimesInc() {
		visitingTeam2ShootTimes++;
	}

	public void visiting3TeamShootTimesInc() {
		visitingTeam3ShootTimes++;
	}

	public void homeTeam3ShootTimesInc() {
		homeTeam3ShootTimes++;
	}

	public void homeTeamOffensiveReboundInc() {
		homeTeamOffensiveRebound++;
	}

	public void visitingTeamOffensiveReboundInc() {
		visitingTeamOffensiveRebound++;
	}

	public void homeTeamDefensiveReboundInc() {
		homeTeamDefensiveRebound++;
	}

	public void visitingTeamDefensiveReboundInc() {
		visitingTeamDefensiveRebound++;
	}

	public int getHomeTeam1ShootTimes() {
		return homeTeam1ShootTimes;
	}

	public int getVisiting1TeamShootTimes() {
		return visitingTeam1ShootTimes;
	}

	public int getHomeTeam2ShootTimes() {
		return homeTeam2ShootTimes;
	}

	public int getVisiting2TeamShootTimes() {
		return visitingTeam2ShootTimes;
	}

	public int getHomeTeam3ShootTimes() {
		return homeTeam3ShootTimes;
	}

	public int getVisiting3TeamShootTimes() {
		return visitingTeam3ShootTimes;
	}

	public int getHomeTeamOffensiveRebound() {
		return homeTeamOffensiveRebound;
	}

	public int getHomeTeamDefensiveRebound() {
		return homeTeamDefensiveRebound;
	}

	public int getVisitingTeamOffensiveRebound() {
		return visitingTeamOffensiveRebound;
	}

	public int getVisitingTeamDefensiveRebound() {
		return visitingTeamDefensiveRebound;
	}

	public void homeTeam1DoomTimesInc() {
		homeTeam1DoomTimes++;
	}

	public void visiting1TeamDoomTimesInc() {
		visitingTeam1DoomTimes++;
	}

	public void homeTeam2DoomTimesInc() {
		homeTeam2DoomTimes++;
	}

	public void visiting2TeamDoomTimesInc() {
		visitingTeam2DoomTimes++;
	}

	public void homeTeam3DoomTimesInc() {
		homeTeam3DoomTimes++;
	}

	public void visiting3TeamDoomTimesInc() {
		visitingTeam3DoomTimes++;
	}

	public String toString() {

		float homeTeam2Percent = 0;
		if (homeTeam2ShootTimes != 0) {
			homeTeam2Percent = homeTeam2DoomTimes * 100 / homeTeam2ShootTimes;
		}

		float visiting2Percent = 0;
		if (visitingTeam2ShootTimes != 0) {
			visiting2Percent = visitingTeam2DoomTimes * 100 / visitingTeam2ShootTimes;
		}

		float homeTeam3Percent = 0;
		if (homeTeam3ShootTimes != 0) {
			homeTeam3Percent = homeTeam3DoomTimes * 100 / homeTeam3ShootTimes;
		}

		float visiting3Percent = 0;
		if (visitingTeam3ShootTimes != 0) {
			visiting3Percent = visitingTeam3DoomTimes * 100 / visitingTeam3ShootTimes;
		}

		int homeTeamTotalPoint = homeTeam1DoomTimes * 1 + homeTeam2DoomTimes * 2 + homeTeam3DoomTimes * 3;
		int visitingTeamTotalPoint = visitingTeam1DoomTimes * 1 + visitingTeam2DoomTimes * 2 + visitingTeam3DoomTimes * 3;

		StringBuffer sb = new StringBuffer();
		sb.append("=======================================================================================================\n");
		sb.append("球队       二分       二分命中率       三分       三分命中率        罚球      后场篮板     前场篮板    总篮板       得分  \n");
		sb.append("-------------------------------------------------------------------------------------------------------\n");
		sb.append(createHomePlayerDataStat());
		sb.append("         " + homeTeam2DoomTimes + "\\" + homeTeam2ShootTimes);
		sb.append("      " + homeTeam2Percent + "%");
		sb.append("       " + homeTeam3DoomTimes + "\\" + homeTeam3ShootTimes);
		sb.append("       " + homeTeam3Percent + "%");
		sb.append("        " + homeTeam1DoomTimes + "\\" + homeTeam1ShootTimes);
		sb.append("         " + homeTeamDefensiveRebound);
		sb.append("         " + homeTeamOffensiveRebound);
		sb.append("         " + (homeTeamOffensiveRebound + homeTeamDefensiveRebound));
		sb.append("         " + homeTeamTotalPoint);
		sb.append("\n");
		sb.append("=======================================================================================================\n");
		sb.append(createVisitingPlayerDataStat());
		sb.append("         " + visitingTeam2DoomTimes + "\\" + visitingTeam2ShootTimes);
		sb.append("      " + visiting2Percent + "%");
		sb.append("       " + visitingTeam3DoomTimes + "\\" + visitingTeam3ShootTimes);
		sb.append("        " + visiting3Percent + "%");
		sb.append("       " + visitingTeam1DoomTimes + "\\" + visitingTeam1ShootTimes);
		sb.append("         " + visitingTeamDefensiveRebound);
		sb.append("         " + visitingTeamOffensiveRebound);
		sb.append("        " + (visitingTeamOffensiveRebound + visitingTeamDefensiveRebound));
		sb.append("         " + visitingTeamTotalPoint);
		sb.append("\n");
		sb.append("=======================================================================================================\n");
		return sb.toString();
	}

	public void playerPoint2ShootTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point2ShootTimesInc();
	}

	public void playerPoint2DoomTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point2DoomTimesInc();
	}

	public void playerPoint3ShootTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point3ShootTimesInc();
	}

	public void playerPoint3DoomTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point3DoomTimesInc();
	}

	public void playerPoint1ShootTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point1ShootTimesInc();
	}

	public void playerPoint1DoomTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.point1DoomTimesInc();
	}

	public void playerOffensiveReboundInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.offensiveReboundInc();
	}

	public void playerDefensiveReboundInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.defensiveReboundInc();
	}

	public void playerFoulTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.foulTimesInc();
	}

	public void playerAssitTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.assistTimesInc();
	}

	// 球员封盖加1
	public void playerBlockTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.blockTimesInc();
	}

	public void playerLapsusTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.lapsusTimesInc();
	}

	public void playerStealsTimesInc(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.stealsTimesInc();
	}

	public void playerIsMain(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		playerDataStat.setIsMain(true);
	}

	/**
	 * check foul out
	 * 
	 * @param player
	 * @param isHomeTeam
	 * @return
	 */
	public boolean checkFoulOut(Player player, boolean isHomeTeam) {
		PlayerDataStat playerDataStat = getPlayerDataStatByPlayer(player, isHomeTeam);
		return playerDataStat.foulOut();
	}

	public PlayerDataStat getPlayerDataStatByPlayer(Player player, boolean isHomeTeam) {
		// debug
		if (player.getId() == 0) {
			System.out.println("error");
		}
		PlayerDataStat playerDataStat = null;
		if (isHomeTeam) {
			playerDataStat = homeTeamPlayersDataStat.get(player.getId());
			if (playerDataStat == null) {
				playerDataStat = new PlayerDataStat();
				playerDataStat.setPlayer(player);
				homeTeamPlayersDataStat.put(player.getId(), playerDataStat);
			}
		} else {
			playerDataStat = visitingTeamPlayersDataStat.get(player.getId());
			if (playerDataStat == null) {
				playerDataStat = new PlayerDataStat();
				playerDataStat.setPlayer(player);
				visitingTeamPlayersDataStat.put(player.getId(), playerDataStat);
			}
		}
		return playerDataStat;
	}

	@SuppressWarnings("unchecked")
	public String createHomePlayerDataStat() {

		StringBuffer sb = new StringBuffer();
		List<PlayerDataStat> list = new ArrayList<PlayerDataStat>();
		for (long key : homeTeamPlayersDataStat.keySet()) {
			list.add(homeTeamPlayersDataStat.get(key));
		}
		Collections.sort(list, new DataStatComparator());
		for (PlayerDataStat playerDataStat : list) {
			sb.append(playerDataStat.toStirng());
		}
		return sb.toString();
	}

	@SuppressWarnings("unchecked")
	public String createVisitingPlayerDataStat() {

		StringBuffer sb = new StringBuffer();
		List<PlayerDataStat> list = new ArrayList<PlayerDataStat>();
		for (long key : visitingTeamPlayersDataStat.keySet()) {
			list.add(visitingTeamPlayersDataStat.get(key));
		}
		Collections.sort(list, new DataStatComparator());
		for (PlayerDataStat playerDataStat : list) {
			sb.append(playerDataStat.toStirng());
		}
		return sb.toString();
	}

	public void saveToDB(long matchId) throws MatchException {

		PlayerDataStat dataStat;
		List<MatchStat> matchStats = new ArrayList<MatchStat>();
		homeTeamPlayersDataStat.putAll(visitingTeamPlayersDataStat);
		for (long key : homeTeamPlayersDataStat.keySet()) {
			dataStat = homeTeamPlayersDataStat.get(key);
			Player player = dataStat.getPlayer();
			MatchStat matchStat = new MatchStat();

			matchStat.setTeamId(player.getTeamid());
			matchStat.setPlayerNo(player.getNo());
			matchStat.setNo(player.getPlayerNo());
			matchStat.setPosition(player.getPosition());
			matchStat.setName(player.getName());
			matchStat.setMatchId(matchId);
			matchStat.setPoint1DoomTimes(dataStat.getPoint1DoomTimes());
			matchStat.setPoint1ShootTimes(dataStat.getPoint1ShootTimes());
			matchStat.setPoint2DoomTimes(dataStat.getPoint2DoomTimes());
			matchStat.setPoint2ShootTimes(dataStat.getPoint2ShootTimes());
			matchStat.setPoint3DoomTimes(dataStat.getPoint3DoomTimes());
			matchStat.setPoint3ShootTimes(dataStat.getPoint3ShootTimes());
			matchStat.setOffensiveRebound(dataStat.getOffensiveRebound());
			matchStat.setDefensiveRebound(dataStat.getDefensiveRebound());
			matchStat.setFoul(dataStat.getFoulTimes());
			matchStat.setLapsus(dataStat.getLapsus());
			matchStat.setAssist(dataStat.getAssist());
			matchStat.setBlock(dataStat.getBlock());
			matchStat.setSteals(dataStat.getSteals());
			matchStat.setIsMain(dataStat.isIsMain());

			matchStats.add(matchStat);

		}

		MatchStatDao matchStatDao = new MatchStatDaoImpl();
		matchStatDao.saveMatachStats(matchStats);

	}
}
