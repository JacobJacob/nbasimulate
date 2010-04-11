package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class MatchNodosityTacticalDetail extends Persistence {

	private long matchNodosityMainId;
	private String position;
	private String playerNo;
	private String playerName;
	private float colligate;

	public long getMatchNodosityMainId() {
		return matchNodosityMainId;
	}

	public void setMatchNodosityMainId(long matchNodosityMainId) {
		this.matchNodosityMainId = matchNodosityMainId;
	}

	public String getPosition() {
		return position;
	}

	public void setPosition(String position) {
		this.position = position;
	}

	public String getPlayerNo() {
		return playerNo;
	}

	public void setPlayerNo(String playerNo) {
		this.playerNo = playerNo;
	}

	public String getPlayerName() {
		return playerName;
	}

	public void setPlayerName(String playerName) {
		this.playerName = playerName;
	}

	public float getColligate() {
		return colligate;
	}

	public void setColligate(float colligate) {
		this.colligate = colligate;
	}

}
