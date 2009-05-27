package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;

public class A extends Persistence {

	private String name;
	private B b;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public B getB() {
		return b;
	}

	public void setB(B b) {
		this.b = b;
	}

}
