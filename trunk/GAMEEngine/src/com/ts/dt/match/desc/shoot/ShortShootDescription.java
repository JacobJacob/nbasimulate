package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.TechnicallyTraitHelper;
import com.ts.dt.po.Player;

public class ShortShootDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		Random random = new Random();
		int a = random.nextInt(16);
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			switch (a) {
			case 0:
				desc = "~1~�Ⱥ��ڵ��� ,���˿յ�,�ӵ��������εĴ���,����,���! ";
				break;
			case 1:
				desc = "~1~�Ⱥ��ڵ��� ,���˿յ�,�ӵ��������εĴ���,����,���! ";
				break;
			case 2:
				desc = "~1~����ֱ�ӳ���,һ���ǳ�,Ư���Ļ���!���������";
				break;
			case 3:
				desc = "~1~��װͻ��,ƭ�����ض�Ա,ͻȻ����Ͷ��,���";
				break;
			case 4:
				desc = "~1~��ס���ض�Ա,ǿ�г���! ���Ȼ����";
				break;
			case 5:
				desc = "~1~��Ȧ���������,�ι�~2~,һ���ܲ���׼��Ͷ��,����!";
				break;
			case 6:
				desc = "~1~��~3~����һ��������Ϻ�,��һ�Ǿ�׼�İ���������˱��ν���!";
				break;
			case 7:
				desc = "~1~��������,���˷��ض�Ա~2~,���ɵĴ������!";
				break;
			case 8:
				desc = "~1~������,�����強��������Ա,һ�����ֹ���Ͷ��,����";
				break;
			case 9:
				desc = "~1~��~3~���εı�����,ͻ����������";
				break;
			case 10:
				desc = "~1~������ǿ�Զ���,�ڶ���ͷ���������";
				break;
			case 11:
				desc = "~1~��Ͷ����";
				break;
			case 12:
				desc = "~1~�Ų�������,�߸�Ծ��Ͷ��,����";
				break;
			case 13:
				desc = "~1~���ڷ�����Ա�ķ��֮ǰ��������";
				break;
			case 14:
				desc = "~1~���Ǹ�Ͷ������,�������κε���,ֱ�ӳ���,Ƥ���������";
				break;
			case 15:
				desc = "~1~�����������һ��������Ա,���ڲ�����Ա֮ǰ����Ͷ������";
				break;
			default:
				break;
			}

		} else {
			switch (a) {
			case 0:
				desc = "~1~�Ⱥ��ڵ��� ,���˿յ�,�ӵ��������εĴ���,����,��ƫ�����! ";
				break;
			case 1:
				desc = "~1~����ֱ�ӳ���,һ���ǳ�,Ư���Ļ���!�����ǲ�һ��û��";
				break;
			case 2:
				desc = "~1~��װͻ��, ƭ�����ض�Ա,ͻȻ����Ͷ��,�򱻷��ض�Ա������һ��,û��";
				break;
			case 3:
				desc = "~1~��ס���ض�Ա,ǿ�г���! Ͷ�˸�����մ";
				break;
			case 4:
				desc = "~1~�ܶ��н���,�ڶ����߸����õ����Ѵ���,һ������ ,������Щ��,������������";
				break;
			case 5:
				desc = "~1~�ܶ��н���,�ڶ����߸����õ����Ѵ���,һ������ ,������Щ��,������������";
				break;
			case 6:
				desc = "~1~��~3~����һ��������Ϻ�,Ͷ������!";
				break;
			case 7:
				desc = "~1~��������,���˷��ض�Ա~2~,��Ͷ�����Ǳ�������һ��,û��!";
				break;
			case 8:
				desc = "~1~������,�����強��������Ա,һ�����ֹ���Ͷ��,�򵯿����";
				break;
			case 9:
				desc = "~1~��~2~���εı�����,ͻ������,����";
				break;
			case 10:
				desc = "~1~��������ǿ�Զ���,��û��";
				break;
			case 11:
				desc = "~1~��Ͷ,����";
				break;
			case 12:
				desc = "~1~�Ų�������,�߸�Ծ��Ͷ��,Ͷ�˸�����մ";
				break;
			case 13:
				desc = "~1~�ڶ������ܵķ���֮ǰǿ�г���,����";
				break;
			case 14:
				desc = "~1~�����״̬���Ǻܺ�,�����ֿ�λͶ��Ҳ����";
				break;
			case 15:
				desc = "~1~�����������һ��������Ա,��Ͷ������������Ա������һ��,û��";
				break;
			default:
				break;
			}
		}
		return desc;
	}

	public String failure(MatchContext context) {
		String desc = null;
		Random random = new Random();
		Player player = context.getCurrentController().getPlayer();
		int trait = TechnicallyTraitHelper.check(player);
		int a = random.nextInt(16);
		return desc;
	}

	public String traitOutSuccess() {
		return null;
	}

	public String traitOutfailure() {
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

}
