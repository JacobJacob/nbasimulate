package com.ts.dt.util;

import com.ts.dt.context.MatchContext;

public class MessagesUtil {

	public static void showLine(MatchContext context, String desc) {

		long time = context.getContinueTime();
		if (context.isHomeTeam()) {
			// Logger.info("[����]" + TimeUtil.timeMillis2TimeFormat(time) + desc
			// + context.currentScore());
		} else {
			// Logger.error("[�Ͷ�]" + TimeUtil.timeMillis2TimeFormat(time) + desc
			// + context.currentScore());
		}

	}
}
