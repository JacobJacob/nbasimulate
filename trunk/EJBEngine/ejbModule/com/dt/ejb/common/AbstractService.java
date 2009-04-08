package com.dt.ejb.common;

public abstract class AbstractService {

	public AbstractService(Object persistenceManager) {
		this.persistenceManager = persistenceManager;
	}

	protected Object persistenceManager;
}
