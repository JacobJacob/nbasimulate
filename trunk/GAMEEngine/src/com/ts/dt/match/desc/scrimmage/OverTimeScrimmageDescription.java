package com.ts.dt.match.desc.scrimmage;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class OverTimeScrimmageDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = "";
		desc = "~1~��~2~����,~3~�����˻���,~4~�õ���,������ʼ";
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
