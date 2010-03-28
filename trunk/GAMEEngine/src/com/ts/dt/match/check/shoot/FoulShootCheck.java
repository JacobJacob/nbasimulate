package com.ts.dt.match.check.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.po.Player;

public class FoulShootCheck implements ResultCheck {

	public void check(MatchContext context) {

		String result = MatchConstant.RESULT_FAILURE;

		int pointOfDivision = 60;

		Player player = context.getCurrentControllerPlayer();

		double p = (player.getShooting() / 10) * 2;

		pointOfDivision += p;

		Random random = new Random();
		int a = random.nextInt(100);

		if (a < pointOfDivision) {
			result = MatchConstant.RESULT_SUCCESS;
		}
		context.setShootActionResult(result);
	}

}
