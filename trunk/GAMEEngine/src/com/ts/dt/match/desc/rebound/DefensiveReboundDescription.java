package com.ts.dt.match.desc.rebound;

import java.util.Random;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class DefensiveReboundDescription implements ActionDescription {

    public String load(MatchContext context) {
	// TODO Auto-generated method stub
	String desc = null;

	if (context.isNotStick()) {
	    desc = "~1~����õ�!";
	} else {
	    Random random = new Random();
	    int ran = random.nextInt(5);
	    switch (ran) {
	    case 1:
		desc = "���ѽ�������Ա��������,~1~�����˷�������!";
		break;
	    case 2:
		desc = "~1~�ӽ�����Աͷ�Ͻ���Ա�õ�!";
		break;
	    case 3:
		desc = "~1~��������,Ӳ�����Ľ����屧ס!";
		break;
	    case 4:
		desc = "~1~��λ���ò������ɽ���õ�!!";
		break;
	    case 5:
		desc = "~1~��һ������!";
		break;
	    default:
		desc = "~1~���ֽ���õ�!";
		break;
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
