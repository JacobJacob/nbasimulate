package com.ts.dt.match.desc.rebound;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class OffensiveReboundDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;

		desc = "~1~判断准确,抢到一个进攻篮板!";
		return desc;
	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}
    
}
