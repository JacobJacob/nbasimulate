package com.ts.dt;

import java.util.Random;

import com.ts.dt.monitor.MatchReqHandle;
import com.ts.dt.monitor.MatchReqMonitor;

public class Main {

	public static void main(String[] args) {

		new MatchReqMonitor().start();
		for (int i = 0; i < 3; i++) {
			String name = "match_handle_client-" + String.valueOf(i);
			new MatchReqHandle(name).start();
			try {
				Thread.sleep(new Random().nextInt(5000));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

	}
}
