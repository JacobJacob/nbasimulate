package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.util.StringUtil;

/*
 * 动作描述工厂类
 */
public class ActionDescriptionFactory {

	public static final String CHECK_CLASS_SUFFIXAL = "Description";
	public static final String CHECK_CLASS_PACKAGE_NAME = "com.ts.dt.match.desc";

	private static ActionDescriptionFactory descriptionFactory;

	private ActionDescriptionFactory() {

	}

	public static ActionDescriptionFactory getInstance() {

		if (descriptionFactory == null) {
			descriptionFactory = new ActionDescriptionFactory();
		}
		return descriptionFactory;
	}

	public ActionDescription createActionDescription(MatchContext context) {

		ActionDescription actionDescription = null;

		String claFulNm;
		Action action = context.getCurrentAction();
		String clsNm = StringUtil.className2ShortNameWithHigherPackage(action);

		claFulNm = CHECK_CLASS_PACKAGE_NAME + "." + clsNm
				+ CHECK_CLASS_SUFFIXAL;

		try {
			actionDescription = (ActionDescription) Class.forName(claFulNm)
					.newInstance();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return actionDescription;

	}
}
