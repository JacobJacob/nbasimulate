package com.ts.dt.match.action.service;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.action.Action;

public interface Service extends Action {
	public void service(MatchContext context) throws MatchException;
}
