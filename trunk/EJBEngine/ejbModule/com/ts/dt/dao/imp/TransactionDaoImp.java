package com.ts.dt.dao.imp;

import com.dt.bottle.exception.ObjectNotFoundException;
import com.dt.bottle.exception.SessionException;
import com.dt.bottle.persistence.Persistable;
import com.dt.bottle.session.Session;
import com.ts.dt.dao.TransactionDao;
import com.ts.dt.po.Transaction;

public class TransactionDaoImp implements TransactionDao {

	public static final String SQL_LOAD_BY_TXNID_TXNCODE = "SELECT * FROM transaction WHERE TXN_ID = ? AND TXN_CODE = ? ";

	public void delete(Persistable obj) {
		// TODO Auto-generated method stub

	}

	public Transaction load(long id) {
		// TODO Auto-generated method stub
		Transaction transaction = new Transaction();
		transaction.load(id);
		return transaction;
	}

	public void save(Persistable obj) {
		// TODO Auto-generated method stub

	}

	public void update(Persistable obj) {
		// TODO Auto-generated method stub

	}

	public Transaction loadByTxnIdTxnCode(String txnId, String txnCode) {

		Object[] parms = { txnId, txnCode };
		Session session = new Session();
		long id = -1;
		try {
			id = session.getIdBySql(SQL_LOAD_BY_TXNID_TXNCODE, parms);
		} catch (ObjectNotFoundException e) {
           e.printStackTrace();
		} catch (SessionException se) {
          se.printStackTrace();
		}
		return load(id);

	}
}
