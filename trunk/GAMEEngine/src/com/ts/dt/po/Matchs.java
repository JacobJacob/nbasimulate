package com.ts.dt.po;

import java.util.Date;
import com.dt.bottle.persistence.Persistence;

public class Matchs extends Persistence {

	public static final long serialVersionUID = -2805454678543428303L;

	private long homeTeamId;
	private long guestTeamId;
	private int status;
	private Date startTime;
	private String point;
	private int type;

	public long getHomeTeamId() {
		return homeTeamId;
	}

	public long getGuestTeamId() {
		return guestTeamId;
	}

	public void setGuestTeamId(long guestTeamId) {
		this.guestTeamId = guestTeamId;
	}

	public int getStatus() {
		return status;
	}

	public void setStatus(int status) {
		this.status = status;
	}

	public void setHomeTeamId(long homeTeamId) {
		this.homeTeamId = homeTeamId;
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

	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}

}
