package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.check.rebound.BackboardCheck;
import com.ts.dt.match.check.rebound.DefaultBackboardCheck;
import com.ts.dt.util.StringUtil;

public class BackboardCheckFactory {

	public static final String CHECK_CLASS_SUFFIXAL = "BackboardCheck";
	public static final String CHECK_CLASS_PACKAGE_NAME = "com.ts.dt.rebound";

	private static BackboardCheckFactory backboardCheckFactory;

	private BackboardCheckFactory() {

	}

	public static BackboardCheckFactory getInstance() {

		if (backboardCheckFactory == null) {
			backboardCheckFactory = new BackboardCheckFactory();
		}
		return backboardCheckFactory;
	}

	public BackboardCheck createBackboardCheckFactory(MatchContext context) {

		BackboardCheck backboardCheck = null;

		String claFulNm;
		Action action = context.getCurrentAction();
		String clsNm = StringUtil.className2ShortName(action);

		claFulNm = CHECK_CLASS_PACKAGE_NAME + "." + clsNm
				+ CHECK_CLASS_SUFFIXAL;

		try {
			backboardCheck = (BackboardCheck) Class.forName(claFulNm)
					.newInstance();
		} catch (Exception e) {
			return new DefaultBackboardCheck();
		}
		return backboardCheck;

	}
}
