package com.ts.dt.match;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.BackboardCheckFactory;
import com.ts.dt.helper.FoulHelper;
import com.ts.dt.helper.RandomCheckHelper;
import com.ts.dt.helper.ReboundHelper;
import com.ts.dt.match.action.pass.Pass;
import com.ts.dt.match.action.scrimmage.Scrimmage;
import com.ts.dt.match.action.shoot.CatchSlamDunk;
import com.ts.dt.match.action.shoot.FoulShoot;
import com.ts.dt.match.action.shoot.SlamDunk;
import com.ts.dt.po.Player;

public class NodosityEngine {

	private MatchContext context;

	public NodosityEngine(MatchContext context) {
		this.context = context;
	}

	public void execute() {

		Controller currentController = context.getCurrentController();
		if (context.getCurrentDefender() == null) {
			checkCurrentDefender();
		}

		if (context.getCurrentActionType() == MatchConstant.NULL_INTEGER) {
			checkCurrentActionType();
		}
		int action = context.getCurrentActionType();

		// 投球动作
		if (action == MatchConstant.ACTION_TYPE_SHOUT) {
			if (context.currentOffensiveTimeOut()) {
				// 做进攻24秒处理
				currentController.foul(context);
				checkNextController(MatchConstant.ACTION_TYPE_FOUL);
			} else {
				currentController.shout(context);
				// 如果是球进,犯规或者出界则不做篮板球争夺处理
				if (!context.isFoul() && !context.isSuccess() && !context.isOutside()) {
					BackboardCheckFactory.getInstance().createBackboardCheckFactory(context).check(context);
				}
				checkNextController(MatchConstant.ACTION_TYPE_SHOUT);
				context.put(MatchConstant.HAS_PASS_TIMES, 0);
			}
			// 传球动作
		} else if (action == MatchConstant.ACTION_TYPE_PASS) {
			if (context.currentOffensiveTimeOut()) {
				currentController.foul(context);
				checkNextController(MatchConstant.ACTION_TYPE_FOUL);
			} else {
				currentController.pass(context);
				checkNextController(MatchConstant.ACTION_TYPE_PASS);
				Pass afterPass = (Pass) context.getCurrentAction();
				afterPass.after(context);
				int hasPassTimes = (Integer) context.get(MatchConstant.HAS_PASS_TIMES);
				hasPassTimes++;
				context.put(MatchConstant.HAS_PASS_TIMES, hasPassTimes);
			}
			// 抢篮板动作
		} else if (action == MatchConstant.ACTION_TYPE_REBOUND) {
			currentController.loose(context);
			checkNextController(MatchConstant.ACTION_TYPE_REBOUND);
			// 发球动作
		} else if (action == MatchConstant.ACTION_TYPE_SERVICE) {
			currentController.service(context);
			checkNextController(MatchConstant.ACTION_TYPE_SERVICE);
			// 争球动作
		} else if (action == MatchConstant.ACTION_TYPE_SCRIMMAGE) {
			currentController.scrimmage(context);
			checkNextController(MatchConstant.ACTION_TYPE_SCRIMMAGE);
			Scrimmage scrimmage = (Scrimmage) context.getCurrentAction();
			scrimmage.after(context);
		}
		checkNextActionType();
		checkNextDefender();
	}

	public void next() {

		Controller currentController = context.getCurrentController();
		Controller nextController = context.getNextController();
		context.setPreviousController(currentController);
		context.setCurrentController(nextController);
		context.setBallRight();

		Controller nextDefender = context.getNextDefender();
		context.setCurrentDefender(nextDefender);

		int nextActionType = context.getNextActionType();

		context.setCurrentActionType(nextActionType);
		context.setNextActionType(MatchConstant.NULL_INTEGER);

		context.setFoul(false);
		context.setOutside(false);

	}

	public void checkNextController(int action) {

		Controller nextController = null;
		if (action == MatchConstant.ACTION_TYPE_PASS) {
			nextController = checkNextControllerForPassAction();
		} else if (action == MatchConstant.ACTION_TYPE_SHOUT) {
			nextController = checkNextControllerForShootAction();
		} else if (action == MatchConstant.ACTION_TYPE_REBOUND) {
			nextController = checkNextControllerForReboundAction();
		} else if (action == MatchConstant.ACTION_TYPE_SERVICE) {
			nextController = checkNextControllerForServiceAction();
		} else if (action == MatchConstant.ACTION_TYPE_SCRIMMAGE) {
			nextController = checkNextControllerForScrimmageAction();
		} else if (action == MatchConstant.ACTION_TYPE_FOUL) {
			nextController = checkNextControllerForOffensiveTimeOut();
		} else {
			System.out.println("error");
		}
		context.setNextController(nextController);
	}

	public void checkNextDefender() {

		Controller nextController = context.getNextController();
		String nextControllerNm = nextController.getControllerName();
		int pg = 0;
		int sg = 0;
		int sf = 0;
		int pf = 0;
		int c = 0;

		if (nextControllerNm.startsWith(MatchConstant.LOCATION_PG)) {
			pg = 70;
			sg = 15;
			sf = 5;
			pf = 5;
			c = 5;
		} else if (nextControllerNm.startsWith(MatchConstant.LOCATION_SG)) {
			pg = 15;
			sg = 70;
			sf = 5;
			pf = 5;
			c = 5;
		} else if (nextControllerNm.startsWith(MatchConstant.LOCATION_SF)) {
			pg = 5;
			sg = 10;
			sf = 70;
			pf = 10;
			c = 5;
		} else if (nextControllerNm.startsWith(MatchConstant.LOCATION_PF)) {
			pg = 5;
			sg = 5;
			sf = 15;
			pf = 70;
			c = 5;
		} else {
			pg = 5;
			sg = 5;
			sf = 10;
			pf = 15;
			c = 65;
		}
		Random random = new Random();
		int a = random.nextInt(pg + sg + sf + pf + c);
		int index = -1;
		if (a < pg) {
			index = 1;
		} else if (a < pg + sg) {
			index = 2;
		} else if (a < pg + sg + sf) {
			index = 3;
		} else if (a < pg + sg + sf + pf) {
			index = 4;
		} else {
			index = 5;
		}
		if (nextControllerNm.endsWith("A")) {
			index += 5;
		}
		Controller nextefender = context.getControllerWithIndx(index);
		context.setNextDefender(nextefender);
	}

	public void checkCurrentDefender() {

		Controller currentController = context.getCurrentController();
		String currentControllerNm = currentController.getControllerName();
		int pg = 0;
		int sg = 0;
		int sf = 0;
		int pf = 0;
		int c = 0;

		if (currentControllerNm.startsWith(MatchConstant.LOCATION_PG)) {
			pg = 70;
			sg = 15;
			sf = 5;
			pf = 5;
			c = 5;
		} else if (currentControllerNm.startsWith(MatchConstant.LOCATION_SG)) {
			pg = 15;
			sg = 70;
			sf = 5;
			pf = 5;
			c = 5;
		} else if (currentControllerNm.startsWith(MatchConstant.LOCATION_SF)) {
			pg = 5;
			sg = 10;
			sf = 70;
			pf = 10;
			c = 5;
		} else if (currentControllerNm.startsWith(MatchConstant.LOCATION_PF)) {
			pg = 5;
			sg = 5;
			sf = 15;
			pf = 70;
			c = 5;
		} else {
			pg = 5;
			sg = 5;
			sf = 10;
			pf = 15;
			c = 65;
		}
		Random random = new Random();
		int a = random.nextInt(pg + sg + sf + pf + c);
		int index = -1;
		if (a < pg) {
			index = 1;
		} else if (a < pg + sg) {
			index = 2;
		} else if (a < pg + sg + sf) {
			index = 3;
		} else if (a < pg + sg + sf + pf) {
			index = 4;
		} else {
			index = 5;
		}
		if (currentControllerNm.endsWith("A")) {
			index += 5;
		}
		Controller currentDefender = context.getControllerWithIndx(index);
		context.setCurrentDefender(currentDefender);
	}

	public Controller checkNextControllerForServiceAction() {

		Controller controller;

		String currentControllerNm = context.getCurrentController().getControllerName();

		int index = -1;
		Random random = new Random();
		int a = random.nextInt(100);

		if (a < 20) {
			index = 1;
		} else if (a < 40) {
			index = 2;
		} else if (a < 60) {
			index = 3;
		} else if (a < 80) {
			index = 4;
		} else {
			index = 5;
		}
		if (currentControllerNm.endsWith("B")) {
			index += 5;
		}
		controller = context.getControllerWithIndx(index);

		if (controller.getControllerName().equals(currentControllerNm)) {
			controller = checkNextControllerForServiceAction();
		}

		context.setNextController(controller);
		return controller;
	}

	// 判断争球后下一个控球者
	public Controller checkNextControllerForScrimmageAction() {

		Controller controller;

		String currentControllerNm = context.getCurrentController().getControllerName();
		String currentDefenderNm = context.getCurrentDefender().getControllerName();

		int index = -1;
		Random random = new Random();
		int a = random.nextInt(100);

		if (a < 20) {
			index = 1;
		} else if (a < 40) {
			index = 2;
		} else if (a < 60) {
			index = 3;
		} else if (a < 80) {
			index = 4;
		} else {
			index = 5;
		}
		// 如果不成功,则是对方拿到球
		if (!context.getScrimmageResult().equals(MatchConstant.RESULT_SUCCESS)) {
			index += 5;
		}
		controller = context.getControllerWithIndx(index);

		if (controller.getControllerName().equals(currentControllerNm)
				|| controller.getControllerName().equals(currentDefenderNm)) {
			controller = checkNextControllerForScrimmageAction();
		}

		context.setNextController(controller);
		return controller;
	}

	public Controller checkNextControllerForPassAction() {

		Controller controller;

		String currentControllerNm = context.getCurrentController().getControllerName();

		String passResult = context.getPassActionResult();

		if (MatchConstant.RESULT_FAILURE_OUTSIDE.equals(passResult)) {
			controller = checkNextControllerForPassIsOutSide();
		} else if (MatchConstant.RESULT_FAILURE_BE_STEAL.equals(passResult)) {
			controller = checkNextControllerForPassBeSteal();
		} else {
			int index = -1;
			Random random = new Random();
			int a = random.nextInt(100);

			if (a < 20) {
				index = 1;
			} else if (a < 40) {
				index = 2;
			} else if (a < 60) {
				index = 3;
			} else if (a < 80) {
				index = 4;
			} else {
				index = 5;
			}
			if (currentControllerNm.endsWith("B")) {
				index += 5;
			}
			controller = context.getControllerWithIndx(index);

			if (controller.getControllerName().equals(currentControllerNm)) {
				controller = checkNextControllerForPassAction();
			}
			context.setNextController(controller);
			checkSlamDunk();
		}
		context.setNextController(controller);
		return controller;

	}

	public Controller checkNextControllerForPassBeSteal() {
		Controller controller;
		controller = context.getCurrentDefender();
		context.setNextActionType(MatchConstant.ACTION_TYPE_PASS);
		return controller;
	}

	public Controller checkNextControllerForPassIsOutSide() {
		Controller controller;
		if (context.isHomeTeam()) {
			controller = context.getControllerWithIndx(MatchConstant.PG_B);
		} else {
			controller = context.getControllerWithIndx(MatchConstant.PG_A);
		}
		context.setNextActionType(MatchConstant.ACTION_TYPE_SERVICE);
		return controller;
	}

	public Controller checkNextControllerForOffensiveTimeOut() {
		Controller controller;
		if (context.isHomeTeam()) {
			controller = context.getControllerWithIndx(MatchConstant.PG_B);
		} else {
			controller = context.getControllerWithIndx(MatchConstant.PG_A);
		}
		context.setNextActionType(MatchConstant.ACTION_TYPE_SERVICE);
		return controller;
	}
    //判断投篮动作后的下一个控球者
	public Controller checkNextControllerForShootAction() {

		Controller controller = null;
		//如果是罚篮,并且尚未罚完,则继续
		if (context.getFoulShootRemain() > 0) {
			controller = context.getCurrentController();
			context.setNextActionType(MatchConstant.ACTION_TYPE_SHOUT);
			context.setNextAction(new FoulShoot());
			return controller;
		}
		//如果投篮投中,则是对方控卫发球
		if (context.getShootActionResult() == MatchConstant.RESULT_SUCCESS) {
			if (context.isHomeTeam()) {
				controller = (Controller) context.getControllers().get("PGB");
			} else {
				controller = (Controller) context.getControllers().get("PGA");
			}
			context.setNextActionType(MatchConstant.ACTION_TYPE_SERVICE);
			return controller;
		}

		int index = -1;
		Random random = new Random();
		int[] percent = ReboundHelper.checkPercentForGetRebound(context.getControllers(), context.isHomeTeam());

		int totalPercent = percent[0] + percent[1] + percent[2] + percent[3] + percent[4];

		int a = random.nextInt(totalPercent);

		if (a < percent[0]) {
			index = 1;
		} else if (a < (percent[0] + percent[1])) {
			index = 2;
		} else if (a < (percent[0] + percent[1] + percent[2])) {
			index = 3;
		} else if (a < (percent[0] + percent[1] + percent[2] + percent[3])) {
			index = 4;
		} else {
			index = 5;
		}
		if (context.isDefensiveRebound() && context.isHomeTeam()) {
			index += 5;
		}
		if (context.isOffensiveRebound() && !context.isHomeTeam()) {
			index += 5;
		}
		controller = context.getControllerWithIndx(index);

		return controller;

	}

	public Controller checkNextControllerForReboundAction() {

		Controller controller = null;

		Random random = new Random();

		int i = random.nextInt(100);
		if (i < 20) {
			context.setNextActionType(MatchConstant.ACTION_TYPE_SHOUT);
		} else {
			context.setNextActionType(MatchConstant.ACTION_TYPE_PASS);
		}
		controller = context.getCurrentController();
		return controller;

	}

	// 判断下一动作是扣篮的可能性
	public void checkSlamDunk() {

		String nextContrNm = context.getNextController().getControllerName();
		Player player = context.getNextController().getPlayer();
		int feasibility = 0;
		long height = player.getAvoirdupois();
		// long bounce = 0;
		if (height < 185) {
			return;
		}
		if (nextContrNm.startsWith("PG")) {
			feasibility += 1;
		} else if (nextContrNm.startsWith("SG")) {
			feasibility += 3;
		} else if (nextContrNm.startsWith("SF")) {
			feasibility += 4;
		} else if (nextContrNm.startsWith("PF")) {
			feasibility += 7;
		} else {
			feasibility += 10;
		}
		feasibility += (height - 150) / 5;

		Random random = new Random();
		int r = random.nextInt(100);

		if (r < feasibility) {
			context.setNextAction(new CatchSlamDunk());
		}
	}

	// check whether 
	public void checkWhetherFoulBeforeShoot() {
		int percent = FoulHelper.checkDefensiveFoulAfterShoot(context);
		if (RandomCheckHelper.defaultCheck(percent)) {
			context.setFoul(true);
		}
	}

	public void checkNextActionType() {

		int hasPassTimes = (Integer) context.get(MatchConstant.HAS_PASS_TIMES);

		int actionType = -1;

		Random random = new Random();
		int a = random.nextInt(100);

		a += (3 - hasPassTimes) * 10;

		if (context.getNextAction() != null && context.getNextAction() instanceof SlamDunk) {
			actionType = MatchConstant.ACTION_TYPE_SHOUT;

		} else if (context.getNextActionType() != MatchConstant.NULL_INTEGER) {
			actionType = context.getNextActionType();
		} else if (context.isDefensiveRebound() || context.isOffensiveRebound()) {
			actionType = MatchConstant.ACTION_TYPE_REBOUND;

		} else if (a > 50) {
			actionType = MatchConstant.ACTION_TYPE_PASS;
		} else {
			actionType = MatchConstant.ACTION_TYPE_SHOUT;
		}
		context.setNextActionType(actionType);
	}

	public void checkCurrentActionType() {

		int hasPassTimes = (Integer) context.get(MatchConstant.HAS_PASS_TIMES);

		int actionType = -1;

		Random random = new Random();
		int a = random.nextInt(100);

		a += (3 - hasPassTimes) * 10;

		if (a > 50) {
			actionType = MatchConstant.ACTION_TYPE_PASS;
		} else {
			actionType = MatchConstant.ACTION_TYPE_SHOUT;
		}
		context.setCurrentActionType(actionType);
	}
}
