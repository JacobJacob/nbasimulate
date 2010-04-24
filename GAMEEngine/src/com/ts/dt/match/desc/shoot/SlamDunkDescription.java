package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class SlamDunkDescription implements ActionDescription {

    public String load(MatchContext context) {
	// TODO Auto-generated method stub
	String desc = null;
	String result = context.getShootActionResult();
	if (MatchConstant.RESULT_SUCCESS.equals(result)) {
	    desc = success(context);
	} else {
	    desc = failure(context);
	}
	return desc;
    }

    public String success(MatchContext context) {

	String desc = "";

	Random random = new Random();
	int a = random.nextInt(5);
	switch (a) {
	case 0:
	    desc = "~1~����������,ͻȻ����,���ֿ���,�Է��Ѿ������˷���!";
	    break;
	case 1:
	    desc = "~1~������ײ��~2~,��һ�������Ƴ��Ŀ��������˱��ν���!";
	    break;
	case 2:
	    desc = "~1~�ڵ׽ǽ����,��������,~2~������˦�����,����˫�ֿ���!";
	    break;
	case 3:
	    desc = "~1~��~3~���˸����н���,��������ɹ�,̫������,����Ӧ�����뱾�ֵ�ʮ�ѽ���!";
	    break;
	case 4:
	    desc = "~1~�ýŲ�ƭ�����ص�������Ա,���ɽ���۽�";
	    break;
	default:
	    break;
	}
	return desc;
    }

    public String failure(MatchContext context) {
	String desc = "";
	desc = "~1~ ͻȻ����,�뵥�ֿ���,��~2~���ʵʵ�ĸ�������! ";
	return desc;
    }

}
