package com.dt.bottle.persistence;

import java.util.List;

import com.dt.bottle.session.Session;

public class Persistence implements Persistable {

	private long id;

	public boolean delete() {
		// TODO Auto-generated method stub
		return false;
	}

	public boolean save() {
		// TODO Auto-generated method stub
		Session session = Session.getInstance();
		boolean result = true;
		try {
			session.save(this);
		} catch (Exception e) {
			result = false;
			e.printStackTrace();
		}
		return result;
	}

	public boolean update() {
		// TODO Auto-generated method stub
		return false;
	}

	public void load(long id) {
		// TODO Auto-generated method stub
		this.id = id;
		Session session = Session.getInstance();
		try {
			session.load(this);
		} catch (Exception e) {

			e.printStackTrace();
		}
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

}
