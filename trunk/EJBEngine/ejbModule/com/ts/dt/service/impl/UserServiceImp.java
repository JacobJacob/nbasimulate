package com.ts.dt.service.impl;

import javax.ejb.Remote;
import javax.ejb.Stateless;

import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;
import com.ts.dt.service.UserService;

@Stateless
@Remote({UserService.class})
public class UserServiceImp implements UserService {

	public void execute(RequestContext reqCxt, ResponseContext resCxt) {
		// TODO Auto-generated method stub

//		UserDaoImp userDaoImp = new UserDaoImp();
//		
//		User user = (User)reqCxt.getObject("user");
//		
//		Session session = Session.getInstance();
//		session.beginTransaction();
//		userDaoImp.save(user);
//		session.endTransaction();
//		
    

	}
	
	public static void main(String[] args){
		
		new UserServiceImp().execute(new RequestContext(), new ResponseContext());
		
	}
   
}
