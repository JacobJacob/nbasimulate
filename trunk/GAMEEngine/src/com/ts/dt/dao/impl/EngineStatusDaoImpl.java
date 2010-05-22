package com.ts.dt.dao.impl;

import com.ts.dt.dao.EngineStatusDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;

public class EngineStatusDaoImpl extends BaseDao implements EngineStatusDao {

	public void save(EngineStatus engineStatus) throws MatchException {
		// TODO Auto-generated method stub
		super.saveOrUpdate(engineStatus);
	}

}
