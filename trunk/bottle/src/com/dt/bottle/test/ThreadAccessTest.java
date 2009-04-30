package com.dt.bottle.test;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;

public class ThreadAccessTest {

	public static void main(String[] args) {
		for (int i = 0; i < 100; i++) {
			new ExecuteThread("Thread-" + i).start();
		}
	}
}

class ExecuteThread extends Thread {

	public ExecuteThread(String name) {
		super(name);
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		for (int i = 0; i < 100; i++) {
			Test test = new Test();
			test.setValue(this.getName() +"#" +i);

			try {
				session.save(test);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		session.endTransaction();
	}
}
