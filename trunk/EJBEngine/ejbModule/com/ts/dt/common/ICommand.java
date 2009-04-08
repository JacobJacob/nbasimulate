package com.ts.dt.common;

import com.ts.dt.context.RequestContext;
import com.ts.dt.context.ResponseContext;

public interface ICommand {
	
   public void execute(RequestContext reqCxt,ResponseContext resCxt);
}
