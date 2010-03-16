package com.dt.bottle.test;

import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;

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

	public static void main(String[] args) {

		Test test = new Test();
		test.setValue("2");
		test.setB(true);
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		try {
			session.save(test);
		} catch (Exception e) {
			System.out.println(e);
		}
		session.endTransaction();
	}

}