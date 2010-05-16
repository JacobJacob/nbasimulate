package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class ProfessionPlayer extends Persistence implements Player {

	private long id;
	private String no;
	private String name;
	private int age;
	private int power;
	private int matchPower;
	private int leaguePower;
	private String position;
	private int stature;
	private int avoirdupois;
	private long teamId;
	private float ability;
	private float shooting;
	private float speed;
	private float strength;
	private float bounce;
	private float stamina;
	private float trisection;
	private float dribble;
	private float pass;
	private float backboard;
	private float steal;
	private float blocked;

	private float defencons;
	private float offencons;
	private float buildupcons;
	private float leadcons;
	private float backbone;
	private int playerNo;

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

	public void setPosition(String position) {
		this.position = position;
	}

	public int getPower() {
		return power;
	}

	public void setPower(int power) {
		this.power = power;
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

	public float getAbility() {
		return ability;
	}

	public void setAbility(float ability) {
		this.ability = ability;
	}

	public float getShooting() {
		return shooting;
	}

	public void setShooting(float shooting) {
		this.shooting = shooting;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public float getSpeed() {
		return speed;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
	}

	public float getStrength() {
		return strength;
	}

	public void setStrength(float strength) {
		this.strength = strength;
	}

	public float getBounce() {
		return bounce;
	}

	public void setBounce(float bounce) {
		this.bounce = bounce;
	}

	public float getStamina() {
		return stamina;
	}

	public void setStamina(float stamina) {
		this.stamina = stamina;
	}

	public float getTrisection() {
		return trisection;
	}

	public void setTrisection(float trisection) {
		this.trisection = trisection;
	}

	public float getDribble() {
		return dribble;
	}

	public void setDribble(float dribble) {
		this.dribble = dribble;
	}

	public float getPass() {
		return pass;
	}

	public void setPass(float pass) {
		this.pass = pass;
	}

	public float getBackboard() {
		return backboard;
	}

	public void setBackboard(float backboard) {
		this.backboard = backboard;
	}

	public float getSteal() {
		return steal;
	}

	public void setSteal(float steal) {
		this.steal = steal;
	}

	public float getBlocked() {
		return blocked;
	}

	public void setBlocked(float blocked) {
		this.blocked = blocked;
	}

	public float getDefencons() {
		return defencons;
	}

	public void setDefencons(float defencons) {
		this.defencons = defencons;
	}

	public float getOffencons() {
		return offencons;
	}

	public void setOffencons(float offencons) {
		this.offencons = offencons;
	}

	public float getBuildupcons() {
		return buildupcons;
	}

	public void setBuildupcons(float buildupcons) {
		this.buildupcons = buildupcons;
	}

	public float getLeadcons() {
		return leadcons;
	}

	public void setLeadcons(float leadcons) {
		this.leadcons = leadcons;
	}

	public float getBackbone() {
		return backbone;
	}

	public void setBackbone(float backbone) {
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

	public int getMatchPower() {
		return matchPower;
	}

	public void setMatchPower(int matchPower) {
		this.matchPower = matchPower;
	}

	public int getLeaguePower() {
		return leaguePower;
	}

	public void setLeaguePower(int leaguePower) {
		this.leaguePower = leaguePower;
	}

	public int getPlayerNo() {
		return playerNo;
	}

	public void setPlayerNo(int playerNo) {
		this.playerNo = playerNo;
	}

}
