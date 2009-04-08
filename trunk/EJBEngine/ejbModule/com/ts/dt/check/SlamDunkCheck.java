package com.ts.dt.check;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;

public class SlamDunkCheck implements ResultCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		String result = MatchConstant.RESULT_FAILURE;

		int pointOfDivision = 60;

		Random random = new Random();
		int a = random.nextInt(100);

		if (a > pointOfDivision) {
			result = MatchConstant.RESULT_SUCCESS;
		}
		context.setShootActionResult(result);
	}

}
