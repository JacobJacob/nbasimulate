package com.ts.dt.match.check;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.helper.RandomCheckHelper;
import com.ts.dt.helper.ScrimmageHelper;
import com.ts.dt.po.Player;

public class StartScrimmageCheck implements ResultCheck {

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
