package com.ts.dt;

import com.ts.dt.monitor.MatchReqHandle;
import com.ts.dt.monitor.MatchReqMonitor;

public class Main {

	public static void main(String[] args) {

		new MatchReqMonitor().start();
		for (int i = 0; i <= 20; i++) {
			new MatchReqHandle().start();
		}
		// new MatchReqHandle().start();
		// new MatchReqHandle().start();
	}
}
