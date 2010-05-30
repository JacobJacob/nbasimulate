package com.ts.dt.match.desc.scrimmage;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class OverTimeScrimmageDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = "";
		desc = "~1~和~2~争球,~3~将球拨了回来,~4~得到球,比赛开始";
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
