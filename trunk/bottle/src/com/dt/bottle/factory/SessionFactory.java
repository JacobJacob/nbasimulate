package com.dt.bottle.factory;

import com.dt.bottle.session.Session;

public class SessionFactory {

	private static SessionFactory factory;

	private SessionFactory() {

	}

	public static SessionFactory getInstance() {

		if (factory == null) {
			factory = new SessionFactory();
		}
		return factory;
	}

	public Session getSession() {
		return new Session();
	}

}
