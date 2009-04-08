package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class LongShootDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		Random random = new Random();
		int a = random.nextInt(100);
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			if (a < 20) {
				desc = "~1~Ҫ�ö��ֲ������Լ�������������������,���������߻���һ�׶�ĵط�ֱ�ӳ���,�������!";
			} else if (a < 40) {
				desc = "~1~�ĳ���̫����,��Ȼ���ض�Ա�Ѿ�������,����������һ��,�������!";
			} else if (a < 40) {
				desc = "~1~�ĳ���̫����,��Ȼ���ض�Ա�Ѿ�������,����������һ��,�������!";
			} else if (a < 40) {
				desc = "~1~�ĳ���̫����,��Ȼ���ض�Ա�Ѿ�������,����������һ��,�������!";
			} else {
				desc = "~1~�ĳ���̫����,��Ȼ���ض�Ա�Ѿ�������,����������һ��,�������!";
			}

		} else {
			if (a < 20) {
				desc = "~1~����ͬ��������ǰ��Ӧ,ֻ��ѡ�����߳���,�����������ϵ��˳���!";
			} else if (a < 40) {
				desc = "~1~�ڵ��߽���,�����һ��,�ಽ�㿪���ض�Ա,��������,Ŷ,����С�˵�,�������������ǰ��!";
			} else if (a < 40) {
				desc = "~1~����ͬ��������ǰ��Ӧ,ֻ��ѡ�����߳���,�����������ϵ��˳���!";
			} else if (a < 40) {
				desc = "~1~����ͬ��������ǰ��Ӧ,ֻ��ѡ�����߳���,�����������ϵ��˳���!";
			} else {
				desc = "~1~����ͬ��������ǰ��Ӧ,ֻ��ѡ�����߳���,�����������ϵ��˳���!";
			}
		}
		return desc;
	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

}
