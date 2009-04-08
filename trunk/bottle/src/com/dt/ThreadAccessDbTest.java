package com.dt;

import com.dt.bottle.dao.PlayerDaoImpl;
import com.dt.bottle.test.Player;

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
			PlayerDaoImpl playerDaoImpl = new PlayerDaoImpl();
			Player player = new Player();
			player.setName("Test");
			playerDaoImpl.save(player);
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