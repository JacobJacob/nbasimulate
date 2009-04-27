package com.ts.dt.match.check.rebound;

import java.util.Random;

import com.ts.dt.context.MatchContext;

public class SlamDunkBackboardCheck implements BackboardCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		int point = 5;
		if (context.isOutside()) {
			return;
		}
		int totalFBackboardA = 3;// = context.getTotalFBackboardA();
		int totalBBackboardA = 4;// = context.getTotalFBackboardA();
		int totalFBackboardB = 5;// = context.getTotalFBackboardA();
		int totalBBackboardB = 6;// = context.getTotalFBackboardA();

		if (context.isHomeTeam()) {
			point = point + totalFBackboardA - totalBBackboardB;
		} else {
			point = point + totalFBackboardB - totalBBackboardA;
		}
		Random random = new Random();
		int i = random.nextInt(100);
		if (i < point) {
			context.setOffensiveRebound(true);
			context.setDefensiveRebound(false);
		} else {
			context.setOffensiveRebound(false);
			context.setDefensiveRebound(true);
		}
	}

}
