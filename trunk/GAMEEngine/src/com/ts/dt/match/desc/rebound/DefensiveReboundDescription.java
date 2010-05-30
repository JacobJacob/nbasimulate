package com.ts.dt.match.desc.rebound;

import java.util.Random;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class DefensiveReboundDescription implements ActionDescription {

    public String load(MatchContext context) {
	// TODO Auto-generated method stub
	String desc = null;

	if (context.isNotStick()) {
	    desc = "~1~将球得到!";
	} else {
	    Random random = new Random();
	    int ran = random.nextInt(5);
	    switch (ran) {
	    case 1:
		desc = "队友将其它球员挡在外面,~1~控制了防守篮板!";
		break;
	    case 2:
		desc = "~1~从进攻球员头上将球员得到!";
		break;
	    case 3:
		desc = "~1~弹跳惊人,硬生生的将篮板抱住!";
		break;
	    case 4:
		desc = "~1~卡位卡得不错，轻松将球得到!!";
		break;
	    case 5:
		desc = "~1~捡到一个篮板!";
		break;
	    default:
		desc = "~1~单手将球得到!";
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
