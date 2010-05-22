package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.exception.MatchException;
import com.ts.dt.util.HibernateUtil;

public class BaseDao {

	public void saveMany(List<?> list) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = session.beginTransaction();
		try {
			for (Object obj : list) {
				session.save(obj);
			}
			tran.commit();
		} catch (HibernateException he) {
			tran.rollback();
			throw new MatchException(he);
		}

	}

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

	public void saveOrUpdate(Object obj) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = session.beginTransaction();
		try {
			session.saveOrUpdate(obj);
			session.flush();
			tran.commit();
		} catch (HibernateException he) {
			tran.rollback();
			throw new MatchException(he);
		}

	}

	public Object load(Class cls, long id) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = session.beginTransaction();
		try {
			return session.load(cls, id);
		} catch (HibernateException he) {
			tran.rollback();
			throw new MatchException(he);
		}

	}
}
