package com.ts.dt.pool;

import java.util.Hashtable;

import com.ts.dt.dao.imp.TransactionDaoImp;
import com.ts.dt.po.Transaction;

public class TransactionPool {

	private static TransactionPool pool;

	private Hashtable<String, Transaction> idTab;
	private Hashtable<String, Object> codeTab;

	private TransactionPool() {
		idTab = new Hashtable<String, Transaction>();
		codeTab = new Hashtable<String, Object>();
	}

	public static TransactionPool getInstance() {

		if (pool == null) {
			pool = new TransactionPool();
		}

		return pool;

	}

	public Transaction getTransaction(String txnId, String txnCode) {
		Transaction transaction;
		if (idTab.containsKey(txnId) && codeTab.containsKey(txnCode)) {
			transaction = idTab.get(txnId);
		} else {
			TransactionDaoImp transactionDaoImp = new TransactionDaoImp();

			transaction = transactionDaoImp.loadByTxnIdTxnCode(txnId, txnCode);
			idTab.put(txnId, transaction);
			codeTab.put(txnCode, "");
		}
		return transaction;
	}
}
