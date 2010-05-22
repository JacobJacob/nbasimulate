package com.ts.dt.po;

import java.io.Serializable;
import java.util.Date;

import jpersist.PersistentObject;

public class MatchNodosityDetail extends PersistentObject implements Serializable {

	public static final long serialVersionUID = -2805454678543427093L;

	private long id;
	private long matchId;
	private long seq;
	private String description;
	private String timeMsg;
	private String pointMsg;
	private boolean isNewLine;
	private long matchNodosityMainId;
	private Date createdTime = new Date();

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

	public boolean isNewLine() {
		return isNewLine;
	}

	public void setNewLine(boolean isNewLine) {
		this.isNewLine = isNewLine;
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
