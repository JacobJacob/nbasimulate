package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class MatchReq extends Persistence {

	public static final long serialVersionUID = -2805454678543428303L;

	private long homeTeamId;

	private long visitingTeamId;

	private char flag;

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

	public char getFlag() {
		return flag;
	}

	public void setFlag(char flag) {
		this.flag = flag;
	}

}
