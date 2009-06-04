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
			desc = "~1~接到~3~的妙传,空中想直接扣篮,~2~跳起防守,干扰了~1~的扣篮,球没有进 . ";
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
			desc += "同时造成了~2~的犯规!";
		}
		return desc;
	}

	public String blocked(MatchContext context) {
		String desc = null;
		Random random = new Random();
		int i = random.nextInt(5);
		switch (i) {
		case 0:
			desc = "~1~的扣篮被~2~拍了一下,没进!";
			break;
		case 1:
			desc = "~1~接到~3~的妙传,空中想直接扣篮,~2~跳起防守,一个大帽,直接把球扇了出去,~2~的防守能力太强了";
			break;
		case 2:
			desc = "~3~看到~1~了的跑位,假装突破,突然把球往空中一抛,想和~1~来一个空中接力，~2~占位准确,一个大帽，直接把球扇了出去!";
			break;
		case 3:
			desc = "~3~运球突破,吸引了对方两名防守队员的包夹,~1~这边已经空了,~3~心领神会,在两名防守球员中间把球一扔,~1~接球想来一个暴扣,但还是被~2~硬生生的盖了下来!";
			break;
		case 4:
			desc = "~1~和~3~玩了个二过一后想直接扣篮,~2~从背后直接把球打掉!";
			break;
		default:
			break;
		}
		return desc;
	}

}
