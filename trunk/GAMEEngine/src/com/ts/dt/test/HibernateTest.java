package com.ts.dt.test;

import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.HibernateUtil;

public class HibernateTest {

	public static void main(String[] args) {

		Session session = HibernateUtil.currentSession();

	}
}
