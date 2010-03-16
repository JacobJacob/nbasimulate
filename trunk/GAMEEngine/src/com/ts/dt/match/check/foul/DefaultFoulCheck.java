package com.ts.dt.match.check.foul;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.action.shoot.FoulShoot;
import com.ts.dt.match.action.shoot.LongShoot;
import com.ts.dt.match.helper.FoulHelper;
import com.ts.dt.match.helper.RandomCheckHelper;
import com.ts.dt.match.helper.SubstitutionHelper;
import com.ts.dt.po.ProfessionPlayer;

public class DefaultFoulCheck implements FoulCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		Action action = context.getCurrentAction();
		// Foul Shoot Not Exist Foul
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
			// statistical significance
			ProfessionPlayer defender = context.getCurrentDefender().getPlayer();
			boolean isHomeTeam = context.getCurrentDefender().getControllerName().endsWith("A");
			context.playerAddFoulTimes(defender, isHomeTeam);
			if (context.checkFoulOut(defender, isHomeTeam)) {
				// substitution
				context.setFoutOutController(context.getCurrentDefender());
				SubstitutionHelper.FoulOutSubstitution(context);
			}
		} else {
			context.setFoul(false);
		}
	}
}
