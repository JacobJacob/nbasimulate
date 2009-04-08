/**
 * 
 */
package com.ts.dt.match.action.foul;

import com.ts.dt.context.MatchContext;

/**
 * @author Administrator
 * 
 */
public class FoulFactory {

	private static FoulFactory shootFactory;

	private FoulFactory() {

	}

	public static FoulFactory getInstance() {

		if (shootFactory == null) {
			shootFactory = new FoulFactory();
		}
		return shootFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Foul createFoulAction(MatchContext context) {

		Foul foul = null;
		if (context.currentOffensiveTimeOut()) {
			foul = new OffensiveTimeOutFoul();
		}
		return foul;
	}
}
