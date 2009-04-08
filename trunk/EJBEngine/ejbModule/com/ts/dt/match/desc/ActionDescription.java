package com.ts.dt.match.desc;

import com.ts.dt.context.MatchContext;

public interface ActionDescription {
	public String load(MatchContext context);

	public String success(MatchContext context);

	public String failure(MatchContext context);
}
