package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.loader.ActionDescLoaderImpl;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.po.ActionDesc;

public class CatchSlamDunkDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			desc = success(context);
		} else if (MatchConstant.RESULT_FAILURE_BLOCKED.equals(result)) {
			desc = blocked(context);
		} else {
			desc = "~1~接到~3~的妙传,空中想直接扣篮,~2~跳起防守,干扰了~1~的扣篮,球没有进 . ";
		}

		return desc;
	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;

		ActionDesc actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("CatchSlamDunk", "success",
				"not_foul");

		if (actionDesc.getIsAssist() == true) {
			context.setAssist(true);
		}

		desc = actionDesc.getActionDesc();
		if (context.isFoul()) {
			desc += "同时造成了~2~的犯规!";
		}
		return desc;
	}

	public String blocked(MatchContext context) {

		ActionDesc actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("CatchSlamDunk", "failure",
				"blocked");
		return actionDesc.getActionDesc();
	}

}
