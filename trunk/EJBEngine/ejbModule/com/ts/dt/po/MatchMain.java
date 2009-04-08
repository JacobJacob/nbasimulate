package com.ts.dt.po;

import java.io.Serializable;
import java.util.Date;

import com.dt.bottle.persistence.Persistence;

public class MatchMain extends Persistence implements Serializable {

	public static final long serialVersionUID = -2805454678543427093L;

	private long homeTeamId;
	private long visitingTeamId;
	private String homeTeamName;
	private String visitingTeamName;
	private Date startTime;
	private String point;

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

	public Date getStartTime() {
		return startTime;
	}

	public void setStartTime(Date startTime) {
		this.startTime = startTime;
	}

	public String getPoint() {
		return point;
	}

	public void setPoint(String point) {
		this.point = point;
	}

	public String getHomeTeamName() {
		return homeTeamName;
	}

	public void setHomeTeamName(String homeTeamName) {
		this.homeTeamName = homeTeamName;
	}

	public String getVisitingTeamName() {
		return visitingTeamName;
	}

	public void setVisitingTeamName(String visitingTeamName) {
		this.visitingTeamName = visitingTeamName;
	}
    
}
