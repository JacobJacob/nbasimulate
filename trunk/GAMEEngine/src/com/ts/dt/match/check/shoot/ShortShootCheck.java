package com.ts.dt.match.check.shoot;

/*判断中距离投篮有没有进
 * 
 * 如果双方球员水平相当,则进与不进的概率为50%
 * 
 * 投篮能力取决于 ,投篮 ,进攻意识 ,速度 ,三分 , 体力,传球队员的传球值 ,本队战术影响值,本动作成功值
 * 
 * 
 *属性                                      基准                                      顶级                                     权重                                  最大可能
 *投篮                                     0                  100              5
 *进攻意识                           0                  100              4
 *速度                                     0                  100              3
 *体力                                     0                  100              3
 *三分                                     0                  100              2
 *传球指数                           0                  100              2
 *本队战术                           0                  100              2
 *
 *
 *防守成功能力取决于 封盖, 防守意识, 抢断, 速度 , 弹跳, 本队战术影响值,  体力
 *
 *封盖                                   0
 *防守意识                         0
 *推断                                   0
 *速度                                   0
 *弹跳                                   0
 *本队战术影响值          0
 *体力                                   0
 *
 * */

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.po.Player;
import com.ts.dt.util.DebugUtil;

public class ShortShootCheck implements ResultCheck {

	// 判断投篮有没有进
	public void check(MatchContext context) {

		String result = MatchConstant.RESULT_FAILURE;
		Player player = context.getCurrentController().getPlayer();
		Player defender = context.getCurrentDefender().getPlayer();

		int shootPower = this.checkShootPower(player, 0, 0);
		int defendPower = this.checkDefenPower(defender, 0);

		int point = 50; // 这是中距离投篮的可能性
		// 如果A进攻为 60 B防守为40,则进攻成功可能性为 70
		point += (shootPower - defendPower);

		// 战术对命中率的影响
		int tacticalPoint = 0;
		if (context.isHomeTeam()) {
			tacticalPoint = context.getHomeTeamOffensiveTacticalPoint();
		} else {
			tacticalPoint = context.getGuestTeamOffensiveTacticalPoint();
		}
		point += ((tacticalPoint - 50) / 5);

		Random random = new Random();
		int a = random.nextInt(100);
		DebugUtil.debug("[" + context.getCurrentController().getControllerName() + "]" + player.getName() + "投篮能力为" + shootPower);
		DebugUtil.debug("[" + context.getCurrentDefender().getControllerName() + "]" + defender.getName() + "防守能力为" + defendPower);
		DebugUtil.debug("本次命中可能性为" + point);
		if (point > 80) {
			point = 80;// 如果大于80强制设为80
		}
		if (a < point) {
			result = MatchConstant.RESULT_SUCCESS;
			DebugUtil.debug("本次投篮命中");
		} else {
			DebugUtil.debug("本次投篮未中");
		}
		context.setShootActionResult(result);
	}

	// 计算投篮能力
	private int checkShootPower(Player player, int prev_action_point, int tactical_point) {
		// *投篮 5 *进攻意识 4 *速度 3 *体力3 *三分 2
		int total = 0;

		double[] attr_power = { player.getShooting(), player.getOffencons(), player.getSpeed(), player.getMatchPower() };
		int[] weight = { 5, 4, 3, 3, 2 };
		int[] max = { 100, 100, 100, 100, 100 };

		for (int i = 0; i < attr_power.length; i++) {
			total += attr_power[i] * weight[i];
		}

		int max_total = 0;
		for (int i = 0; i < max.length; i++) {
			max_total += (max[i] * weight[i]);
		}

		int power = total * 100 / max_total;
		return power;

	}

	// 计算防守能力
	private int checkDefenPower(Player player, int tactical_point) {
		// *封盖4 *防守意识4 *抢断3 *速度3 *弹跳3 *体力3
		int total = 0;

		double[] attr_power = { player.getBlocked(), player.getDefencons(), player.getSteal(), player.getSpeed(), player.getBounce(), player.getMatchPower() };
		int[] weight = { 4, 4, 3, 3, 3, 3 };
		int[] max = { 100, 100, 100, 100, 100, 100 };

		for (int i = 0; i < attr_power.length; i++) {
			total += attr_power[i] * weight[i];
		}

		int max_total = 0;
		for (int i = 0; i < max.length; i++) {
			max_total += (max[i] * weight[i]);
		}

		int power = total * 100 / max_total;
		return power;

	}

}
