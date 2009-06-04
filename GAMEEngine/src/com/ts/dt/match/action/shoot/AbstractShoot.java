package com.ts.dt.match.action.shoot;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.BlockCheckFactory;
import com.ts.dt.factory.FoulCheckFactory;
import com.ts.dt.factory.ResultCheckFactory;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.po.Player;
import com.ts.dt.util.MessagesUtil;

public abstract class AbstractShoot implements Action {

	public String execute(MatchContext context) {

		String result = null;

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtContrNm = context.getCurrentController().getControllerName();
		String currtPlayerNm = context.getCurrentController().getPlayer().getName();
		Player player = context.getCurrentController().getPlayer();
		String currtDefenderNm = context.getCurrentDefender().getPlayer().getName();
		String previousPlayerNm = "";
		if (context.getPreviousController() != null) {
			previousPlayerNm = context.getPreviousController().getPlayer().getName();
		}
		// check whether block
		BlockCheckFactory.getInstance().createBlockCheckFactory(context);
		// check the shoot result
		if (context.isBlock()) {
			context.setShootActionResult(MatchConstant.RESULT_FAILURE_BLOCKED);
		} else {
			ResultCheckFactory.getInstance().createResultCheck(context).check(context);
			if (!context.isSuccess()) {
				// check whether foul
				FoulCheckFactory.getInstance().createFoulCheckFactory(context).check(context);
			}
		}

		String desc = description.load(context);
		if (desc == null) {
			// Logger.error("desc is null");
		}
		String currentTeamNm = context.getCurrentController().getTeamFlg();
		String previousTeamNm = "";
		if (context.getPreviousController() != null) {
			previousTeamNm = context.getPreviousController().getTeamFlg();
		}

		if (!currentTeamNm.equals(previousTeamNm)) {
			// Logger.error("while occor same?");
		}

		String currentShootNo = "";
		if (this instanceof FoulShoot) {
			int remainFoulShoot = context.getFoulShootRemain();
			if (context.getFoulShootType() == MatchConstant.FOUL_SHOOT_TYPE_TWO) {
				if (remainFoulShoot == 2) {
					currentShootNo = "1";
				} else {
					currentShootNo = "2";
				}
			} else {
				if (remainFoulShoot == 3) {
					currentShootNo = "1";
				} else if (remainFoulShoot == 2) {
					currentShootNo = "2";
				} else {
					currentShootNo = "3";
				}
			}
		}

		desc = desc.replace("~1~", currtPlayerNm.trim());
		desc = desc.replace("~2~", currtDefenderNm.trim());
		desc = desc.replace("~3~", previousPlayerNm.trim());
		desc = desc.replace("~6~", currentShootNo);

		boolean isHomeTeam = currtContrNm.endsWith("A");

		Action currentAction = context.getCurrentAction();
		if (currentAction instanceof LongShoot) {
			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_THREE_POINT, isHomeTeam);
				context.doomTimesInc(MatchConstant.INC_THREE_POINT, isHomeTeam);
				context.playerDoomTimesInc(MatchConstant.INC_THREE_POINT, player, isHomeTeam);
			}
			context.shootTimesInc(MatchConstant.INC_THREE_POINT, isHomeTeam);
			context.playerShootTimesInc(MatchConstant.INC_THREE_POINT, player, isHomeTeam);
		} else if (currentAction instanceof FoulShoot) {
			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_ONE_POINT, isHomeTeam);
				context.doomTimesInc(MatchConstant.INC_ONE_POINT, isHomeTeam);
				context.playerDoomTimesInc(MatchConstant.INC_ONE_POINT, player, isHomeTeam);
			}
			context.shootTimesInc(MatchConstant.INC_ONE_POINT, isHomeTeam);
			context.playerShootTimesInc(MatchConstant.INC_ONE_POINT, player, isHomeTeam);
			context.foulShootRemainDec();
		} else {

			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_TWO_POINT, isHomeTeam);
				context.doomTimesInc(MatchConstant.INC_TWO_POINT, isHomeTeam);
				context.playerDoomTimesInc(MatchConstant.INC_TWO_POINT, player, isHomeTeam);
			}
			context.shootTimesInc(MatchConstant.INC_TWO_POINT, isHomeTeam);
			context.playerShootTimesInc(MatchConstant.INC_TWO_POINT, player, isHomeTeam);

		}

		if (context.isAssist()) {
			// handle for assist
			handleAssit(context);
			context.setAssist(false);
		}

		MatchInfoHelper.save(context, desc);

		MessagesUtil.showLine(context, desc);

		return result;
	}

	final private void handleAssit(MatchContext context) {

		String previousControllerNm = context.getPreviousController().getControllerName();
		int previousActionType = context.getPreviousActionType();
		if (previousActionType == MatchConstant.ACTION_TYPE_PASS) {
			Player previousPlayer = context.getPreviousController().getPlayer();
			context.playerAssitTimesInc(previousPlayer, previousControllerNm.endsWith("A"));
		}
	}
}
