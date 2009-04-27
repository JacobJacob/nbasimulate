package com.ts.dt.match.check.foul;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.helper.FoulHelper;
import com.ts.dt.helper.RandomCheckHelper;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.action.shoot.FoulShoot;
import com.ts.dt.match.action.shoot.LongShoot;

public class DefaultFoulCheck implements FoulCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		Action action = context.getCurrentAction();
		if (action instanceof FoulShoot) {
			return;
		}

		if (RandomCheckHelper.defaultCheck(FoulHelper.checkDefensiveFoulAfterShoot(context))) {
			context.setFoul(true);
			if (context.isSuccess()) {

				context.setFoulShootRemain(1);
				context.setFoulShootType(MatchConstant.FOUL_SHOOT_TYPE_ONE);

			} else {
				if (action instanceof LongShoot) {

					context.setFoulShootRemain(3);
					context.setFoulShootType(MatchConstant.FOUL_SHOOT_TYPE_THREE);
				} else {

					context.setFoulShootRemain(2);
					context.setFoulShootType(MatchConstant.FOUL_SHOOT_TYPE_TWO);
				}
			}
		} else {
			context.setFoul(false);
		}
	}

}
