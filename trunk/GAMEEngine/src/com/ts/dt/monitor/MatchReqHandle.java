package com.ts.dt.monitor;

import java.io.PrintWriter;
import java.io.StringWriter;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.EngineStatusDao;
import com.ts.dt.dao.ErrorMatchDao;
import com.ts.dt.dao.MatchDao;
import com.ts.dt.dao.impl.EngineStatusDaoImpl;
import com.ts.dt.dao.impl.ErrorMatchDaoImpl;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.engine.MatchEngine;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;
import com.ts.dt.po.ErrorMatch;
import com.ts.dt.po.MatchReq;
import com.ts.dt.po.Matchs;
import com.ts.dt.pool.MatchReqPool;
import com.ts.dt.util.Logger;

public class MatchReqHandle extends Thread {

	private String name;
	private int finishCount = 0;
	private boolean go = true;
	private String msg = "";
	private boolean pause = false;
	private boolean error = false;
	private int status;

	public synchronized boolean isPause() {
		return pause;
	}

	public synchronized void setPause(boolean pause) {
		this.pause = pause;
	}

	public synchronized boolean isError() {
		return error;
	}

	public synchronized void setError(boolean error) {
		this.error = error;
	}

	public MatchReqHandle(String name) {
		this.name = name;
	}

	private MatchEngine engine = new MatchEngineImpl();

	@Override
	public void run() {
		// TODO Auto-generated method stub
		MonitorThread t = new MonitorThread();
		t.start();
		while (go) {
			while (this.isPause()) {
				this.status = 1;
				try {
					sleep(1000 * 20);
				} catch (InterruptedException ie) {
					ie.printStackTrace();
				}
			}
			while (this.isError()) {
				this.status = 3;
				try {
					sleep(1000 * 20);
				} catch (InterruptedException ie) {
					ie.printStackTrace();
				}
			}
			this.status = 2;
			MatchReq matchReq = null;
			Matchs match = null;
			try {
				this.msg = "start to get task";
				matchReq = MatchReqPool.get();
				long match_id = matchReq.getId();
				this.msg = "start to execute match:" + match_id;
				engine.execute(match_id);
				this.finishCount++;
				this.msg = "finish execute match:" + match_id;

				MatchDao matchDao = new MatchDaoImpl();
				match = matchDao.load(match_id);
				match.setStatus(MatchStatus.FINISH);
				match.setClient(name);
				matchDao.update(match);

			} catch (MatchException me) {
				this.error = true;
				me.printStackTrace();
				ErrorMatch errorMatch = new ErrorMatch();
				errorMatch.setMatchId(match.getId());
				StringWriter sw = new StringWriter();
				PrintWriter pw = new PrintWriter(sw);
				me.printStackTrace(pw);
				errorMatch.setRemark(sw.toString());
				errorMatch.setType(match.getType());
				errorMatch.setClient(name);
				Logger.logToDb("error", sw.toString());

				ErrorMatchDao errorMatchDao = new ErrorMatchDaoImpl();
				try {
					errorMatchDao.save(errorMatch);
				} catch (MatchException e) {
					e.printStackTrace();
				}
			} catch (Exception e) {
				e.printStackTrace();
			}

		}

	}

	public class MonitorThread extends Thread {

		public void run() {

			while (go) {
				try {
					this.reportStatus();
					String cmd = this.getCommand();
					if ("PAUSE".equals(cmd)) {
						setPause(true);
					} else if ("CONTINUE".equals(cmd)) {
						setPause(false);
						setError(false);
					} else if ("EXIT".equals(cmd)) {
						go = false;
						status = 4;
						reportStatus();
					}
					sleep(1000 * 20);
				} catch (Throwable t) {
					t.printStackTrace();
				}

			}
		}

		private void reportStatus() throws MatchException {
			EngineStatusDao engineStatusDao = new EngineStatusDaoImpl();
			EngineStatus engineStatus = null;
			boolean update = true;
			engineStatus = engineStatusDao.load(name);
			if (engineStatus == null) {
				engineStatus = new EngineStatus();
				update = false;
			}
			engineStatus.setName(name);
			engineStatus.setStatus(status);
			engineStatus.setInfo(msg + "[handle total:" + finishCount + "]");

			if (update) {
				engineStatusDao.update(engineStatus);
			} else {
				engineStatusDao.save(engineStatus);
			}

		}

		private String getCommand() throws MatchException {
			EngineStatusDao engineStatusDao = new EngineStatusDaoImpl();
			EngineStatus engineStatus = null;
			engineStatus = engineStatusDao.load(name);
			if (engineStatus != null) {
				String cmd = engineStatus.getCmd();
				engineStatus.setCmd(null);
				engineStatusDao.update(engineStatus);
				return cmd;
			}
			return null;

		}

	}

}
