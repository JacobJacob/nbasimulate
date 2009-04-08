package com.ts.dt.facade;

import com.ts.dt.context.RequestContext;
import com.ts.dt.context.ResponseContext;

public interface ServiceFacadeBean {
   public ResponseContext processRequest(RequestContext reqCtx);
}
