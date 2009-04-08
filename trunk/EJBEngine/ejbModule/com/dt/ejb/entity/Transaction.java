package com.dt.ejb.entity;

import javax.persistence.GeneratedValue;
import javax.persistence.Entity;
import javax.persistence.Id;

@Entity
public class Transaction {

	private long id;

	private String txnId;

	private String txnCode;

	private String className;

	@Id
	@GeneratedValue
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

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
