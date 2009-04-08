package com.ts.dt.match.action.scrimmage;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;

public interface Scrimmage extends Action {
	public String before(MatchContext context);

	public String after(MatchContext context);
}
