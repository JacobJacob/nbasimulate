package com.ts.dt.match.check.block;

import java.util.Random;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.helper.BlockHelper;
import com.ts.dt.po.Player;

public class DefaultBlockCheck implements BlockCheck {

	private static int DEFAULT_DIV_POINT = 10;

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		Player curtPlayer = context.getCurrentControllerPlayer();
		Player curtDefender = context.getCurrentDefender().getPlayer();

		double blockPower = BlockHelper.checkBlockPower(curtPlayer);
		double vsBlockPower = BlockHelper.checkVsBlockPower(curtDefender);

		int divPoint = 0;

		if (vsBlockPower > blockPower) {
			divPoint = DEFAULT_DIV_POINT - (int) ((vsBlockPower - blockPower) / 10);
		} else {
			divPoint = DEFAULT_DIV_POINT;
		}

		Random random = new Random();
		int randnumber = random.nextInt(100);
		if (randnumber < divPoint) {
			context.setBlock(true);
		} else {
			context.setBlock(false);

		}
	}

}
