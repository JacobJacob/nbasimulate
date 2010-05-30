package com.ts.dt.match.helper;

import java.util.Hashtable;

import com.ts.dt.match.Controller;
import com.ts.dt.po.Player;

/*
 * 任何能力都以100计算,分别有,传球, 运球, 投篮, 三分, 篮板, 封盖
 * 100 极品球员,现实中应该不存在
 * 90 超极巨星,如科比的投篮,罗德曼的篮板
 * 80 一般世星,如马丁, 罗伊之类
 * 70 中产球员
 * 60 勉强还行, 角色球员
 * 50 以下,不及格
 * 
 * 
 * 篮板, 双方的能力每差1点,相应的抢得篮板球的概率就要提一个百分点, 
 * 如 A 90 B 80 ,那么A和B抢篮板球时,A得到球的概率为  55% , B的为45% A 90 B 70 A60% B40%  
 *由于考虑到前后场之分,所以抢后场篮板时所得到的加成为20,也就是说,A前场(90), B后场(70) 那么他们得到球的概率是一样的
 *
 *
 *篮板球员能力计算公式
 *
 *属性       基准      顶级   权重        最大可能
 *身高      0   220    3      220 * 3
 *弹跳      0    100    3     100 * 3
 *强壮      0    100    2     100 * 2
 *篮板      0    100    4     100 * 4
 *速度      0    100    2     100 * 2
 *求和(属性 - 基准值  * 权重) /  280 * 100
 */

public class ReboundHelper {

	// 判断场上球员争抢篮板的能力
	public static int[] checkPercentForGetRebound(Hashtable<String, Controller> controllers, boolean isHomeTeam) {
		int[] percent = new int[] { 15, 25, 35, 44, 55 };

		String teamFlg = "";
		if (isHomeTeam) {
			teamFlg = "A";
		} else {
			teamFlg = "B";
		}
		String[] positions = { "PG", "SG", "SF", "PF", "C" };

		for (int i = 0; i < 5; i++) {
			int power = checkReboundPower(controllers.get(positions[i] + teamFlg).getPlayer());
			power -= 60;
			if (power > 0) {
				percent[i] += power;
			}
		}

		return percent;
	}

	public static int checkReboundPower(Player player) {
		// 计算篮板能力值
		int total = 0;

		// int[] base = { 0, 0, 0, 0, 0 };
		int[] weight = { 2, 2, 3, 3, 4 };
		int[] max = { 100, 100, 220, 100, 100 };

		total += (player.getStrength() * 2);
		total += (player.getSpeed() * 2);
		total += (player.getStature() * 3);
		total += (player.getBounce() * 3);
		total += (player.getBackboard() * 4);

		int max_total = 0;
		for (int i = 0; i < max.length; i++) {
			max_total += (max[i] * weight[i]);
		}

		int power = total * 100 / max_total;
		return power;
	}
}
