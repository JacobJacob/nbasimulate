package com.ts.dt.match.action.pass;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;

public interface Pass extends Action {

	public String before(MatchContext context);

	public String after(MatchContext context);

	public String checkResult(MatchContext context);
}
