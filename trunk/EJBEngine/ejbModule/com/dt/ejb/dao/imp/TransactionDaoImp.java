package com.dt.ejb.dao.imp;

import com.dt.ejb.dao.DaoUtil;
import com.dt.ejb.dao.TransactionDao;

public class TransactionDaoImp extends DaoUtil implements TransactionDao {

	public TransactionDaoImp(Object persistenceManager) {
		super(persistenceManager);
	}
}
