package com.ts.dt.match.desc;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;

public interface ActionDescription {
	public String load(MatchContext context) throws MatchException;

	public String success(MatchContext context) throws MatchException;

	public String failure(MatchContext context) throws MatchException;
}
