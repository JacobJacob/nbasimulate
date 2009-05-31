package com.ts.dt.stat;

import com.ts.dt.dao.MatchStatDao;
import com.ts.dt.dao.impl.MatchStatDaoImpl;
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

	private int foulTimes;

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

	public void foulTimesInc() {
		foulTimes++;
	}
	
	/**
	 * check foul out
	 * @return
	 */
	public boolean foulOut()
	{
	   return foulTimes >= 6;
	}

	public int getTotalPoint() {
		return point1DoomTimes * 1 + point2DoomTimes * 2 + point3DoomTimes * 3;
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

	public void saveToDB(long matchId) {

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

		matchStat.setPlayerId(player.getId());
		matchStat.setTeamId(player.getTeamid());
		matchStat.setMatchId(matchId);

		matchStatDao.save(matchStat);

	}

}
