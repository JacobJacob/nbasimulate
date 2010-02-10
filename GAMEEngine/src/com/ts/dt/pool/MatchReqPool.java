package com.ts.dt.pool;

import java.util.LinkedList;
import java.util.List;

import com.ts.dt.po.MatchReq;

public class MatchReqPool {

	private static List<MatchReq> pool = new LinkedList<MatchReq>();
	private static Object lock = new Object();

	public static void put(MatchReq req) {
		System.err.println("Thread:" + Thread.currentThread() + " start to put a obj....");
		pool.add(req);
		synchronized (lock) {
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
}