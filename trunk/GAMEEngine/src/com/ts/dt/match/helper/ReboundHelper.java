package com.ts.dt.match.helper;

import java.util.Hashtable;

import com.ts.dt.match.Controller;
import com.ts.dt.po.Player;

/*
 * �κ���������100����,�ֱ���,����, ����, Ͷ��, ����, ����, ���
 * 100 ��Ʒ��Ա,��ʵ��Ӧ�ò�����
 * 90 ��������,��Ʊȵ�Ͷ��,�޵���������
 * 80 һ������,����, ����֮��
 * 70 �в���Ա
 * 60 ��ǿ����, ��ɫ��Ա
 * 50 ����,������
 * 
 * 
 * ����, ˫��������ÿ��1��,��Ӧ������������ĸ��ʾ�Ҫ��һ���ٷֵ�, 
 * �� A 90 B 80 ,��ôA��B��������ʱ,A�õ���ĸ���Ϊ  55% , B��Ϊ45% A 90 B 70 A60% B40%  
 *���ڿ��ǵ�ǰ��֮��,������������ʱ���õ��ļӳ�Ϊ20,Ҳ����˵,Aǰ��(90), B��(70) ��ô���ǵõ���ĸ�����һ����
 *
 *
 *������Ա�������㹫ʽ
 *
 *����       ��׼      ����   Ȩ��        ������
 *���      0   220    3      220 * 3
 *����      0    100    3     100 * 3
 *ǿ׳      0    100    2     100 * 2
 *����      0    100    4     100 * 4
 *�ٶ�      0    100    2     100 * 2
 *���(���� - ��׼ֵ  * Ȩ��) /  280 * 100
 */

public class ReboundHelper {

	// �жϳ�����Ա�������������
	public static int[] checkPercentForGetRebound(Hashtable<String, Controller> controllers, boolean isHomeTeam) {
		int[] percent = new int[] { 5, 15, 25, 40, 60 };

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
		// ������������ֵ
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
