package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class MatchStat extends Persistence {

	private long teamId;

	private long playerId;

	private long matchId;

	private int point2ShootTimes;
	private int point2DoomTimes;

	private int point3ShootTimes;
	private int point3DoomTimes;

	private int point1ShootTimes;
	private int point1DoomTimes;

	private int offensiveRebound;
	private int defensiveRebound;

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

	public long getPlayerId() {
		return playerId;
	}

	public void setPlayerId(long playerId) {
		this.playerId = playerId;
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

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

}
