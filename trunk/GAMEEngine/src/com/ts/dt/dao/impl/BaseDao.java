package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.hibernate.exception.JDBCConnectionException;
import org.hibernate.exception.LockAcquisitionException;

import com.ts.dt.exception.MatchException;
import com.ts.dt.util.HibernateUtil;

public class BaseDao {

	public void saveMany(List<?> list) throws MatchException {

		while (true) {
			Session session = HibernateUtil.currentSession();
			Transaction tran = null;
			try {
				tran = session.beginTransaction();
				for (Object obj : list) {
					session.save(obj);
				}
				tran.commit();
				break;
			} catch (HibernateException he) {
				if (tran != null) {
					tran.rollback();
				}
				if (he instanceof JDBCConnectionException || he instanceof LockAcquisitionException) {
					try {
						Thread.sleep(1000 * 10);
					} catch (InterruptedException ie) {
						ie.printStackTrace();
					}
				} else {
					throw new MatchException(he);
				}
			} finally {
				HibernateUtil.closeSession();
			}
		}

	}

	public void updateMany(List<?> list) throws MatchException {

		while (true) {
			Session session = HibernateUtil.currentSession();
			Transaction tran = null;
			try {
				tran = session.beginTransaction();
				for (Object obj : list) {
					session.merge(obj);
				}
				tran.commit();
				break;
			} catch (HibernateException he) {
				if (tran != null) {
					tran.rollback();
				}
				if (he instanceof JDBCConnectionException || he instanceof LockAcquisitionException) {
					try {
						Thread.sleep(1000 * 10);
					} catch (InterruptedException ie) {
						ie.printStackTrace();
					}
				} else {
					throw new MatchException(he);
				}
			} finally {
				HibernateUtil.closeSession();
			}
		}

	}

	public void save(Object obj) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			session.save(obj);
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}
	}

	public void update(Object obj) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			session.update(obj);
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}

	}

	public void saveOrUpdate(Object obj) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			session.saveOrUpdate(obj);
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}

	}

	public Object load(Class<?> cls, long id) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = null;
		Object obj;
		try {
			tran = session.beginTransaction();
			obj = session.load(cls, id);
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		}
		return obj;

	}
}
