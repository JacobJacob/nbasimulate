package com.ts.dt.helper;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.Controller;

/*
 * check the defender
 */
public class DefenderHelper {

	private static final int[] DATA_FOR_PG = { 70, 15, 5, 5, 5 };
	private static final int[] DATA_FOR_SG = { 15, 70, 5, 5, 5 };
	private static final int[] DATA_FOR_SF = { 5, 10, 70, 10, 5 };
	private static final int[] DATA_FOR_PF = { 5, 5, 15, 70, 5 };
	private static final int[] DATA_FOR_C = { 5, 5, 10, 15, 65 };

	private static final int PG = 0;
	private static final int SG = 1;
	private static final int SF = 2;
	private static final int PF = 3;
	private static final int C = 4;

	/**
	 * check the next defender
	 * 
	 * @param context
	 */
	public static void checkNextDefender(MatchContext context) {
		Controller nextController = context.getNextController();
		Controller nextDefender = checkDefender(context, nextController);
		context.setNextDefender(nextDefender);
	}

	/**
	 * check the current defender
	 * 
	 * @param context
	 */
	public static void checkCurrentDefender(MatchContext context) {
		Controller currentController = context.getCurrentController();
		Controller currentDefender = checkDefender(context, currentController);
		context.setCurrentDefender(currentDefender);
	}

	private static Controller checkDefender(MatchContext context, Controller controller) {

		String controllerNm = controller.getControllerName();

		int[] temp;
		if (controllerNm.startsWith(MatchConstant.LOCATION_PG)) {
			temp = DATA_FOR_PG;
		} else if (controllerNm.startsWith(MatchConstant.LOCATION_SG)) {
			temp = DATA_FOR_SF;
		} else if (controllerNm.startsWith(MatchConstant.LOCATION_SF)) {
			temp = DATA_FOR_SG;
		} else if (controllerNm.startsWith(MatchConstant.LOCATION_PF)) {
			temp = DATA_FOR_PF;
		} else {
			temp = DATA_FOR_C;
		}
		Random random = new Random();
		int a = random.nextInt(temp[PG] + temp[SG] + temp[SF] + temp[PF] + temp[C]);
		int index = -1;
		if (a < temp[PG]) {
			index = 1;
		} else if (a < temp[PG] + temp[SG]) {
			index = 2;
		} else if (a < temp[PG] + temp[SG] + temp[SF]) {
			index = 3;
		} else if (a < temp[PG] + temp[SG] + temp[SF] + temp[PF]) {
			index = 4;
		} else {
			index = 5;
		}
		if (controllerNm.endsWith("A")) {
			index += 5;
		}
		Controller defender = context.getControllerWithIndx(index);
		return defender;
	}
}
