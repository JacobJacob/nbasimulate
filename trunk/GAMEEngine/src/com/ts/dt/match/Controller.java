package com.ts.dt.match;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.foul.Foul;
import com.ts.dt.match.action.foul.FoulFactory;
import com.ts.dt.match.action.pass.Pass;
import com.ts.dt.match.action.pass.PassFactory;
import com.ts.dt.match.action.rebound.Rebound;
import com.ts.dt.match.action.rebound.ReboundFactory;
import com.ts.dt.match.action.scrimmage.Scrimmage;
import com.ts.dt.match.action.scrimmage.ScrimmageFactory;
import com.ts.dt.match.action.service.Service;
import com.ts.dt.match.action.service.ServiceFactory;
import com.ts.dt.match.action.shoot.FoulShoot;
import com.ts.dt.match.action.shoot.Shoot;
import com.ts.dt.match.action.shoot.ShootFactory;
import com.ts.dt.match.helper.ActionCostTimeHelper;
import com.ts.dt.po.Player;

public class Controller {

	private Player player;
	private String controllerName;
	private String teamFlg;

	// 投球动作
	public void shout(MatchContext context) {

		Shoot shoot = ShootFactory.getInstance().createShootAction(context);
		shoot.execute(context);
		long remainTime = ActionCostTimeHelper.shootRemainTime(player); // 投球以后还剩余的时间
		long currentOffensiveCostTime = context.getCurrentOffensiveCostTime(); // 当前进攻已经花费时间
		long thisActionCostTime = 240 - remainTime;

		if (thisActionCostTime < currentOffensiveCostTime) {
			thisActionCostTime = currentOffensiveCostTime;
		}

		// 整个动作所花费的时间-之前所计算的时间,等于本次要加上去的时间, 罚球的话是停表
		if (!(shoot instanceof FoulShoot)) {
			long addTime = thisActionCostTime - currentOffensiveCostTime;
			context.nodosityCostTimeAdd(addTime);
			context.currentOffensiveCostTimeReset(); // 重置已经花费的时间
		}

	}

	// the rebound action
	public void loose(MatchContext context) {
		Rebound rebound = ReboundFactory.getInstance().createReboundAction(context);
		rebound.execute(context);
	}

	// the pass action
	public void pass(MatchContext context) {
		Pass pass = PassFactory.getInstance().createPassAction(context);
		pass.before(context);
		long costTime = ActionCostTimeHelper.passCostTime(player);
		context.nodosityCostTimeAdd(costTime);
		context.currentOffensiveCostTimeAdd(costTime);
	}

	// the scrimmage action
	public void scrimmage(MatchContext context) {
		Scrimmage scrimmage = ScrimmageFactory.getInstance().createScrimmageAction(context);
		scrimmage.before(context);
	}

	// the foul action
	public void foul(MatchContext context) {
		Foul foul = FoulFactory.getInstance().createFoulAction(context);
		foul.execute(context);
		// reset 24 offensive cost time
		context.currentOffensiveCostTimeReset();
	}

	public void service(MatchContext context) {
		Service service = ServiceFactory.getInstance().createServiceAction(context);
		service.service(context);
	}

	public Player getPlayer() {
		return player;
	}

	public void setPlayer(Player player) {
		this.player = player;
	}

	public String getControllerName() {
		return controllerName;
	}

	public void setControllerName(String controllerName) {
		this.controllerName = controllerName;
	}

	public String getTeamFlg() {
		return teamFlg;
	}

	public void setTeamFlg(String teamFlg) {
		this.teamFlg = teamFlg;
	}

}
