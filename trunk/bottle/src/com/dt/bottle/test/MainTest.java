package com.dt.bottle.test;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;

public class MainTest {

	public static void main(String[] args) {

		// A a = new A();
		// B b = new B();
		//
		// b.setName("B");
		// D d1 = new D();
		// d1.load(1);
		// d1.setName("new d1 name");
		// b.addD(d1);
		//
		// a.setName("A");
		//
		// C c1 = new C();
		// c1.setName("C1");
		// a.addC(c1);
		// C c2 = new C();
		// c2.setName("C2");
		// a.addC(c2);
		//
		// a.setB(b);
		//
		// Session session = BottleUtil.currentSession();
		// session.beginTransaction();
		// a.save();
		//
		// session.endTransaction();
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		long start = System.currentTimeMillis();

		// Test test = new Test();
		// test.setValue("Value-");
		// test.setB(false);
		// test.save();
		for (int i = 0; i < 2000; i++) {
			Test test = new Test();
			test.setValue("Value-" + i);
			test.setB(false);
			test.save();
		}

		long end = System.currentTimeMillis();
		System.out.println(end - start);
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
