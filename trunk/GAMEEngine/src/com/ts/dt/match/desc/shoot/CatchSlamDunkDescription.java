package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.loader.ActionDescLoaderImpl;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.po.ActionDesc;

public class CatchSlamDunkDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			desc = success(context);
		} else if (MatchConstant.RESULT_FAILURE_BLOCKED.equals(result)) {
			desc = blocked(context);
		} else {
			desc = "~1~�ӵ�~3~���,������ֱ�ӿ���,~2~�������,������~1~�Ŀ���,��û�н� . ";
		}

		return desc;
	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;

		ActionDesc actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("CatchSlamDunk", "success",
				"not_foul");

		if (actionDesc.getIsAssist() == true) {
			context.setAssist(true);
		}

		desc = actionDesc.getActionDesc();
		if (context.isFoul()) {
			desc += "ͬʱ�����~2~�ķ���!";
		}
		return desc;
	}

	public String blocked(MatchContext context) {
		String desc = null;
		Random random = new Random();
		int i = random.nextInt(5);
		switch (i) {
		case 0:
			desc = "~1~�Ŀ�����~2~����һ��,û��!";
			break;
		case 1:
			desc = "~1~�ӵ�~3~���,������ֱ�ӿ���,~2~�������,һ����ñ,ֱ�Ӱ������˳�ȥ,~2~�ķ�������̫ǿ��";
			break;
		case 2:
			desc = "~3~����~1~�˵���λ,��װͻ��,ͻȻ����������һ��,���~1~��һ�����н�����~2~ռλ׼ȷ,һ����ñ��ֱ�Ӱ������˳�ȥ!";
			break;
		case 3:
			desc = "~3~����ͻ��,�����˶Է��������ض�Ա�İ���,~1~����Ѿ�����,~3~�������,������������Ա�м����һ��,~1~��������һ������,�����Ǳ�~2~Ӳ�����ĸ�������!";
			break;
		case 4:
			desc = "~1~��~3~���˸�����һ����ֱ�ӿ���,~2~�ӱ���ֱ�Ӱ�����!";
			break;
		default:
			break;
		}
		return desc;
	}

}
