package com.dt.ejb.common;

import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;

public interface ICommand {
  
  public void execute(RequestContext reqCtx,ResponseContext resCtx);
}
