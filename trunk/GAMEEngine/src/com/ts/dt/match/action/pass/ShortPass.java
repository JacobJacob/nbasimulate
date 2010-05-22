package com.ts.dt.match.action.pass;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.PassResultCheckFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.po.Player;
import com.ts.dt.util.MessagesUtil;

public class ShortPass implements Pass {

	public String checkResult(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String after(MatchContext context) throws MatchException {
		// TODO Auto-generated method stub
		String currtPlayerNm = context.getCurrentController().getPlayerName();
		String nextPlayerNm = context.getNextController().getPlayerName();

		String result = null;

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtDefenderNm = context.getCurrentDefender().getPlayerName();
		String previousPlayerNm = "";
		if (context.getPreviousController() != null) {
			previousPlayerNm = context.getPreviousController().getPlayerName();
		}

		String desc = description.load(context);
		if (desc == null) {
			// Logger.error("desc is null");
		}
		if (currtPlayerNm.trim().equals(currtDefenderNm.trim())) {
			// Logger.error("why occor this?");
		}
		if (currtPlayerNm.trim().equals(previousPlayerNm.trim())) {
			// Logger.error("why occor this?");
		}
		desc = desc.replace("~1~", currtPlayerNm.trim());
		desc = desc.replace("~2~", currtDefenderNm.trim());
		desc = desc.replace("~3~", previousPlayerNm.trim());
		desc = desc.replace("~4~", nextPlayerNm.trim());

		if (!context.getPassActionResult().equals(MatchConstant.RESULT_SUCCESS)) {
			MessagesUtil.showLine(context, desc);
			MatchInfoHelper.save(context, desc);
			if (context.getPassActionResult().equals(MatchConstant.RESULT_FAILURE_BE_STEAL)) {
				this.handleBeSteal(context);
			}
		} else {
			MatchInfoHelper.save(context, desc);
		}

		return result;
	}

	public String before(MatchContext context) {
		// TODO Auto-generated method stub
		context.setCurrentAction(this);
		PassResultCheckFactory.getInstance().createResultCheck(context).check(context);
		return null;
	}

	// 被断时做技术统计
	public void handleBeSteal(MatchContext context) {

		// 控球者失误加1
		String currentControllerNm = context.getCurrentController().getControllerName();
		Player currentControllerPlayer = context.getCurrentController().getPlayer();
		context.playerLapsusTimesInc(currentControllerPlayer, currentControllerNm.endsWith("A"));

		// 防守者抢断加1
		String currentDefenderNm = context.getCurrentDefender().getControllerName();
		Player currentDefenderPlayer = context.getCurrentDefender().getPlayer();
		context.playerStealsTimesInc(currentDefenderPlayer, currentDefenderNm.endsWith("A"));
	}

}
