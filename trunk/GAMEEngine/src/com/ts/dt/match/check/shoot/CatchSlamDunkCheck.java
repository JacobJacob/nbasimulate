package com.ts.dt.match.check.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;

public class CatchSlamDunkCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		String result = MatchConstant.RESULT_FAILURE;

		int pointOfDivision = 20;

		Random random = new Random();
		int a = random.nextInt(100);

		if (a > pointOfDivision) {
			result = MatchConstant.RESULT_SUCCESS;
		} else {
			result = MatchConstant.RESULT_FAILURE;
		}
		context.setShootActionResult(result);
	}

}
