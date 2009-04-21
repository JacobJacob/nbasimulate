package com.ts.dt.check;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.helper.PassHelper;
import com.ts.dt.helper.RandomCheckHelper;
import com.ts.dt.po.Player;

public class LongPassCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		int percent = 80;
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
				context.setPassActionResult(MatchConstant.RESULT_FAILURE_BE_STEAL);
			}
		}
	}

}
