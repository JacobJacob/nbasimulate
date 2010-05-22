package com.ts.dt.po;

import java.util.Date;

import jpersist.PersistentObject;

public class Tactics {

	private static final long serialVersionUID = 2868778652908220997L;

	private long id;
	private String name;
	private String flg;
	private Date createdTime = new Date();

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

	public String getFlg() {
		return flg;
	}

	public void setFlg(String flg) {
		this.flg = flg;
	}

	public Date getCreatedTime() {
		return createdTime;
	}

	public void setCreatedTime(Date createdTime) {
		this.createdTime = createdTime;
	}
}
