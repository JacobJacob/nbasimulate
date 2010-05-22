package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.Query;
import org.hibernate.Session;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.HibernateUtil;

public class MatchReqDaoImpl extends BaseDao implements MatchReqDao {

	public List<Matchs> getAllNewReq() {

		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from Matchs a where a.status = :status ");
		q.setMaxResults(5);
		q.setInteger("status", MatchStatus.ACCP);
		List<Matchs> list = q.list();
		return list;

	}

	public void save(Matchs matchReq) throws MatchException {
		// TODO Auto-generated method stub
		super.save(matchReq);
	}

	public static void main(String[] args) {
		MatchReqDaoImpl matchReqDaoImpl = new MatchReqDaoImpl();
		List list = matchReqDaoImpl.getAllNewReq();
		System.out.println(list.size());
	}
}
