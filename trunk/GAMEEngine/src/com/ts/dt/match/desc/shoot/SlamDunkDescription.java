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
	    desc = "~1~弹跳力惊人,突然起跳,单手扣篮,对方已经放弃了防守!";
	    break;
	case 1:
	    desc = "~1~用身体撞开~2~,以一记力大势沉的扣篮结束了本次进攻!";
	    break;
	case 2:
	    desc = "~1~在底角接球后,快速启动,~2~己经被甩到身后,轻松双手扣篮!";
	    break;
	case 3:
	    desc = "~1~和~3~做了个空中接力,单身扣篮成功,太精采了,该球应该能入本轮的十佳进球!";
	    break;
	case 4:
	    desc = "~1~用脚步骗过防守的两名队员,轻松将球扣进";
	    break;
	default:
	    break;
	}
	return desc;
    }

    public String failure(MatchContext context) {
	String desc = "";
	desc = "~1~ 突然起跳,想单手扣篮,被~2~结结实实的盖了下来! ";
	return desc;
    }

}
