package com.ts.dt.match.action.foul;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.action.Action;

public interface Foul extends Action {
	public void execute(MatchContext context) throws MatchException;
}
