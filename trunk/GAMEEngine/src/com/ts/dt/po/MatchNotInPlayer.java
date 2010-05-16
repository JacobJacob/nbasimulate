package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class MatchNotInPlayer extends Persistence {

	private long teamId;
	private long matchId;
	private String playerNo;
	private double ability;
	private String name;
	private int no;
	private String position;

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

	public String getPlayerNo() {
		return playerNo;
	}

	public void setPlayerNo(String playerNo) {
		this.playerNo = playerNo;
	}

	public double getAbility() {
		return ability;
	}

	public void setAbility(double ability) {
		this.ability = ability;
	}

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getNo() {
		return no;
	}

	public void setNo(int no) {
		this.no = no;
	}

	public String getPosition() {
		return position;
	}

	public void setPosition(String position) {
		this.position = position;
	}

}
