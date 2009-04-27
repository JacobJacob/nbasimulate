package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.check.foul.DefaultFoulCheck;
import com.ts.dt.match.check.foul.FoulCheck;
import com.ts.dt.util.StringUtil;

public class FoulCheckFactory {

	public static final String CHECK_CLASS_SUFFIXAL = "FoulCheck";
	public static final String CHECK_CLASS_PACKAGE_NAME = "com.ts.dt.foul";

	private static FoulCheckFactory foulheckFactory;

	private FoulCheckFactory() {

	}

	public static FoulCheckFactory getInstance() {

		if (foulheckFactory == null) {
			foulheckFactory = new FoulCheckFactory();
		}
		return foulheckFactory;
	}

	public FoulCheck createFoulCheckFactory(MatchContext context) {

		FoulCheck foulCheck = null;

		String claFulNm;
		Action action = context.getCurrentAction();
		String clsNm = StringUtil.className2ShortName(action);

		claFulNm = CHECK_CLASS_PACKAGE_NAME + "." + clsNm + CHECK_CLASS_SUFFIXAL;

		try {
			foulCheck = (FoulCheck) Class.forName(claFulNm).newInstance();
		} catch (Exception e) {
			foulCheck = new DefaultFoulCheck();
		}
		return foulCheck;

	}
}
