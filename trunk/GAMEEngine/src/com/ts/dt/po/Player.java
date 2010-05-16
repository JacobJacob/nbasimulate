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

	public abstract float getAbility();

	public abstract void setAbility(float ability);

	public abstract float getShooting();

	public abstract void setShooting(float shooting);

	public abstract long getId();

	public abstract void setId(long id);

	public abstract float getSpeed();

	public abstract void setSpeed(float speed);

	public abstract float getStrength();

	public abstract void setStrength(float strength);

	public abstract float getBounce();

	public abstract void setBounce(float bounce);

	public abstract float getStamina();

	public abstract void setStamina(float stamina);

	public abstract float getTrisection();

	public abstract void setTrisection(float trisection);

	public abstract float getDribble();

	public abstract void setDribble(float dribble);

	public abstract float getPass();

	public abstract void setPass(float pass);

	public abstract float getBackboard();

	public abstract void setBackboard(float backboard);

	public abstract float getSteal();

	public abstract void setSteal(float steal);

	public abstract float getBlocked();

	public abstract void setBlocked(float blocked);

	public abstract float getDefencons();

	public abstract void setDefencons(float defencons);

	public abstract float getOffencons();

	public abstract void setOffencons(float offencons);

	public abstract float getBuildupcons();

	public abstract void setBuildupcons(float buildupcons);

	public abstract float getLeadcons();

	public abstract void setLeadcons(float leadcons);

	public abstract float getBackbone();

	public abstract void setBackbone(float backbone);

	public abstract String getNo();

	public abstract void setNo(String no);

	public abstract long getTeamId();

	public abstract void setTeamId(long teamId);

	public abstract int getMatchPower();

	public abstract void setMatchPower(int matchPower);

	public abstract int getLeaguePower();

	public abstract void setLeaguePower(int leaguePower);

	public abstract int getPlayerNo();
}