package com.dt.ejb.entity;

import javax.persistence.GeneratedValue;
import javax.persistence.Id;

public class Entity {
	
	private long id ;

	@Id
	@GeneratedValue
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}
	
	

}
