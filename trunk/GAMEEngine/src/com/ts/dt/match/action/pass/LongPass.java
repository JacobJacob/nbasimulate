package com.ts.dt.match.action.pass;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.PassResultCheckFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.po.Player;
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
		// String currentTeamNm = context.getCurrentController().getTeamFlg();
		// String previousTeamNm = "";
		// if (context.getPreviousController() != null) {
		// previousTeamNm = context.getPreviousController().getTeamFlg();
		// }

		desc = desc.replace(MatchConstant.CURRENT_PLAYER_NAME_PLACEHOLDER, currtPlayerNm.trim());
		desc = desc.replace(MatchConstant.CURRENT_DEFENDER_NAME_PLACEHOLDER, currtDefenderNm.trim());
		desc = desc.replace(MatchConstant.PREVIOUS_PLAYER_NAME_PLACEHOLDER, previousPlayerNm.trim());
		desc = desc.replace(MatchConstant.NEXT_PLAYER_NAME_PLACEHOLDER, nextPlayerNm.trim());

		if (!context.getPassActionResult().equals(MatchConstant.RESULT_SUCCESS)) {
			MessagesUtil.showLine(context, desc);
			MatchInfoHelper.save(context, desc);
			if (context.getPassActionResult().equals(MatchConstant.RESULT_FAILURE_BE_STEAL)) {
				this.handleBeSteal(context);
			}

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
