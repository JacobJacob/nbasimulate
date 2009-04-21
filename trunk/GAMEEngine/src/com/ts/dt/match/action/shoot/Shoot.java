package com.ts.dt.match.action.shoot;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;

public interface Shoot extends Action{
    
	public String execute(MatchContext context);
	
}
