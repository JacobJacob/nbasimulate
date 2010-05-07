package com.ts.dt.po;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.util.Date;

import com.dt.bottle.persistence.Persistence;

public class Matchs extends Persistence {

    public static final long serialVersionUID = -2805454678543428303L;

    public static final String UPDATE_SQL = "update matchs set home_team_id=?, guest_team_id=?, status=?, sub_status=?, start_time=?, point=?, type=? where id=?";

    private long homeTeamId;
    private long guestTeamId;
    private int status;
    private int subStatus;
    private Date startTime;
    private String point;
    private int type;
    private boolean isYouth;

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
	    statement.setLong(8, this.getId());
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

}
