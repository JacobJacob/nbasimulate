/**
 * 
 */
package com.ts.dt.match.action.scrimmage;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;

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
	public Scrimmage createScrimmageAction(MatchContext context) throws MatchException {

		Scrimmage scrimmage = null;
		try {
			if (context.isJustStart()) {
				// ����
				scrimmage = new StartScrimmage();
				context.setJustStart(false);
				// } else if (context.isOffensiveRebound()) {
				// scrimmage = new FoulScrimmage();
				// 
			} else if (context.getSeq() > 4) {
				// ��ʱ������
				scrimmage = new OverTimeScrimmage();
			}

		} catch (Exception e) {
			e.printStackTrace();
		}

		if (scrimmage == null) {
			throw new MatchException("�������쳣[" + context.getSeq() + "]");
		}

		return scrimmage;
	}
}
