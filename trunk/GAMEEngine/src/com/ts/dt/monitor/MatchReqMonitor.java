package com.ts.dt.monitor;

import java.util.Iterator;
import java.util.List;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.dao.impl.MatchReqDaoImpl;
import com.ts.dt.po.Matchs;
import com.ts.dt.pool.MatchReqPool;
import com.ts.dt.util.Logger;

public class MatchReqMonitor extends Thread {

	MatchReqDao matchReqDao = new MatchReqDaoImpl();

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			List<Matchs> list = matchReqDao.getAllNewReq();
			Iterator<Matchs> iterator = list.iterator();
			if (list.size() > 0) {
				Logger.info("Has New Request....");
			} else {
				Logger.info("Not New Request....");
			}
			while (iterator.hasNext()) {
				Matchs req = iterator.next();
				req.setStatus(1);
				Session session = BottleUtil.currentSession();
				session.beginTransaction();
				req.save();
				session.endTransaction();
				MatchReqPool.put(req);
			}
			try {
				sleep(5000);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
}
