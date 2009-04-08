package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;

public class Player extends Persistence {

	private long id;
	private String name;
	private int age;
	private int position;
	private int stature;
	private int avoirdupois;
	private long teamId;
	private double ability;
	
	public long getId() {
		return id;
	}
	public void setId(long id) {
		this.id = id;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public int getAge() {
		return age;
	}
	public void setAge(int age) {
		this.age = age;
	}
	public int getPosition() {
		return position;
	}
	public void setPosition(int position) {
		this.position = position;
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
	public long getTeamId() {
		return teamId;
	}
	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}
	public double getAbility() {
		return ability;
	}
	public void setAbility(double ability) {
		this.ability = ability;
	}
	
	
}
