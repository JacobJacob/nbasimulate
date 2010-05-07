package com.ts.dt.match.check.rebound;

import com.ts.dt.context.MatchContext;

public interface BackboardCheck {

	public int defensiveReboundInc = 65; // 后场篮板加权

	public void check(MatchContext context);
}
