package com.ts.dt.po;

import java.util.Date;

public class MatchNodosityTacticalDetail {

	private static final long serialVersionUID = 158899388567875270L;

	private long id;
	private long matchNodosityMainId;
	private String position;
	private String playerNo;
	private String playerName;
	private int power;
	private float ability;
	private int age;
	private int stature;
	private int avoirdupois;
	private int no;
	private Date createdTime = new Date();

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

	public int getPower() {
		return power;
	}

	public void setPower(int power) {
		this.power = power;
	}

	public int getAge() {
		return age;
	}

	public void setAge(int age) {
		this.age = age;
	}

	public int getStature() {
		return stature;
	}

	public void setStature(int stature) {
		this.stature = stature;
	}

	public int getAvoirdupois() {
		return avoirdupois;
	}

	public void setAvoirdupois(int avoirdupois) {
		this.avoirdupois = avoirdupois;
	}

	public int getNo() {
		return no;
	}

	public void setNo(int no) {
		this.no = no;
	}

	public float getAbility() {
		return ability;
	}

	public void setAbility(float ability) {
		this.ability = ability;
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
