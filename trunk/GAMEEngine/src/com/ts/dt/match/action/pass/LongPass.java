package com.ts.dt.match.action.pass;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.ResultCheckFactory;
import com.ts.dt.helper.MatchInfoHelper;
import com.ts.dt.match.desc.ActionDescription;
//import com.ts.dt.util.Logger;
import com.ts.dt.util.MessagesUtil;

public class LongPass implements Pass {

	public String checkResult(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String after(MatchContext context) {
		// TODO Auto-generated method stub
		String currtPlayerNm = context.getCurrentController().getPlayer().getName();
		String nextPlayerNm = context.getNextController().getPlayer().getName();

		String result = null;

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtDefenderNm = context.getCurrentDefender().getPlayer().getName();
		String previousPlayerNm = "";
		if (context.getPreviousController() != null) {
			previousPlayerNm = context.getPreviousController().getPlayer().getName();
		}

		String desc = description.load(context);
		if (desc == null) {

		}
		String currentTeamNm = context.getCurrentController().getTeamFlg();
		String previousTeamNm = "";
		if (context.getPreviousController() != null) {
			previousTeamNm = context.getPreviousController().getTeamFlg();
		}

		desc = desc.replace("~1~", currtPlayerNm.trim());
		desc = desc.replace("~2~", currtDefenderNm.trim());
		desc = desc.replace("~3~", previousPlayerNm.trim());
		desc = desc.replace("~4~", nextPlayerNm.trim());

		if (!context.getPassActionResult().equals(MatchConstant.RESULT_SUCCESS)) {
			MessagesUtil.showLine(context, desc);
			MatchInfoHelper.save(context,desc);
		}

		return result;
	}

	public String before(MatchContext context) {
		// TODO Auto-generated method stub
		context.setCurrentAction(this);
		ResultCheckFactory.getInstance().createResultCheck(context).check(context);
		return null;
	}

}
