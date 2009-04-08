package com.dt.ejb.dao;

import javax.persistence.EntityManager;

public class DaoUtil {

	private EntityManager em;

	public DaoUtil(Object em) {
		if(em instanceof EntityManager) {
			this.em = (EntityManager)em	;
		}
	}

	public void save(Object object) {
		em.persist(object);
	}

}
