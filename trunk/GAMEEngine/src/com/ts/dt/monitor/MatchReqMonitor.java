package com.ts.dt.monitor;

import java.util.Iterator;
import java.util.List;

import com.ts.dt.constants.MatchStatus;
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
			if (MatchReqPool.size() == 0) {

				List<Matchs> list = matchReqDao.getAllNewReq();
				Iterator<Matchs> iterator = list.iterator();
				if (list.size() > 0) {
					Logger.info("Has New Request....");
				}
				while (iterator.hasNext()) {
					Matchs req = iterator.next();
					req.setPoint("[0:0]");
					req.setStatus(MatchStatus.START);
					matchReqDao.save(req);
					MatchReqPool.put(req);
				}
			}
			try {
				sleep(5000);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
}
