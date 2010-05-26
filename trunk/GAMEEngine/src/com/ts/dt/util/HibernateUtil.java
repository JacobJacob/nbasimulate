package com.ts.dt.util;

import org.hibernate.HibernateException;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

public class HibernateUtil {

	private static SessionFactory sessionFactory;

	static {
		try {
			sessionFactory = new Configuration().configure().buildSessionFactory();
		} catch (HibernateException he) {
			he.printStackTrace();
			new RuntimeException(he.getMessage());
		}
	}

	public static final ThreadLocal<Session> session = new ThreadLocal<Session>();

	public static Session currentSession() throws HibernateException {

		Session s = session.get();
		if (s == null) {
			s = sessionFactory.openSession();
			session.set(s);
		}
		return s;

	}

	public static void closeSession() throws HibernateException {
		Session s = (Session) session.get();
		session.set(null);
		if (s != null) {
			s.close();
		}
	}
}
