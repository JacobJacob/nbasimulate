package com.ts.dt.match.action.substitution;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;

public interface Substitution extends Action {
	public String execute(MatchContext context);
}
