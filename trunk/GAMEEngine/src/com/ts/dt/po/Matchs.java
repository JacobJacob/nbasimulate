package com.ts.dt.po;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.util.Date;

import jpersist.PersistentObject;

public class Matchs {

	public static final long serialVersionUID = -2805454678543428303L;

	public static final String UPDATE_SQL = "update matchs set home_team_id=?, guest_team_id=?, status=?, sub_status=?, start_time=?, point=?, type=?, overTime=? where id=?";

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
	private Date createdTime = new Date();

	public boolean update(Connection connection) {
		try {
			PreparedStatement statement = connection.prepareStatement(UPDATE_SQL);
			statement.setLong(1, this.homeTeamId);
			statement.setLong(2, this.guestTeamId);
			statement.setInt(3, this.status);
			statement.setInt(4, this.subStatus);
			statement.setDate(5, new java.sql.Date(this.startTime.getTime()));
			statement.setString(6, this.point);
			statement.setInt(7, this.type);
			statement.setInt(8, this.overtime);
			statement.setLong(9, this.getId());
			statement.execute();

		} catch (Exception e) {
			e.printStackTrace();
			return false;
		}
		return true;
	}

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
}
