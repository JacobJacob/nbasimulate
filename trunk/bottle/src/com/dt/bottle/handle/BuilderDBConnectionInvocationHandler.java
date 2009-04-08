package com.dt.bottle.handle;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;

import com.dt.bottle.connector.DBConnection;

public class BuilderDBConnectionInvocationHandler implements InvocationHandler {
    
	private DBConnection conn ;
	private BuilderDBConnectionInvocationHandler(DBConnection conn){
		this.conn = conn;
	}
	public Object invoke(Object proxy, Method method, Object[] args)
			throws Throwable {
		// TODO Auto-generated method stub
		if ("close".equals(method.getName())) {
			
		}
		return method.invoke(conn, args);
	}

}
