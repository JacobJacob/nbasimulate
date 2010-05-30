package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.action.foul.DefensiveFoul;
import com.ts.dt.match.action.foul.OffensiveFoul;
import com.ts.dt.match.action.foul.OffensiveTimeOutFoul;
import com.ts.dt.match.action.pass.LongPass;
import com.ts.dt.match.action.pass.ShortPass;
import com.ts.dt.match.action.rebound.DefensiveRebound;
import com.ts.dt.match.action.rebound.OffensiveRebound;
import com.ts.dt.match.action.scrimmage.FoulScrimmage;
import com.ts.dt.match.action.scrimmage.OverTimeScrimmage;
import com.ts.dt.match.action.scrimmage.StartScrimmage;
import com.ts.dt.match.action.service.DefaultService;
import com.ts.dt.match.action.shoot.CatchSlamDunk;
import com.ts.dt.match.action.shoot.FoulShoot;
import com.ts.dt.match.action.shoot.HookFloatShoot;
import com.ts.dt.match.action.shoot.LongShoot;
import com.ts.dt.match.action.shoot.ShortShoot;
import com.ts.dt.match.action.shoot.SlamDunk;
import com.ts.dt.match.action.shoot.SlingShoot;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.desc.foul.OffensiveTimeOutFoulDescription;
import com.ts.dt.match.desc.pass.LongPassDescription;
import com.ts.dt.match.desc.pass.ShortPassDescription;
import com.ts.dt.match.desc.rebound.DefensiveReboundDescription;
import com.ts.dt.match.desc.rebound.OffensiveReboundDescription;
import com.ts.dt.match.desc.scrimmage.OverTimeScrimmageDescription;
import com.ts.dt.match.desc.scrimmage.StartScrimmageDescription;
import com.ts.dt.match.desc.service.DefaultServiceDescription;
import com.ts.dt.match.desc.shoot.CatchSlamDunkDescription;
import com.ts.dt.match.desc.shoot.FoulShootDescription;
import com.ts.dt.match.desc.shoot.LongShootDescription;
import com.ts.dt.match.desc.shoot.ShortShootDescription;
import com.ts.dt.match.desc.shoot.SlamDunkDescription;

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

	public ActionDescription createActionDescription(MatchContext context) throws MatchException {

		ActionDescription actionDescription = null;

		Action action = context.getCurrentAction();

		if (action instanceof ShortShoot) {
			actionDescription = new ShortShootDescription();
		} else if (action instanceof CatchSlamDunk) {
			actionDescription = new CatchSlamDunkDescription();
		} else if (action instanceof SlamDunk) {
			actionDescription = new SlamDunkDescription();
		} else if (action instanceof FoulShoot) {
			actionDescription = new FoulShootDescription();
		} else if (action instanceof LongShoot) {
			actionDescription = new LongShootDescription();
		} else if (action instanceof HookFloatShoot) {
			// actionDescription = new HookFloatShootDescription();
		} else if (action instanceof SlingShoot) {
			// actionDescription = new SlingShootDescription();
		} else if (action instanceof LongPass) {
			actionDescription = new LongPassDescription();
		} else if (action instanceof ShortPass) {
			actionDescription = new ShortPassDescription();
		} else if (action instanceof DefensiveRebound) {
			actionDescription = new DefensiveReboundDescription();
		} else if (action instanceof OffensiveRebound) {
			actionDescription = new OffensiveReboundDescription();
		} else if (action instanceof FoulScrimmage) {
			// actionDescription = new FoulScrimmageDescription();
		} else if (action instanceof OverTimeScrimmage) {
			actionDescription = new OverTimeScrimmageDescription();
		} else if (action instanceof StartScrimmage) {
			actionDescription = new StartScrimmageDescription();
		} else if (action instanceof DefensiveFoul) {
			// actionDescription = new DefensiveFoulDescription();
		} else if (action instanceof OffensiveFoul) {
			// actionDescription = new OffensiveFoulDescription();
		} else if (action instanceof OffensiveTimeOutFoul) {
			actionDescription = new OffensiveTimeOutFoulDescription();
		} else if (action instanceof DefaultService) {
			actionDescription = new DefaultServiceDescription();
		} else {
			throw new MatchException("动作没定义" + action.toString());
		}

		return actionDescription;

	}
}
