package com.ts.dt.match.check.pass;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.match.helper.PassHelper;
import com.ts.dt.match.helper.RandomCheckHelper;
import com.ts.dt.po.Player;

public class ShortPassCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		int percent = 90;
		Player currtPlayer = context.getCurrentController().getPlayer();
		Player currtDefender = context.getCurrentDefender().getPlayer();
		int passPower = PassHelper.checkPlayerPassPower(currtPlayer);
		int vsPassPower = PassHelper.checkPlayerPassPower(currtDefender);

		percent += (int) ((passPower - vsPassPower) / 10);
		if (RandomCheckHelper.defaultCheck(percent)) {
			context.setPassActionResult(MatchConstant.RESULT_SUCCESS);
		} else {
			if (RandomCheckHelper.defaultCheck(70)) {
				context.setPassActionResult(MatchConstant.RESULT_FAILURE_OUTSIDE);
			} else {
				//被断
				context.setPassActionResult(MatchConstant.RESULT_FAILURE_BE_STEAL);
			}
		}
	}
}
