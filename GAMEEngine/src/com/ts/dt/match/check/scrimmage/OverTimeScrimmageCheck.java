package com.ts.dt.match.check.scrimmage;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.match.helper.RandomCheckHelper;
import com.ts.dt.match.helper.ScrimmageHelper;
import com.ts.dt.po.Player;

public class OverTimeScrimmageCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		Player currtPlayer = context.getCurrentController().getPlayer();
		Player currtDefender = context.getCurrentDefender().getPlayer();

		int percent = ScrimmageHelper.checkScrimmageResult(currtPlayer, currtDefender);

		if (RandomCheckHelper.defaultCheck(percent)) {
			context.setScrimmageResult(MatchConstant.RESULT_SUCCESS);
		} else {
			context.setScrimmageResult(MatchConstant.RESULT_FAILURE);
		}

	}

}
