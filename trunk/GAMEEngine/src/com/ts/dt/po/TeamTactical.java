package com.ts.dt.po;

import java.util.Date;

public class TeamTactical {

	private static final long serialVersionUID = -1033603893431540798L;

	private long id;
	private long teamId;
	private int type;

	private long tacticalDetail1Id;
	private long tacticalDetail2Id;
	private long tacticalDetail3Id;
	private long tacticalDetail4Id;
	private long tacticalDetail5Id;
	private long tacticalDetail6Id;
	private long tacticalDetail7Id;
	private long tacticalDetail8Id;
	private Date createdTime = new Date();

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

	public long getTacticalDetail1Id() {
		return tacticalDetail1Id;
	}

	public void setTacticalDetail1Id(long tacticalDetail1Id) {
		this.tacticalDetail1Id = tacticalDetail1Id;
	}

	public long getTacticalDetail2Id() {
		return tacticalDetail2Id;
	}

	public void setTacticalDetail2Id(long tacticalDetail2Id) {
		this.tacticalDetail2Id = tacticalDetail2Id;
	}

	public long getTacticalDetail3Id() {
		return tacticalDetail3Id;
	}

	public void setTacticalDetail3Id(long tacticalDetail3Id) {
		this.tacticalDetail3Id = tacticalDetail3Id;
	}

	public long getTacticalDetail4Id() {
		return tacticalDetail4Id;
	}

	public void setTacticalDetail4Id(long tacticalDetail4Id) {
		this.tacticalDetail4Id = tacticalDetail4Id;
	}

	public long getTacticalDetail5Id() {
		return tacticalDetail5Id;
	}

	public void setTacticalDetail5Id(long tacticalDetail5Id) {
		this.tacticalDetail5Id = tacticalDetail5Id;
	}

	public long getTacticalDetail6Id() {
		return tacticalDetail6Id;
	}

	public void setTacticalDetail6Id(long tacticalDetail6Id) {
		this.tacticalDetail6Id = tacticalDetail6Id;
	}

	public long getTacticalDetail7Id() {
		return tacticalDetail7Id;
	}

	public void setTacticalDetail7Id(long tacticalDetail7Id) {
		this.tacticalDetail7Id = tacticalDetail7Id;
	}

	public long getTacticalDetail8Id() {
		return tacticalDetail8Id;
	}

	public void setTacticalDetail8Id(long tacticalDetail8Id) {
		this.tacticalDetail8Id = tacticalDetail8Id;
	}

	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}

	public Date getCreatedTime() {
		return createdTime;
	}

	public void setCreatedTime(Date createdTime) {
		this.createdTime = createdTime;
	}
}
