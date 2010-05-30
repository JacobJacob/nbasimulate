package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchReq;
import com.ts.dt.util.HibernateUtil;

public class MatchReqDaoImpl extends BaseDao implements MatchReqDao {

	public List<MatchReq> getAllNewReq() throws MatchException {

		Session session = HibernateUtil.currentSession();
		List<MatchReq> list = null;
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			Query q = session.createQuery("from Matchs a where a.status = :status ");
			q.setMaxResults(5);
			q.setInteger("status", MatchStatus.ACCP);
			list = q.list();
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}
		return list;
	}

	public MatchReq getOneNewReq() throws MatchException {

		Session session = HibernateUtil.currentSession();
		MatchReq matchReq = null;
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			Query q = session.createQuery("from MatchReq a where a.status = :status ");
			q.setMaxResults(1);
			q.setInteger("status", MatchStatus.ACCP);
			matchReq = (MatchReq) q.uniqueResult();
			tran.commit();
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}
		return matchReq;
	}

	public void update(MatchReq matchReq) throws MatchException {
		// TODO Auto-generated method stub
		super.update(matchReq);
	}

	public void update(List<MatchReq> matchReqs) throws MatchException {
		// TODO Auto-generated method stub
		super.updateMany(matchReqs);
	}

	public static void main(String[] args) throws MatchException {
		MatchReqDaoImpl matchReqDaoImpl = new MatchReqDaoImpl();
		// Matchs match = matchReqDaoImpl.getOneNewReq();
		// System.out.println(match);
	}
}
