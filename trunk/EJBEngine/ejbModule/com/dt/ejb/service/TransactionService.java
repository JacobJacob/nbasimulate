package com.dt.ejb.service;

import com.dt.ejb.common.AbstractService;
import com.dt.ejb.common.ICommand;
import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;
import com.dt.ejb.dao.TransactionDao;
import com.dt.ejb.dao.imp.TransactionDaoImp;
import com.dt.ejb.entity.Transaction;

public class TransactionService extends AbstractService implements ICommand {

	public TransactionService(Object persistenceManager) {
		super(persistenceManager);
	}

	@Override
	public void execute(RequestContext reqCtx, ResponseContext resCtx) {
		// TODO Auto-generated method stub
		Transaction transaction = new Transaction();

		transaction.setTxnId("UserManager");
		transaction.setTxnCode("Save");
		transaction.setClassName("com.dt.ejb.service.UserService");

		TransactionDao transactionDao = new TransactionDaoImp(persistenceManager);

		transactionDao.save(transaction);
		

	}

}
