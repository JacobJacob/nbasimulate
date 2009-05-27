package com.dt.bottle.test;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;

public class MainTest {

	public static void main(String[] args) {

		A a = new A();
		B b = new B();

		b.setName("B");
		a.setName("A");
		a.setB(b);

		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		a.save();

		session.endTransaction();

	}

	private static void load() {

		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		// session.closeCache();
		for (int j = 0; j < 50; j++) {
			try {
				Test test = (Test) session.load(Test.class, 248081 + j);
				System.out.println("Value Is:" + test.getValue());
				test.setValue("new value-" + j);
				session.update(test);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		session.endTransaction();
	}
}
