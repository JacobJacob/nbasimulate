/**
 * 
 */
package com.ts.dt.match.action.rebound;

import com.ts.dt.context.MatchContext;

/**
 * @author Administrator
 * 
 */
public class ReboundFactory {

	private static ReboundFactory shootFactory;

	private ReboundFactory() {

	}

	public static ReboundFactory getInstance() {

		if (shootFactory == null) {
			shootFactory = new ReboundFactory();
		}
		return shootFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Rebound createReboundAction(MatchContext context) {

		Rebound rebound = null;
		try {
			if (context.isDefensiveRebound()) {
				rebound = new DefensiveRebound();
			} else if (context.isOffensiveRebound()) {
				rebound = new OffensiveRebound();
			} else {
				throw new Exception("why call this method ? ");
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		context.setOffensiveRebound(false);
		context.setDefensiveRebound(false);

		return rebound;
	}

}
