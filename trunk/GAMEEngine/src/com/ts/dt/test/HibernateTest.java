package com.ts.dt.test;

import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.HibernateUtil;

public class HibernateTest {

	public static void main(String[] args) {

		Session session = HibernateUtil.currentSession();

		Transaction tran = session.beginTransaction();
		ProfessionPlayer player = new ProfessionPlayer();
		player.setNo("12eweqwe34");
		session.save(player);
		session.flush();
		tran.commit();
	}
}
