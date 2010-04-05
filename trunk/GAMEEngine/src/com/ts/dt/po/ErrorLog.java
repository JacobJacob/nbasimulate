package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class ErrorLog extends Persistence {

	private String log;
	private String type;

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

}
