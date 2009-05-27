package com.dt.bottle.test;

import java.util.ArrayList;
import java.util.List;

import com.dt.bottle.persistence.Persistence;

public class A extends Persistence {

	private String name;
	private B b;
	private List<C> list;

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

	public List<C> getList() {
		return list;
	}

	public void setList(List<C> list) {
		this.list = list;
	}

	public void addC(C c) {
		if (list == null) {
			list = new ArrayList<C>();
		}
		list.add(c);
	}

}
