package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class YouthPlayer extends Persistence implements Player {

	private long id;
	private String no;
	private String name;
	private int age;
	private String position;
	private int stature;
	private int power;
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

	private double defencons;
	private double offencons;
	private double buildupcons;
	private double leadcons;
	private double backbone;

	public float getColligate() {
		return 0;
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

	public String getPosition() {
		return position;
	}

	public int getPower() {
		return power;
	}

	public void setPower(int power) {
		this.power = power;
	}

	public void setPosition(String position) {
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

	public long getTeamid() {
		return teamId;
	}

	public void setTeamid(long teamId) {
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

	public double getDefencons() {
		return defencons;
	}

	public void setDefencons(double defencons) {
		this.defencons = defencons;
	}

	public double getOffencons() {
		return offencons;
	}

	public void setOffencons(double offencons) {
		this.offencons = offencons;
	}

	public double getBuildupcons() {
		return buildupcons;
	}

	public void setBuildupcons(double buildupcons) {
		this.buildupcons = buildupcons;
	}

	public double getLeadcons() {
		return leadcons;
	}

	public void setLeadcons(double leadcons) {
		this.leadcons = leadcons;
	}

	public double getBackbone() {
		return backbone;
	}

	public void setBackbone(double backbone) {
		this.backbone = backbone;
	}

	public String getNo() {
		return no;
	}

	public void setNo(String no) {
		this.no = no;
	}

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

}
