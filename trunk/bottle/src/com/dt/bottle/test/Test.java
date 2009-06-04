package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;

public class Test extends Persistence {

	private String value;
	private boolean b;

	public String getValue() {
		return value;
	}

	public void setValue(String value) {
		this.value = value;
	}

	public boolean getB() {
		return b;
	}

	public void setB(boolean b) {
		this.b = b;
	}

}