package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;

public class D extends Persistence {

	private long bId;
	private String name;

	public long getBId() {
		return bId;
	}

	public void setBId(long id) {
		bId = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

}
