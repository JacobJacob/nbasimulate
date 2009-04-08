package com.dt.ejb.dao.imp;

import com.dt.ejb.dao.DaoUtil;
import com.dt.ejb.dao.UserDao;

public class UserDaoImp extends DaoUtil implements UserDao {

	public UserDaoImp(Object persistenceManager) {

		super(persistenceManager);
	}

}
