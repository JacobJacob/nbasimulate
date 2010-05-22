package com.ts.dt.po;

import java.util.Date;

public class Team {

	private static final long serialVersionUID = -1193583213322117826L;

	private long id;
	private String name;
	private Date createdTime = new Date();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
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
