package com.ts.dt.match.check.foul;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;

public interface FoulCheck {
	public void check(MatchContext context) throws MatchException;
}
