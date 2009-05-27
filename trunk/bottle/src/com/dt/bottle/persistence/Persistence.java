package com.dt.bottle.persistence;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;

public class Persistence implements Persistable {

	private long id;

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public boolean delete() {
		// TODO Auto-generated method stub
		// Session session = BottleUtil.currentSession();
		boolean result = true;
		try {
			// session.(this);
		} catch (Exception e) {
			result = false;
			e.printStackTrace();
		}
		return result;
	}

	public boolean save() {

		// TODO Auto-generated method stub
		if (this.id != 0) {
			return update();
		} else {
			return insert();
		}

	}

	private boolean insert() {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		boolean result = true;
		try {
			session.save(this);
		} catch (Exception e) {
			result = false;
			e.printStackTrace();
		}
		return result;
	}

	private boolean update() {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		boolean result = true;
		try {
			session.update(this);
		} catch (Exception e) {
			result = false;
			e.printStackTrace();
		}
		return result;
	}

	public void load(long id) {
		// TODO Auto-generated method stub
		this.id = id;
		Session session = BottleUtil.currentSession();
		try {
			session.load(this.getClass(), id);
		} catch (Exception e) {

			e.printStackTrace();
		}
	}

}
