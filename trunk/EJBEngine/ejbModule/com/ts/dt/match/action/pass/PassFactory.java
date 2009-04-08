package com.ts.dt.match.action.pass;

import com.ts.dt.context.MatchContext;
import com.ts.dt.helper.RandomCheckHelper;

public class PassFactory {
	private static PassFactory passFactory;

	private PassFactory() {

	}

	public static PassFactory getInstance() {

		if (passFactory == null) {
			passFactory = new PassFactory();
		}
		return passFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Pass createPassAction(MatchContext context) {

		Pass pass = null;

		if (RandomCheckHelper.defaultCheck(90)) {
			pass = new ShortPass();
		} else {
			pass = new LongPass();
		}

		return pass;
	}

}
