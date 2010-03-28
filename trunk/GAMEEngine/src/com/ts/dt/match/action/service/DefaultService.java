package com.ts.dt.match.action.service;

import com.ts.dt.context.MatchContext;
import com.ts.dt.factory.ActionDescriptionFactory;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.po.Player;
//import com.ts.dt.util.Logger;
import com.ts.dt.util.MessagesUtil;

public class DefaultService implements Service {

	public void service(MatchContext context) {
		// TODO Auto-generated method stub

		context.setCurrentAction(this);
		ActionDescription description = ActionDescriptionFactory.getInstance().createActionDescription(context);

		String currtContrNm = context.getCurrentController().getControllerName();
		String currtPlayerNm = context.getCurrentController().getPlayer().getName();
		Player player = context.getCurrentController().getPlayer();
		String currtDefenderNm = context.getCurrentDefender().getPlayer().getName();
		String previousPlayerNm = null;
		if (context.getPreviousController() != null) {
			previousPlayerNm = context.getPreviousController().getPlayer().getName();
		}

		String desc = description.load(context);
		if (desc == null) {
			//Logger.error("desc is null");
		}
		String currentTeamNm = context.getCurrentController().getTeamFlg();
		String previousTeamNm = "";
		if (context.getPreviousController() != null) {
			previousTeamNm = context.getPreviousController().getTeamFlg();
		}

		desc = desc.replace("~1~", currtPlayerNm.trim());

		MessagesUtil.showLine(context, desc);

	}

}
