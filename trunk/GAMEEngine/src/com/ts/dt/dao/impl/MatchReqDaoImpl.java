package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.HibernateUtil;

public class MatchReqDaoImpl extends BaseDao implements MatchReqDao {

	public List<Matchs> getAllNewReq() throws MatchException {

		Session session = HibernateUtil.currentSession();
		Transaction tran = null;
		try {
			tran = session.beginTransaction();
			Query q = session.createQuery("from Matchs a where a.status = :status ");
			q.setMaxResults(5);
			q.setInteger("status", MatchStatus.ACCP);
			List<Matchs> list = q.list();
			tran.commit();
			return list;
		} catch (HibernateException he) {
			if (tran != null) {
				tran.rollback();
			}
			throw new MatchException(he);
		} finally {
			HibernateUtil.closeSession();
		}
	}

	public void save(Matchs matchReq) throws MatchException {
		// TODO Auto-generated method stub
		super.save(matchReq);
	}

	public static void main(String[] args) throws MatchException {
		MatchReqDaoImpl matchReqDaoImpl = new MatchReqDaoImpl();
		List list = matchReqDaoImpl.getAllNewReq();
		System.out.println(list.size());
	}
}
