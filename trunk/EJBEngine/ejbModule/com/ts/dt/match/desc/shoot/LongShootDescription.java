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
				desc = "~1~要让对手不重视自己的三分能力付出代价,在离三分线还有一米多的地方直接出手,三分球进!";
			} else if (a < 40) {
				desc = "~1~的出手太快了,虽然防守队员已经跟上来,但还是慢了一步,三分球进!";
			} else if (a < 40) {
				desc = "~1~的出手太快了,虽然防守队员已经跟上来,但还是慢了一步,三分球进!";
			} else if (a < 40) {
				desc = "~1~的出手太快了,虽然防守队员已经跟上来,但还是慢了一步,三分球进!";
			} else {
				desc = "~1~的出手太快了,虽然防守队员已经跟上来,但还是慢了一步,三分球进!";
			}

		} else {
			if (a < 20) {
				desc = "~1~见到同伴无人上前接应,只能选择外线出手,球砸在篮筐上弹了出来!";
			} else if (a < 40) {
				desc = "~1~在底线接球,虚晃了一下,侧步躲开防守队员,出手三分,哦,力量小了点,球砸在了篮筐的前沿!";
			} else if (a < 40) {
				desc = "~1~见到同伴无人上前接应,只能选择外线出手,球砸在篮筐上弹了出来!";
			} else if (a < 40) {
				desc = "~1~见到同伴无人上前接应,只能选择外线出手,球砸在篮筐上弹了出来!";
			} else {
				desc = "~1~见到同伴无人上前接应,只能选择外线出手,球砸在篮筐上弹了出来!";
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
