package com.ts.dt.stat;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.dao.impl.MatchStatDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchStat;
import com.ts.dt.po.Player;

public class PlayerDataStat {

	private Player player;
	private int point2ShootTimes;
	private int point2DoomTimes;

	private int point3ShootTimes;
	private int point3DoomTimes;

	private int point1ShootTimes;
	private int point1DoomTimes;

	private int offensiveRebound;
	private int defensiveRebound;

	private int assist;
	private int lapsus;
	private int block;
	private int steals;

	private int foulTimes;

	private boolean isMain;

	public void point2ShootTimesInc() {
		point2ShootTimes++;
	}

	public void point2DoomTimesInc() {
		point2DoomTimes++;
	}

	public void point3ShootTimesInc() {
		point3ShootTimes++;
	}

	public void point3DoomTimesInc() {
		point3DoomTimes++;
	}

	public void point1ShootTimesInc() {
		point1ShootTimes++;
	}

	public void point1DoomTimesInc() {
		point1DoomTimes++;
	}

	public void offensiveReboundInc() {
		offensiveRebound++;
	}

	public void defensiveReboundInc() {
		defensiveRebound++;
	}

	public void assistTimesInc() {
		assist++;
	}

	public void lapsusTimesInc() {
		lapsus++;
	}

	public void blockTimesInc() {
		block++;
	}

	public void foulTimesInc() {
		foulTimes++;
	}

	public void stealsTimesInc() {
		steals++;
	}

	/**
	 * check foul out
	 * 
	 * @return
	 */
	public boolean foulOut() {
		return foulTimes >= 6;
	}

	public int getTotalPoint() {
		return point1DoomTimes * 1 + point2DoomTimes * 2 + point3DoomTimes * 3;
	}

	public boolean isIsMain() {
		return isMain;
	}

	public void setIsMain(boolean isMain) {
		this.isMain = isMain;
	}

	public String toStirng() {

		StringBuffer sb = new StringBuffer();

		float point2Percent = 0;
		if (point2ShootTimes > 0) {
			point2Percent = point2DoomTimes * 100 / point2ShootTimes;
		}
		float point3Percent = 0;
		if (point3ShootTimes > 0) {
			point3Percent = point3DoomTimes * 100 / point3ShootTimes;
		}

		sb.append(player.getName() + "    " + point2DoomTimes + "\\" + point2ShootTimes);
		sb.append("      " + point2Percent + "%");
		sb.append("        " + point3DoomTimes + "\\" + point3ShootTimes);
		sb.append("        " + point3Percent + "%");
		sb.append("        " + point1DoomTimes + "\\" + point1ShootTimes);
		sb.append("         " + defensiveRebound);
		sb.append("         " + offensiveRebound);
		sb.append("         " + (offensiveRebound + defensiveRebound));
		sb.append("        " + getTotalPoint());
		sb.append("\n");
		sb.append("-------------------------------------------------------------------------------------------------------\n");

		return sb.toString();
	}

	public Player getPlayer() {
		return player;
	}

	public void setPlayer(Player player) {
		this.player = player;
	}

	public void saveToDB(long matchId) throws MatchException {

		MatchStatDao matchStatDao = new MatchStatDaoImpl();

		MatchStat matchStat = new MatchStat();
		matchStat.setDefensiveRebound(defensiveRebound);
		matchStat.setOffensiveRebound(offensiveRebound);
		matchStat.setPoint1DoomTimes(point1DoomTimes);
		matchStat.setPoint1ShootTimes(point1ShootTimes);
		matchStat.setPoint2DoomTimes(point2DoomTimes);
		matchStat.setPoint2ShootTimes(point2ShootTimes);
		matchStat.setPoint3DoomTimes(point3DoomTimes);
		matchStat.setPoint3ShootTimes(point3ShootTimes);
		matchStat.setFoul(foulTimes);
		matchStat.setAssist(assist);
		matchStat.setLapsus(lapsus);
		matchStat.setBlock(block);

		matchStat.setPlayerNo(player.getNo());
		matchStat.setTeamId(player.getTeamid());
		matchStat.setAbility(player.getAbility());
		matchStat.setAge(player.getAge());
		matchStat.setName(player.getName());
		matchStat.setNo(player.getPlayerNo());

		matchStat.setSteals(steals);
		matchStat.setIsMain(isMain);
		matchStat.setMatchId(matchId);

		matchStatDao.save(matchStat);

	}

	public int getPoint2ShootTimes() {
		return point2ShootTimes;
	}

	public void setPoint2ShootTimes(int point2ShootTimes) {
		this.point2ShootTimes = point2ShootTimes;
	}

	public int getPoint2DoomTimes() {
		return point2DoomTimes;
	}

	public void setPoint2DoomTimes(int point2DoomTimes) {
		this.point2DoomTimes = point2DoomTimes;
	}

	public int getPoint3ShootTimes() {
		return point3ShootTimes;
	}

	public void setPoint3ShootTimes(int point3ShootTimes) {
		this.point3ShootTimes = point3ShootTimes;
	}

	public int getPoint3DoomTimes() {
		return point3DoomTimes;
	}

	public void setPoint3DoomTimes(int point3DoomTimes) {
		this.point3DoomTimes = point3DoomTimes;
	}

	public int getPoint1ShootTimes() {
		return point1ShootTimes;
	}

	public void setPoint1ShootTimes(int point1ShootTimes) {
		this.point1ShootTimes = point1ShootTimes;
	}

	public int getPoint1DoomTimes() {
		return point1DoomTimes;
	}

	public void setPoint1DoomTimes(int point1DoomTimes) {
		this.point1DoomTimes = point1DoomTimes;
	}

	public int getOffensiveRebound() {
		return offensiveRebound;
	}

	public void setOffensiveRebound(int offensiveRebound) {
		this.offensiveRebound = offensiveRebound;
	}

	public int getDefensiveRebound() {
		return defensiveRebound;
	}

	public void setDefensiveRebound(int defensiveRebound) {
		this.defensiveRebound = defensiveRebound;
	}

	public int getAssist() {
		return assist;
	}

	public void setAssist(int assist) {
		this.assist = assist;
	}

	public int getLapsus() {
		return lapsus;
	}

	public void setLapsus(int lapsus) {
		this.lapsus = lapsus;
	}

	public int getBlock() {
		return block;
	}

	public void setBlock(int block) {
		this.block = block;
	}

	public int getSteals() {
		return steals;
	}

	public void setSteals(int steals) {
		this.steals = steals;
	}

	public int getFoulTimes() {
		return foulTimes;
	}

	public void setFoulTimes(int foulTimes) {
		this.foulTimes = foulTimes;
	}

	public boolean isMain() {
		return isMain;
	}

	public void setMain(boolean isMain) {
		this.isMain = isMain;
	}

}
