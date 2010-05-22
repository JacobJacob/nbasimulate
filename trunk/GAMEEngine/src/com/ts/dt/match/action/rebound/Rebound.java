package com.ts.dt.match.action.rebound;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.action.Action;

public interface Rebound extends Action {
	public String execute(MatchContext context) throws MatchException;
}
