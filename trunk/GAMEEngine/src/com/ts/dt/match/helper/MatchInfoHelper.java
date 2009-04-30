package com.ts.dt.match.helper;

import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.MatchDetailDao;
import com.ts.dt.dao.impl.MatchDetailDaoImpl;
import com.ts.dt.po.MatchDetail;
import com.ts.dt.util.TimeUtil;

public class MatchInfoHelper {
	
	public static void save(MatchContext context, String desc) {

		MatchDetail matchDetail = new MatchDetail();
		MatchDetailDao matchDetailDao = new MatchDetailDaoImpl();

		String timeMsg = TimeUtil.timeMillis2TimeFormat(context.getContinueTime());
		String pointMsg = context.currentScore();

		matchDetail.setMatchId(context.getMatchId());
		matchDetail.setDescription(desc);
		matchDetail.setSeq(context.getCurrentSeq());
		matchDetail.setTimeMsg(timeMsg);
		matchDetail.setPointMsg(pointMsg);
		matchDetailDao.save(matchDetail);
	}
}
