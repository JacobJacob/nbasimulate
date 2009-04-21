package com.ts.dt.match.desc.pass;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;

public class ShortPassDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String result = context.getPassActionResult();
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			return success(context);
		} else if (MatchConstant.RESULT_FAILURE_BE_STEAL.equals(result)) {
			return loadBeSteal();
		} else {
			return loadIsOutSide();
		}
	}

	public String success(MatchContext context) {

		String desc = null;
		desc = "~1~将球传给了~4~";
		return desc;

	}

	public String failure(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

	public String loadBeSteal() {

		String desc = null;
		desc = "~3~传球,被~2~断了";
		return desc;

	}

	public String loadIsOutSide() {

		String desc = null;
		desc = "~1~传球出界";
		return desc;

	}

}
