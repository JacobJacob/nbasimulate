package com.ts.dt.match.check.scrimmage;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.match.helper.RandomCheckHelper;
import com.ts.dt.match.helper.ScrimmageHelper;
import com.ts.dt.po.ProfessionPlayer;

public class StartScrimmageCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		ProfessionPlayer currtPlayer = context.getCurrentController().getPlayer();
		ProfessionPlayer currtDefender = context.getCurrentDefender().getPlayer();

		int percent = ScrimmageHelper.checkScrimmageResult(currtPlayer, currtDefender);

		if (RandomCheckHelper.defaultCheck(percent)) {
			context.setScrimmageResult(MatchConstant.RESULT_SUCCESS);
		} else {
			context.setScrimmageResult(MatchConstant.RESULT_FAILURE);
		}

	}

}
