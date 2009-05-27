package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;

public class C extends Persistence {

	private long aId;
	private String name;

	public long getAId() {
		return aId;
	}

	public void setAId(long id) {
		aId = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

}
