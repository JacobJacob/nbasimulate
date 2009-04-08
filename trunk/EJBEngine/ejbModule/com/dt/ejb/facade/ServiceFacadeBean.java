package com.dt.ejb.facade;

import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;

public interface ServiceFacadeBean {
	
    public ResponseContext processRequest(RequestContext reqCtx)throws Exception ;
}
