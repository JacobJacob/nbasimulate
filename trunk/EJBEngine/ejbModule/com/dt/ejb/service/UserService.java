package com.dt.ejb.service;

import com.dt.ejb.common.ICommand;
import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;
import com.dt.ejb.dao.UserDao;
import com.dt.ejb.dao.imp.UserDaoImp;
import com.dt.ejb.entity.User;

public class UserService implements ICommand{

	private Object persistenceManager;
	
	public UserService(Object persistenceManager){
		this.persistenceManager = persistenceManager;
	}
	public void execute(RequestContext reqCtx, ResponseContext resCtx) {
		// TODO Auto-generated method stub
		User user = new User();
	    user.setUserName("Jacky");
	    user.setPassword("821015");
	    UserDao userDao = new UserDaoImp(persistenceManager);
	    userDao.save(user);
	}
   
}
