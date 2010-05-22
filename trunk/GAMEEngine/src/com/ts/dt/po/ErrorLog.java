package com.ts.dt.po;

import java.util.Date;

import jpersist.PersistentObject;

public class ErrorLog extends PersistentObject {

	private static final long serialVersionUID = -1215064671692626507L;

	private long id;
	private String log;
	private String type;
	private Date createdTime = new Date();

	public String getLog() {
		return log;
	}

	public void setLog(String log) {
		this.log = log;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
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
