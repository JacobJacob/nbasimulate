package com.ts.dt.match.check.rebound;

import java.util.Random;

import com.ts.dt.context.MatchContext;

public class DefaultBackboardCheck implements BackboardCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		int point;
		if (context.isOutside()) {
			return;
		}
		int totalHomeBackboard = context.getTotalHomeBackboard() / 5;
		int totalGuestBackboard = context.getTotalGuestBackboard() / 5;

		if (context.isHomeTeam()) {
			// 主队控球,就是前场篮板
			totalGuestBackboard += BackboardCheck.defensiveReboundInc;
		} else {
			// 客队队控球,就是后场篮板
			totalHomeBackboard += BackboardCheck.defensiveReboundInc;
		}

		int sub_point = 0;
		if (totalHomeBackboard >= totalGuestBackboard) {
			sub_point = totalHomeBackboard - totalGuestBackboard;
			point = (100 - sub_point) / 2 + sub_point;
		} else {
			sub_point = totalGuestBackboard - totalHomeBackboard;
			point = (100 - sub_point) / 2;
		}

		Random random = new Random();
		int i = random.nextInt(100);

		if (context.isHomeTeam()) {
			this.debug("前场篮板可能性为:" + point);
			this.debug("前场球队篮板能力为:" + context.getTotalHomeBackboard());
			this.debug("后场球队篮板能力为:" + context.getTotalGuestBackboard());
			if (i < point) {
				// 如果是主队,且概率在主队这边,则是前场篮板
				context.setOffensiveRebound(true);
				context.setDefensiveRebound(false);
			} else {
				// 如果是主队,且概率在客队这边,则是后场篮板
				context.setOffensiveRebound(false);
				context.setDefensiveRebound(true);
			}
		} else {
			if (i < point) {
				// 如果是客队,且概率在主队这边,则是后场篮板
				context.setOffensiveRebound(false);
				context.setDefensiveRebound(true);
			} else {
				// 如果是客队,且概率在客队这边,则是前场篮板
				context.setOffensiveRebound(true);
				context.setDefensiveRebound(false);
			}
		}
	}

	private void debug(String message) {
		System.out.println(message);
	}

}
