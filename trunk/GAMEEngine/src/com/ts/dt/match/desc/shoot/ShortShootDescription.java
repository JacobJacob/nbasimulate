package com.ts.dt.match.desc.shoot;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.loader.ActionDescLoaderImpl;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.po.ActionDesc;

public class ShortShootDescription implements ActionDescription {

	public String load(MatchContext context) throws MatchException {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			desc = success(context);
		} else {
			desc = failure(context);
		}
		return desc;
	}

	public String failure(MatchContext context) throws MatchException {
		// TODO Auto-generated method stub

		String flg = null;
		if (context.isFoul()) {
			flg = "foul";
		} else {
			flg = "not_foul";
		}

		ActionDesc actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("ShortShoot", "failure", flg);
		if (actionDesc.getNotStick() == true) {
			context.setNotStick(true);
		}
		return actionDesc.getActionDesc();
	}

	public String success(MatchContext context) throws MatchException {
		// TODO Auto-generated method stub

		String flg = null;
		if (context.isFoul()) {
			flg = "foul";
		} else {
			flg = "not_foul";
		}

		ActionDesc actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("ShortShoot", "success", flg);

		// 看是不是一个助攻球
		if (actionDesc.getIsAssist() == true) {
			context.setAssist(true);
		}

		return actionDesc.getActionDesc();
	}

}
