package com.ts.dt.monitor;

import java.util.Iterator;
import java.util.List;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.dao.impl.MatchReqDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchReq;
import com.ts.dt.pool.MatchReqPool;
import com.ts.dt.util.Logger;

public class MatchReqMonitor extends Thread {

	MatchReqDao matchReqDao = null;
	private String name;

	public MatchReqMonitor() {
		this.name = "match request monitor";
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			if (MatchReqPool.size() == 0) {

				try {

					matchReqDao = new MatchReqDaoImpl();

					List<MatchReq> list = matchReqDao.getAllNewReq();

					Iterator<MatchReq> iterator = list.iterator();
					if (list.size() > 0) {
						Logger.info("Has New Request....");

						while (iterator.hasNext()) {
							MatchReq req = iterator.next();
							req.setPoint("[0:0]");
							req.setStatus(MatchStatus.START);

						}
						matchReqDao.update(list);
						MatchReqPool.put(list);
					}

				} catch (MatchException em) {
					em.printStackTrace();
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
