package com.ts.dt.dao.impl;

import org.hibernate.HibernateException;
import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.exception.MatchException;
import com.ts.dt.util.HibernateUtil;

public class BaseDao {

	public void save(Object obj) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = session.beginTransaction();
		try {
			session.save(obj);
			session.flush();
			tran.commit();
		} catch (HibernateException he) {
			tran.rollback();
			throw new MatchException(he);
		}

	}
}
