package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class Transaction extends Persistence {

	private String txnId;
	private String txnCode;
	private String className;

	public String getTxnId() {
		return txnId;
	}

	public void setTxnId(String txnId) {
		this.txnId = txnId;
	}

	public String getTxnCode() {
		return txnCode;
	}

	public void setTxnCode(String txnCode) {
		this.txnCode = txnCode;
	}

	public String getClassName() {
		return className;
	}

	public void setClassName(String className) {
		this.className = className;
	}

}
