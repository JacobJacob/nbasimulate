package com.ts.dt.po;

public class ActionAssoAttr {
	
	private long id;
	private long action_id;
	private String name;
	private String desc;
	private char plus_minus;
	private int point;
	public long getId() {
		return id;
	}
	public void setId(long id) {
		this.id = id;
	}
	public long getAction_id() {
		return action_id;
	}
	public void setAction_id(long action_id) {
		this.action_id = action_id;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getDesc() {
		return desc;
	}
	public void setDesc(String desc) {
		this.desc = desc;
	}
	public char getPlus_minus() {
		return plus_minus;
	}
	public void setPlus_minus(char plus_minus) {
		this.plus_minus = plus_minus;
	}
	public int getPoint() {
		return point;
	}
	public void setPoint(int point) {
		this.point = point;
	}
	
	
}
