package com.ts.dt.po;

import java.util.Date;

public class TeamTacticalDetail {

	private static final long serialVersionUID = -7415045201242653137L;

	private long id;
	private short offensiveTacticalType;
	private short defendTacticalType;
	private long teamId;
	private String name;
	private char seq;

	private String cid;
	private String pfid;
	private String sfid;
	private String sgid;
	private String pgid;
	private Date createdTime = new Date();

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public short getOffensiveTacticalType() {
		return offensiveTacticalType;
	}

	public void setOffensiveTacticalType(short offensiveTacticalType) {
		this.offensiveTacticalType = offensiveTacticalType;
	}

	public short getDefendTacticalType() {
		return defendTacticalType;
	}

	public void setDefendTacticalType(short defendTacticalType) {
		this.defendTacticalType = defendTacticalType;
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

	public char getSeq() {
		return seq;
	}

	public void setSeq(char seq) {
		this.seq = seq;
	}

	public String getCid() {
		return cid;
	}

	public void setCid(String cid) {
		this.cid = cid;
	}

	public String getPfid() {
		return pfid;
	}

	public void setPfid(String pfid) {
		this.pfid = pfid;
	}

	public String getSfid() {
		return sfid;
	}

	public void setSfid(String sfid) {
		this.sfid = sfid;
	}

	public String getSgid() {
		return sgid;
	}

	public void setSgid(String sgid) {
		this.sgid = sgid;
	}

	public String getPgid() {
		return pgid;
	}

	public void setPgid(String pgid) {
		this.pgid = pgid;
	}

	public Date getCreatedTime() {
		return createdTime;
	}

	public void setCreatedTime(Date createdTime) {
		this.createdTime = createdTime;
	}

}
