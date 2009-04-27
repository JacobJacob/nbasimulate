package com.ts.dt.dao.impl;

import com.dt.bottle.persistence.Persistable;
import com.ts.dt.dao.UserDao;
import com.ts.dt.po.User;

public class UserDaoImp implements UserDao {

	public void delete(Persistable persist) {
		// TODO Auto-generated method stub

	}

	public Object load(long ipkey) {
		// TODO Auto-generated method stub
		return null;
	}

	public void save(Persistable persist) {
		// TODO Auto-generated method stub
        User user = (User)persist;
        user.save();
	}

	public void update(Persistable persist) {
		// TODO Auto-generated method stub

	}

}
