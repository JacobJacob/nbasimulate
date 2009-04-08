package com.ts.dt.test;

import com.ts.dt.dao.PlayerDao;
import com.ts.dt.dao.imp.PlayerDaoImpl;
import com.ts.dt.po.Player;

public class ThreadAccessDbTest extends Thread {

	private String name;

	public ThreadAccessDbTest(String name) {
		this.name = name;
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			System.out.println(name + " start to work....");
			PlayerDao playerDao = new PlayerDaoImpl();
			Player player = new Player();
			player.setName("Test");
			playerDao.save(player);
			System.out.println(name + " finish work....");
			try {
				sleep(2000);
			} catch (InterruptedException ie) {
				ie.printStackTrace();
			}
		}
	}

	public static void main(String[] args) {
		for (int i = 0; i < 10; i++) {
			new ThreadAccessDbTest("Thread-" + (i + 1)).start();
		}
	}

}
