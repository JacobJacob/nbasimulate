package com.ts.dt.match.action.service;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.util.MessagesUtil;

public class DefaultService implements Service {

	public void service(MatchContext context)throws MatchException {
		// TODO Auto-generated method stub

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtPlayerNm = context.getCurrentController().getPlayerName();
		String desc = description.load(context);
		desc = desc.replace("~1~", currtPlayerNm.trim());
		MessagesUtil.showLine(context, desc);
		MatchInfoHelper.save(context, desc);
		context.setNewLine(true);

	}

}
