package com.ts.dt.dao.impl;

import org.hibernate.Query;
import org.hibernate.Session;

import com.ts.dt.dao.EngineStatusDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;
import com.ts.dt.po.Player;
import com.ts.dt.util.HibernateUtil;

public class EngineStatusDaoImpl extends BaseDao implements EngineStatusDao {

	public void save(EngineStatus engineStatus) throws MatchException {
		// TODO Auto-generated method stub
		super.saveOrUpdate(engineStatus);
	}

	public void update(EngineStatus engineStatus) throws MatchException {
		// TODO Auto-generated method stub
		super.update(engineStatus);
	}

	public EngineStatus load(String name) {
		// TODO Auto-generated method stub
		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from EngineStatus a where a.name = :name");
		q.setString("name", name);
		EngineStatus engineStatus = (EngineStatus) q.uniqueResult();
		return engineStatus;

	}

}
