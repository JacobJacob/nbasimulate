package com.ts.dt.match.action.foul;

import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.helper.MatchInfoHelper;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.util.MessagesUtil;

public class OffensiveTimeOutFoul implements Foul {

	public void execute(MatchContext context) {
		// TODO Auto-generated method stub
		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);
		String desc = description.load(context);
		MessagesUtil.showLine(context, desc);
		MatchInfoHelper.save(context,desc);
	}

}
