package com.ts.dt.match.helper;

import com.ts.dt.context.MatchContext;
import com.ts.dt.po.MatchNodosityDetail;
import com.ts.dt.util.TimeUtil;

public class MatchInfoHelper {

	public static void save(MatchContext context, String desc) {

		MatchNodosityDetail matchDetail = new MatchNodosityDetail();
		// MatchDetailDao matchDetailDao = new MatchDetailDaoImpl();

		String timeMsg = TimeUtil.timeMillis2TimeFormat(context.getContinueTime());
		String pointMsg = context.currentScore();

		matchDetail.setMatchId(context.getMatchId());
		matchDetail.setDescription(desc);
		matchDetail.setSeq(context.getCurrentSeq());
		matchDetail.setTimeMsg(timeMsg);
		matchDetail.setPointMsg(pointMsg);
		matchDetail.setNewLine(context.isNewLine());
		// matchDetailDao.save(matchDetail);
		context.getNodosityMain().addMatchDetailLog(matchDetail);
	}
}
