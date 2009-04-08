package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;


public class Player extends Persistence{

	private long id;
	private String name;
	private int age;
	private int position;
	private int stature;
	private int avoirdupois;
	private long teamId;
	private double ability;
	private double shooting;
	private double speed;
	private double strength;
	private double bounce;
	private double stamina;
	private double trisection;
	private double dribble;
	private double pass;
	private double backboard;
	private double steal;
	private double blocked;

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

	public double getShooting() {
		return shooting;
	}

	public void setShooting(double shooting) {
		this.shooting = shooting;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public double getSpeed() {
		return speed;
	}

	public void setSpeed(double speed) {
		this.speed = speed;
	}

	public double getStrength() {
		return strength;
	}

	public void setStrength(double strength) {
		this.strength = strength;
	}

	public double getBounce() {
		return bounce;
	}

	public void setBounce(double bounce) {
		this.bounce = bounce;
	}

	public double getStamina() {
		return stamina;
	}

	public void setStamina(double stamina) {
		this.stamina = stamina;
	}

	public double getTrisection() {
		return trisection;
	}

	public void setTrisection(double trisection) {
		this.trisection = trisection;
	}

	public double getDribble() {
		return dribble;
	}

	public void setDribble(double dribble) {
		this.dribble = dribble;
	}

	public double getPass() {
		return pass;
	}

	public void setPass(double pass) {
		this.pass = pass;
	}

	public double getBackboard() {
		return backboard;
	}

	public void setBackboard(double backboard) {
		this.backboard = backboard;
	}

	public double getSteal() {
		return steal;
	}

	public void setSteal(double steal) {
		this.steal = steal;
	}

	public double getBlocked() {
		return blocked;
	}

	public void setBlocked(double blocked) {
		this.blocked = blocked;
	}
    
}
