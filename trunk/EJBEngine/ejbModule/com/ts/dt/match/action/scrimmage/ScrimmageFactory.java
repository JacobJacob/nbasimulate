/**
 * 
 */
package com.ts.dt.match.action.scrimmage;

import com.ts.dt.context.MatchContext;

/**
 * @author Administrator
 * 
 */
public class ScrimmageFactory {

	private static ScrimmageFactory shootFactory;

	private ScrimmageFactory() {

	}

	public static ScrimmageFactory getInstance() {

		if (shootFactory == null) {
			shootFactory = new ScrimmageFactory();
		}
		return shootFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Scrimmage createScrimmageAction(MatchContext context) {

		Scrimmage scrimmage = null;
		try {
			if (context.isJustStart()) {
				scrimmage = new StartScrimmage();
			} else if (context.isOffensiveRebound()) {
				scrimmage = new FoulScrimmage();
			} else {
				throw new Exception("why call this method ? ");
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		context.setJustStart(false);

		return scrimmage;
	}

}
