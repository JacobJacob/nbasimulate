package com.ts.dt.pool;

import java.util.LinkedList;
import java.util.List;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.dao.impl.MatchDaoImpl;
import com.ts.dt.dao.impl.MatchReqDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchReq;
import com.ts.dt.po.Matchs;

public class MatchReqPool {

	private static List<MatchReq> pool = new LinkedList<MatchReq>();
	private static Object lock = new Object();
	private static Object dbLock = new Object();

	public static void put(MatchReq req) {
		System.err.println("Thread:" + Thread.currentThread() + " start to put a obj....");
		synchronized (lock) {
			pool.add(req);
			System.err.println("Thread:" + Thread.currentThread() + " start to notify others thread....");
			lock.notifyAll();
		}
	}

	public static void put(List<MatchReq> reqs) {
		System.err.println("Thread:" + Thread.currentThread() + " start to put a objs....");
		synchronized (lock) {
			pool.addAll(reqs);
			System.err.println("Thread:" + Thread.currentThread() + " start to notify others thread....");
			lock.notifyAll();
		}
	}

	public static MatchReq get() {
		MatchReq req = null;
		synchronized (lock) {
			while (pool.size() == 0) {

				try {
					System.out.println("Thread:" + Thread.currentThread() + " start to wait....");
					lock.wait();
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
			System.out.println("Thread:" + Thread.currentThread() + " start to notify others thread....");
			System.out.println("-----Thread:" + Thread.currentThread() + "Now Size:" + pool.size());
			req = pool.remove(0);
			lock.notifyAll();
			System.out.println("Thread:" + Thread.currentThread() + " success get a obj....");
		}
		return req;
	}

	public static int size() {
		synchronized (lock) {
			int size = pool.size();
			lock.notifyAll();
			return size;
		}
	}

	public static MatchReq getFromDb() throws MatchException {
		MatchReq req = null;
		synchronized (dbLock) {
			MatchReqDao matchReqDao = new MatchReqDaoImpl();
			req = matchReqDao.getOneNewReq();
			while (req == null) {
				try {
					System.out.println("Thread:" + Thread.currentThread() + " start to wait....");
					dbLock.wait();
				} catch (Exception e) {
					e.printStackTrace();
				}
				req = matchReqDao.getOneNewReq();
			}
			req.setPoint("[0:0]");
			req.setStatus(MatchStatus.START);
			matchReqDao.update(req);
			dbLock.notifyAll();
			System.out.println("Thread:" + Thread.currentThread() + " success get a obj from db....");
		}
		return req;
	}
}
