package com.ts.dt.match.desc.shoot;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class FoulShootDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		if (context.getFoulShootType() == MatchConstant.FOUL_SHOOT_TYPE_ONE) {
			if (context.isSuccess()) {
				desc = "�ӷ�����";
			} else {
				desc = "�ӷ�����";
			}
		} else {
			if (context.isSuccess()) {
				desc = success(context);
			} else {
				desc = failure(context);
			}
		}
		return desc;
	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return "��~6~��û��!";
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		return "��~6~������!";
	}

}
