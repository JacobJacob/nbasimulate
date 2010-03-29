package com.ts.dt.match.check.rebound;

import java.util.Random;

import com.ts.dt.context.MatchContext;

public class DefaultBackboardCheck implements BackboardCheck {

	public void check(MatchContext context) {
		// TODO Auto-generated method stub
		int point;
		if (context.isOutside()) {
			return;
		}
		int totalHomeBackboard = context.getTotalHomeBackboard() / 5;
		int totalGuestBackboard = context.getTotalGuestBackboard() / 5;

		if (context.isHomeTeam()) {
			// ���ӿ���,����ǰ������
			totalGuestBackboard += BackboardCheck.defensiveReboundInc;
		} else {
			// �ͶӶӿ���,���Ǻ�����
			totalHomeBackboard += BackboardCheck.defensiveReboundInc;
		}

		int sub_point = 0;
		if (totalHomeBackboard >= totalGuestBackboard) {
			sub_point = totalHomeBackboard - totalGuestBackboard;
			point = (100 - sub_point) / 2 + sub_point;
		} else {
			sub_point = totalGuestBackboard - totalHomeBackboard;
			point = (100 - sub_point) / 2;
		}

		Random random = new Random();
		int i = random.nextInt(100);

		if (context.isHomeTeam()) {
			if (i < point) {
				// ���������,�Ҹ������������,����ǰ������
				context.setOffensiveRebound(true);
				context.setDefensiveRebound(false);
			} else {
				// ���������,�Ҹ����ڿͶ����,���Ǻ�����
				context.setOffensiveRebound(false);
				context.setDefensiveRebound(true);
			}
		} else {
			if (i < point) {
				// ����ǿͶ�,�Ҹ������������,���Ǻ�����
				context.setOffensiveRebound(false);
				context.setDefensiveRebound(true);
			} else {
				// ����ǿͶ�,�Ҹ����ڿͶ����,����ǰ������
				context.setOffensiveRebound(true);
				context.setDefensiveRebound(false);
			}
		}
	}

	public void debug(String message) {
		System.out.println(message);
	}

}
