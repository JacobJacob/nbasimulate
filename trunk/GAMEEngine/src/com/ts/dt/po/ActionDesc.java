package com.ts.dt.po;

import java.io.Serializable;
import java.util.Date;

import jpersist.PersistentObject;

public class ActionDesc extends PersistentObject implements Serializable {

	public static final long serialVersionUID = -2805454657233427093L;

	private long id;
	private String actionName;
	private String actionDesc;
	private String result;
	private String flg;
	private boolean isAssist;
	private boolean notStick;
	private int percent;
	private Date createdTime = new Date();

	public boolean delete() {
		// TODO Auto-generated method stub
		return false;
	}

	public void load(long id) {
		// TODO Auto-generated method stub

	}

	public boolean save() {
		// TODO Auto-generated method stub
		return false;
	}

	public boolean update() {
		// TODO Auto-generated method stub
		return false;
	}

	public String getActionName() {
		return actionName;
	}

	public void setActionName(String actionName) {
		this.actionName = actionName;
	}

	public String getActionDesc() {
		return actionDesc;
	}

	public void setActionDesc(String actionDesc) {
		this.actionDesc = actionDesc;
	}

	public String getResult() {
		return result;
	}

	public void setResult(String result) {
		this.result = result;
	}

	public String getFlg() {
		return flg;
	}

	public void setFlg(String flg) {
		this.flg = flg;
	}

	public int getPercent() {
		return percent;
	}

	public void setPercent(int percent) {
		this.percent = percent;
	}

	public boolean getIsAssist() {
		return isAssist;
	}

	public void setIsAssist(boolean isAssist) {
		this.isAssist = isAssist;
	}

	public boolean getNotStick() {
		return notStick;
	}

	public void setNotStick(boolean notStick) {
		this.notStick = notStick;
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
