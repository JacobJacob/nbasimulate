package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.util.StringUtil;

public class PassResultCheckFactory {

	public static final String CHECK_CLASS_SUFFIXAL = "Check";
	public static final String CHECK_CLASS_PACKAGE_NAME = "com.ts.dt.match.check.pass";

	private static PassResultCheckFactory checkFactory;

	private PassResultCheckFactory() {

	}

	public static PassResultCheckFactory getInstance() {

		if (checkFactory == null) {
			checkFactory = new PassResultCheckFactory();
		}
		return checkFactory;
	}

	public ResultCheck createResultCheck(MatchContext context) {

		ResultCheck resultCheck = null;
		String claFulNm;
		Action action = context.getCurrentAction();
		String clsNm = StringUtil.className2ShortName(action);

		claFulNm = CHECK_CLASS_PACKAGE_NAME + "." + clsNm
				+ CHECK_CLASS_SUFFIXAL;

		try {
			resultCheck = (ResultCheck) Class.forName(claFulNm).newInstance();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return resultCheck;
	}
}
