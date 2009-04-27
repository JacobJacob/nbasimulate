package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class Team extends Persistence {

	private String name;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

}
