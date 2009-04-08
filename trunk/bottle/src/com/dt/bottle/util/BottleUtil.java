package com.dt.bottle.util;

import javax.security.auth.login.Configuration;

import com.dt.bottle.factory.SessionFactory;
import com.dt.bottle.session.Session;

public class BottleUtil {

	public static final SessionFactory sessionFactory;
	static {
		try {
			sessionFactory = SessionFactory.getInstance();
		} catch (Throwable ex) {
			throw new ExceptionInInitializerError(ex);
		}
	}
	public static final ThreadLocal<Session> session = new ThreadLocal<Session>();

	public static Session currentSession() {
		Session s = session.get();
		if (s == null) {
			s = sessionFactory.getSession();
			session.set(s);
		}
		return s;
	}

	public static void closeSession() {
		Session s = session.get();
		if (s != null) {
			s.close();
		}
		session.set(null);
	}

}
