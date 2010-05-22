package com.ts.dt.po;

import java.util.Date;

import jpersist.PersistentObject;

public class EngineStatus {

	private static final long serialVersionUID = -7087943317305234555L;

	private long id;
	private String name;
	private String status;
	private Date createdTime = new Date();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
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
