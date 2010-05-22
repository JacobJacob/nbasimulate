package com.ts.dt.match.action.scrimmage;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.ScrimmageResultCheckFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.util.MessagesUtil;

//开场的争球
public class StartScrimmage implements Scrimmage {

	public String before(MatchContext context) {
		// TODO Auto-generated method stub
		String result = null;
		context.setCurrentAction(this);
		ScrimmageResultCheckFactory.getInstance().createResultCheck(context).check(context);

		return result;
	}

	public String after(MatchContext context) throws MatchException {
		// TODO Auto-generated method stub
		String result = null;
		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtPlayerNm = context.getCurrentController().getPlayer().getName();
		String currtDefenderNm = context.getCurrentDefender().getPlayer().getName();

		String successerNm = null;
		if (context.getScrimmageResult().equals(MatchConstant.RESULT_SUCCESS)) {
			// 主队争球成功
			context.setHomeStart(true);
			successerNm = currtPlayerNm;
		} else {
			// 客队争球成功
			context.setHomeStart(false);
			successerNm = currtDefenderNm;
		}
		String nextControllerNm = context.getNextController().getPlayer().getName();

		String desc = description.load(context);
		desc = desc.replace("~1~", currtPlayerNm.trim());
		desc = desc.replace("~2~", currtDefenderNm.trim());
		desc = desc.replace("~3~", successerNm.trim());
		desc = desc.replaceAll("~4~", nextControllerNm.trim());

		MessagesUtil.showLine(context, desc);
		MatchInfoHelper.save(context, desc);
		return result;
	}
}
