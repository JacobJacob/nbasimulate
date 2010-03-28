/**
 * 
 */
package com.ts.dt.match.action.shoot;

import java.util.Random;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.helper.TechnicallyTraitHelper;
import com.ts.dt.po.Player;

/**
 * @author Administrator
 * 
 */
public class ShootFactory {

	private static ShootFactory shootFactory;

	private ShootFactory() {

	}

	public static ShootFactory getInstance() {

		if (shootFactory == null) {
			shootFactory = new ShootFactory();
		}
		return shootFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Shoot createShootAction(MatchContext context) {

		Shoot shoot = null;

		if (context.getNextAction() != null) {
			shoot = (Shoot) context.getNextAction();
			context.removeNextAction();
			return shoot;
		}

		int shortShootProbability = 20;
		int longShootProbability = 2;
		int slamDunkProbability = 0;
		int slingShootProbability = 0;
		int hookFloatShootProbability = 0;
		int breacShootProbability = 0;

		Player player = context.getCurrentController().getPlayer();
		int trait = TechnicallyTraitHelper.check(player);

		switch (trait) {
		case TechnicallyTraitHelper.TECHNICALLY_TRAIT_OUT:
			longShootProbability += 4;
			break;
		case TechnicallyTraitHelper.TECHNICALLY_TRAIT_INT:
			break;
		case TechnicallyTraitHelper.TECHNICALLY_TRAIT_MID:
			shortShootProbability += 5;
			break;
		case TechnicallyTraitHelper.TECHNICALLY_TRAIT_BREAK:
			break;
		case TechnicallyTraitHelper.TECHNICALLY_TRAIT_SWIPE:
			slamDunkProbability += 5;
			break;
		default:
			break;
		}

		int total = 0;
		total += shortShootProbability;
		total += longShootProbability;
		total += slamDunkProbability;
		total += slingShootProbability;
		total += hookFloatShootProbability;
		total += breacShootProbability;

		int shortShootProbabilityPercent = shortShootProbability;
		int longShootProbabilityPercent = shortShootProbabilityPercent + longShootProbability;
		int slamDunkProbabilityPercent = longShootProbabilityPercent + slamDunkProbability;
		int slingShootProbabilityPercent = slamDunkProbabilityPercent + slamDunkProbability;
		int hookFloatShootProbabilityPercent = slingShootProbabilityPercent + slingShootProbability;
		int breacShootProbabilityPercent = hookFloatShootProbabilityPercent + breacShootProbability;

		Random random = new Random();
		int a = random.nextInt(total);

		if (a < shortShootProbabilityPercent) {
			shoot = new ShortShoot();
		} else if (a < longShootProbabilityPercent) {
			shoot = new LongShoot();
		} else if (a < slamDunkProbabilityPercent) {
			shoot = new SlamDunk();
		} else if (a < slingShootProbabilityPercent) {
			shoot = new SlingShoot();
		} else if (a < hookFloatShootProbabilityPercent) {
			shoot = new HookFloatShoot();
		} else if (a < breacShootProbabilityPercent) {
			shoot = new BreachShoot();
		}
		return shoot;
	}
}
