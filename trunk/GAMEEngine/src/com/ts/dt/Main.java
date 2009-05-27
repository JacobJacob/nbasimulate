package com.ts.dt;

import com.ts.dt.monitor.MatchReqHandle;
import com.ts.dt.monitor.MatchReqMonitor;

public class Main {

	public static void main(String[] args) {

		new MatchReqMonitor().start();
		new MatchReqHandle().start();
		new MatchReqHandle().start();
		new MatchReqHandle().start();
	}
}
