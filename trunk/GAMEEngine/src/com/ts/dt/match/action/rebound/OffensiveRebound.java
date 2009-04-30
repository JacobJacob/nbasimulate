package com.ts.dt.match.action.rebound;

import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.MatchInfoHelper;
import com.ts.dt.po.Player;
import com.ts.dt.util.MessagesUtil;

public class OffensiveRebound implements Rebound {

	public String execute(MatchContext context) {
		// TODO Auto-generated method stub
		String result = null;
		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtPlayerName = context.getCurrentController().getPlayer().getName();
		Player player = context.getCurrentController().getPlayer();
		String currtContrNm = context.getCurrentController().getControllerName();
		String desc = description.load(context);

		context.addOffensiveRebound(currtContrNm.endsWith("A"));
		context.playerAddOffensiveRebound(player, currtContrNm.endsWith("A"));

		desc = desc.replace("~1~", currtPlayerName.trim());

		MessagesUtil.showLine(context, desc);
		
		MatchInfoHelper.save(context,desc);

		return result;
	}

}
