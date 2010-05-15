package com.ts.dt.match.check.shoot;

/*�ж��о���Ͷ����û�н�
 * 
 * ���˫����Աˮƽ�൱,����벻���ĸ���Ϊ50%
 * 
 * Ͷ������ȡ���� ,Ͷ�� ,������ʶ ,�ٶ� ,���� , ����,�����Ա�Ĵ���ֵ ,����ս��Ӱ��ֵ,�������ɹ�ֵ
 * 
 * 
 *����                                      ��׼                                      ����                                     Ȩ��                                  ������
 *Ͷ��                                     0                  100              5
 *������ʶ                           0                  100              4
 *�ٶ�                                     0                  100              3
 *����                                     0                  100              3
 *����                                     0                  100              2
 *����ָ��                           0                  100              2
 *����ս��                           0                  100              2
 *
 *
 *���سɹ�����ȡ���� ���, ������ʶ, ����, �ٶ� , ����, ����ս��Ӱ��ֵ,  ����
 *
 *���                                   0
 *������ʶ                         0
 *�ƶ�                                   0
 *�ٶ�                                   0
 *����                                   0
 *����ս��Ӱ��ֵ          0
 *����                                   0
 *
 * */

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.check.ResultCheck;
import com.ts.dt.po.Player;
import com.ts.dt.util.DebugUtil;

public class ShortShootCheck implements ResultCheck {

	// �ж�Ͷ����û�н�
	public void check(MatchContext context) {

		String result = MatchConstant.RESULT_FAILURE;
		Player player = context.getCurrentController().getPlayer();
		Player defender = context.getCurrentDefender().getPlayer();

		int shootPower = this.checkShootPower(player, 0, 0);
		int defendPower = this.checkDefenPower(defender, 0);

		int point = 50; // �����о���Ͷ���Ŀ�����
		// ���A����Ϊ 60 B����Ϊ40,������ɹ�������Ϊ 70
		point += (shootPower - defendPower);

		// ս���������ʵ�Ӱ��
		int tacticalPoint = 0;
		if (context.isHomeTeam()) {
			tacticalPoint = context.getHomeTeamOffensiveTacticalPoint();
		} else {
			tacticalPoint = context.getGuestTeamOffensiveTacticalPoint();
		}
		point += ((tacticalPoint - 50) / 5);

		Random random = new Random();
		int a = random.nextInt(100);
		DebugUtil.debug("[" + context.getCurrentController().getControllerName() + "]" + player.getName() + "Ͷ������Ϊ" + shootPower);
		DebugUtil.debug("[" + context.getCurrentDefender().getControllerName() + "]" + defender.getName() + "��������Ϊ" + defendPower);
		DebugUtil.debug("�������п�����Ϊ" + point);
		if (point > 80) {
			point = 80;// �������80ǿ����Ϊ80
		}
		if (a < point) {
			result = MatchConstant.RESULT_SUCCESS;
			DebugUtil.debug("����Ͷ������");
		} else {
			DebugUtil.debug("����Ͷ��δ��");
		}
		context.setShootActionResult(result);
	}

	// ����Ͷ������
	private int checkShootPower(Player player, int prev_action_point, int tactical_point) {
		// *Ͷ�� 5 *������ʶ 4 *�ٶ� 3 *����3 *���� 2
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

	// �����������
	private int checkDefenPower(Player player, int tactical_point) {
		// *���4 *������ʶ4 *����3 *�ٶ�3 *����3 *����3
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
