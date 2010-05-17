package com.ts.dt.match.action.shoot;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.factory.BlockCheckFactory;
import com.ts.dt.factory.FoulCheckFactory;
import com.ts.dt.factory.ShootResultCheckFactory;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.check.block.BlockCheck;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.po.Player;
import com.ts.dt.util.Logger;
import com.ts.dt.util.MessagesUtil;

public abstract class AbstractShoot implements Action {

	public String execute(MatchContext context) throws MatchException {

		String result = null;

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtContrNm = context.getCurrentController().getControllerName();
		String currtPlayerNm = context.getCurrentController().getPlayerName();
		Player player = context.getCurrentController().getPlayer();
		String currtDefenderNm = context.getCurrentDefender().getPlayerName();
		String previousPlayerNm = "";
		if (context.getPreviousController() != null) {
			previousPlayerNm = context.getPreviousController().getPlayerName();
		}
		// �ж��Ƿ񱻷��
		BlockCheck blockCheck = BlockCheckFactory.getInstance().createBlockCheckFactory(context);
		blockCheck.check(context);
		// check the shoot result
		if (context.isBlock()) {
			this.handleBlock(context);
			context.setShootActionResult(MatchConstant.RESULT_FAILURE_BLOCKED);
		} else {
			ShootResultCheckFactory.getInstance().createResultCheck(context).check(context);
			// if (!context.isSuccess()) {
			// �������,�ж�һ���Ƿ񷸹�
			FoulCheckFactory.getInstance().createFoulCheckFactory(context).check(context);
			// }
		}

		String desc = description.load(context);
		if (desc == null) {
			throw new MatchException("��������Ϊ��");
		}

		String currentTeamNm = context.getCurrentController().getTeamFlg();
		String previousTeamNm = "";
		if (context.getPreviousController() != null) {
			previousTeamNm = context.getPreviousController().getTeamFlg();
		}

		if (!currentTeamNm.equals(previousTeamNm)) {
			Logger.logToDb("error", "while occor same?");
		}

		String currentShootNo = "";
		// �����Ƿ���
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

		desc = desc.replace("~1~", currtPlayerNm.trim()); // ��ǰ�����Ա
		desc = desc.replace("~2~", currtDefenderNm.trim()); // ��ǰ���ض�Ա
		desc = desc.replace("~3~", previousPlayerNm.trim()); // ��һ��������Ա
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

		context.setNewLine(false);

		// �ҹ�ͳ��
		if (context.isAssist()) {
			handleAssit(context);
			context.setAssist(false);
		}

		// �����������
		MatchInfoHelper.save(context, desc);
		MessagesUtil.showLine(context, desc);
		return result;
	}

	// ������ͳ��
	final private void handleAssit(MatchContext context) {

		String previousControllerNm = context.getPreviousController().getControllerName();
		int previousActionType = context.getPreviousActionType();
		if (previousActionType == MatchConstant.ACTION_TYPE_PASS) {
			Player previousPlayer = context.getPreviousController().getPlayer();
			context.playerAssitTimesInc(previousPlayer, previousControllerNm.endsWith("A"));
		}
	}

	// �����ͳ��
	final private void handleBlock(MatchContext context) {

		String currentDefenderNm = context.getCurrentDefender().getControllerName();
		Player currentDefenderPlayer = context.getCurrentDefender().getPlayer();
		context.playerBlockTimesInc(currentDefenderPlayer, currentDefenderNm.endsWith("A"));
		context.setBlock(false);

	}
}
