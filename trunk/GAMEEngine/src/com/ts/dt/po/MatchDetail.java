package com.ts.dt.po;

import java.io.Serializable;

import com.dt.bottle.persistence.Persistence;

public class MatchDetail extends Persistence implements Serializable {

	public static final long serialVersionUID = -2805454678543427093L;

	private long matchId;
	private long seq;
	private String description;
	private String timeMsg;
	private String pointMsg;
	private long matchNodosityMainId;

	public boolean delete() {
		// TODO Auto-generated method stub
		return false;
	}

	public void load(long id) {
		// TODO Auto-generated method stub

	}

	public boolean update() {
		// TODO Auto-generated method stub
		return false;
	}

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

	public long getSeq() {
		return seq;
	}

	public void setSeq(long seq) {
		this.seq = seq;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getTimeMsg() {
		return timeMsg;
	}

	public void setTimeMsg(String timeMsg) {
		this.timeMsg = timeMsg;
	}

	public String getPointMsg() {
		return pointMsg;
	}

	public void setPointMsg(String pointMsg) {
		this.pointMsg = pointMsg;
	}

	public long getMatchNodosityMainId() {
		return matchNodosityMainId;
	}

	public void setMatchNodosityMainId(long matchNodosityMainId) {
		this.matchNodosityMainId = matchNodosityMainId;
	}

}
