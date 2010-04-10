package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class TeamTacticalDetail extends Persistence {

	private int offensive_tactical_type;
	private int defend_tactical_type;
	private long team_id;
	private String name;

	private char seq;

	private String cid;
	private String pfid;
	private String sfid;
	private String sgid;
	private String pgid;

	public int getOffensive_tactical_type() {
		return offensive_tactical_type;
	}

	public void setOffensive_tactical_type(int offensiveTacticalType) {
		offensive_tactical_type = offensiveTacticalType;
	}

	public int getDefend_tactical_type() {
		return defend_tactical_type;
	}

	public void setDefend_tactical_type(int defendTacticalType) {
		defend_tactical_type = defendTacticalType;
	}

	public long getTeam_id() {
		return team_id;
	}

	public void setTeam_id(long teamId) {
		team_id = teamId;
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

}