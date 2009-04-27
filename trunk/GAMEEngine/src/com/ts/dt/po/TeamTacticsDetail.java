package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class TeamTacticsDetail extends Persistence {

	private long teamTacticsId;
	private long tacticsId;
	private long seq;

	private long cId;
	private long pfId;
	private long sfId;
	private long sgId;
	private long pgId;

	public long getTeamTacticsId() {
		return teamTacticsId;
	}

	public void setTeamTacticsId(long teamTacticsId) {
		this.teamTacticsId = teamTacticsId;
	}

	public long getTacticsId() {
		return tacticsId;
	}

	public void setTacticsId(long tacticsId) {
		this.tacticsId = tacticsId;
	}

	public long getSeq() {
		return seq;
	}

	public void setSeq(long seq) {
		this.seq = seq;
	}

	public long getCId() {
		return cId;
	}

	public void setCId(long id) {
		cId = id;
	}

	public long getPfId() {
		return pfId;
	}

	public void setPfId(long pfId) {
		this.pfId = pfId;
	}

	public long getSfId() {
		return sfId;
	}

	public void setSfId(long sfId) {
		this.sfId = sfId;
	}

	public long getSgId() {
		return sgId;
	}

	public void setSgId(long sgId) {
		this.sgId = sgId;
	}

	public long getPgId() {
		return pgId;
	}

	public void setPgId(long pgId) {
		this.pgId = pgId;
	}

}
