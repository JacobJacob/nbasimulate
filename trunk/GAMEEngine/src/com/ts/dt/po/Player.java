package com.ts.dt.po;

import com.dt.bottle.persistence.Persistable;

public interface Player extends Persistable {

	public abstract float getColligate();

	public abstract String getName();

	public abstract void setName(String name);

	public abstract int getAge();

	public abstract void setAge(int age);

	public abstract String getPosition();

	public abstract int getPower();

	public abstract void setPower(int power);

	public abstract void setPosition(String position);

	public abstract int getStature();

	public abstract void setStature(int stature);

	public abstract int getAvoirdupois();

	public abstract void setAvoirdupois(int avoirdupois);

	public abstract long getTeamid();

	public abstract void setTeamid(long teamId);

	public abstract double getAbility();

	public abstract void setAbility(double ability);

	public abstract double getShooting();

	public abstract void setShooting(double shooting);

	public abstract long getId();

	public abstract void setId(long id);

	public abstract double getSpeed();

	public abstract void setSpeed(double speed);

	public abstract double getStrength();

	public abstract void setStrength(double strength);

	public abstract double getBounce();

	public abstract void setBounce(double bounce);

	public abstract double getStamina();

	public abstract void setStamina(double stamina);

	public abstract double getTrisection();

	public abstract void setTrisection(double trisection);

	public abstract double getDribble();

	public abstract void setDribble(double dribble);

	public abstract double getPass();

	public abstract void setPass(double pass);

	public abstract double getBackboard();

	public abstract void setBackboard(double backboard);

	public abstract double getSteal();

	public abstract void setSteal(double steal);

	public abstract double getBlocked();

	public abstract void setBlocked(double blocked);

	public abstract double getDefencons();

	public abstract void setDefencons(double defencons);

	public abstract double getOffencons();

	public abstract void setOffencons(double offencons);

	public abstract double getBuildupcons();

	public abstract void setBuildupcons(double buildupcons);

	public abstract double getLeadcons();

	public abstract void setLeadcons(double leadcons);

	public abstract double getBackbone();

	public abstract void setBackbone(double backbone);

	public abstract String getNo();

	public abstract void setNo(String no);

	public abstract long getTeamId();

	public abstract void setTeamId(long teamId);

	public abstract int getMatchPower();

	public abstract void setMatchPower(int matchPower);

	public abstract int getLeaguePower();

	public abstract void setLeaguePower(int leaguePower);

}