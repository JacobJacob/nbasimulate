package com.ts.dt;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.po.MatchReq;

public class MainTest {

	public static void main(String[] args) {

		Session session = BottleUtil.currentSession();
		session.beginTransaction();
		for (int i = 0; i < 2000; i++) {
			MatchReq req = new MatchReq();
			req.setHomeTeamId(1);
			req.setVisitingTeamId(2);
			req.setFlag('N');
			req.save();

		}
		session.endTransaction();
	}
}
