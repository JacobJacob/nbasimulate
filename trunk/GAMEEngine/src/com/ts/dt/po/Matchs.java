package com.ts.dt.po;

import java.util.Date;

public class Matchs {

	public static final long serialVersionUID = -2805454678543428303L;

	private long id;
	private long homeTeamId;
	private long guestTeamId;
	private int status;
	private int subStatus;
	private Date startTime;
	private String point;
	private int type;
	private int overtime = 0; // 一个多少个加时
	private boolean isYouth;
	private String client;
	private Date createdTime = new Date();

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

	public int getSubStatus() {
		return subStatus;
	}

	public void setSubStatus(int subStatus) {
		this.subStatus = subStatus;
	}

	public boolean getIsYouth() {
		return isYouth;
	}

	public void setIsYouth(boolean isYouth) {
		this.isYouth = isYouth;
	}

	public int getOvertime() {
		return overtime;
	}

	public void setOvertime(int overtime) {
		this.overtime = overtime;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public Date getCreatedTime() {
		return createdTime;
	}

	public void setCreatedTime(Date createdTime) {
		this.createdTime = createdTime;
	}

	public String getClient() {
		return client;
	}

	public void setClient(String client) {
		this.client = client;
	}

}
