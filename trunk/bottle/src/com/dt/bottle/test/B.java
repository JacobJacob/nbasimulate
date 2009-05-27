package com.dt.bottle.test;

import java.util.ArrayList;
import java.util.List;

import com.dt.bottle.persistence.Persistence;

public class B extends Persistence {

	private long aId;
	private String name;
	private List<D> list;

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

	public List<D> getList() {
		return list;
	}

	public void setList(List<D> list) {
		this.list = list;
	}

	public void addD(D d) {
		if (list == null) {
			list = new ArrayList<D>();
		}
		list.add(d);
	}
}
