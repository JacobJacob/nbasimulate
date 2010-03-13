package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class TeamTacticalDetail extends Persistence {

	private long tacticsId;
	private long seq;

	private long cid;
	private long pfid;
	private long sfid;
	private long sgid;
	private long pgid;

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

	public long getCid() {
		return cid;
	}

	public void setCid(long id) {
		cid = id;
	}

	public long getPfid() {
		return pfid;
	}

	public void setPfid(long pfId) {
		this.pfid = pfId;
	}

	public long getSfid() {
		return sfid;
	}

	public void setSfid(long sfId) {
		this.sfid = sfId;
	}

	public long getSgid() {
		return sgid;
	}

	public void setSgid(long sgId) {
		this.sgid = sgId;
	}

	public long getPgid() {
		return pgid;
	}

	public void setPgid(long pgId) {
		this.pgid = pgId;
	}

}
