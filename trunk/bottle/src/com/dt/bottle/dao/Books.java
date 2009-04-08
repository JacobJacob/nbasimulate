package com.dt.bottle.dao;

import com.dt.bottle.persistence.Persistence;

public class Books extends Persistence {
   
	private String name ;

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	
	
}
