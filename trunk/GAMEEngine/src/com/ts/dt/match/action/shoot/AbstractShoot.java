package com.ts.dt.match.action.shoot;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.FoulCheckFactory;
import com.ts.dt.factory.ResultCheckFactory;
import com.ts.dt.helper.MatchInfoHelper;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.desc.ActionDescription;
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

		// 判断投篮结果
		ResultCheckFactory.getInstance().createResultCheck(context).check(context);
		// 判断是否犯规
		FoulCheckFactory.getInstance().createFoulCheckFactory(context).check(context);

		String desc = description.load(context);
		if (desc == null) {
			//Logger.error("desc is null");
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

		Action currentAction = context.getCurrentAction();
		if (currentAction instanceof LongShoot) {
			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_THREE_POINT, currtContrNm.endsWith("A"));
				context.doomTimesInc(MatchConstant.INC_THREE_POINT, currtContrNm.endsWith("A"));
				context.playerDoomTimesInc(MatchConstant.INC_THREE_POINT, player, currtContrNm.endsWith("A"));
			}
			context.shootTimesInc(MatchConstant.INC_THREE_POINT, currtContrNm.endsWith("A"));
			context.playerShootTimesInc(MatchConstant.INC_THREE_POINT, player, currtContrNm.endsWith("A"));
		} else if (currentAction instanceof FoulShoot) {
			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_ONE_POINT, currtContrNm.endsWith("A"));
				context.doomTimesInc(MatchConstant.INC_ONE_POINT, currtContrNm.endsWith("A"));
				context.playerDoomTimesInc(MatchConstant.INC_ONE_POINT, player, currtContrNm.endsWith("A"));
			}
			context.shootTimesInc(MatchConstant.INC_ONE_POINT, currtContrNm.endsWith("A"));
			context.playerShootTimesInc(MatchConstant.INC_ONE_POINT, player, currtContrNm.endsWith("A"));
			context.foulShootRemainDec();
		} else {

			if (context.isSuccess()) {
				context.pointInc(MatchConstant.INC_TWO_POINT, currtContrNm.endsWith("A"));
				context.doomTimesInc(MatchConstant.INC_TWO_POINT, currtContrNm.endsWith("A"));
				context.playerDoomTimesInc(MatchConstant.INC_TWO_POINT, player, currtContrNm.endsWith("A"));
			}
			context.shootTimesInc(MatchConstant.INC_TWO_POINT, currtContrNm.endsWith("A"));
			context.playerShootTimesInc(MatchConstant.INC_TWO_POINT, player, currtContrNm.endsWith("A"));
		}

		MatchInfoHelper.save(context,desc);

		MessagesUtil.showLine(context, desc);

		return result;
	}
}
