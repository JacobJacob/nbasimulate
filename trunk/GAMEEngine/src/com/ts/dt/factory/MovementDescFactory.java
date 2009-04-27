package com.ts.dt.factory;

import java.util.ArrayList;
import java.util.List;

import com.ts.dt.po.Player;
import com.ts.dt.po.Tactics;

public class MovementDescFactory {

	private static MovementDescFactory descFactory;
	private List<String> pgMovementDescs;

	private MovementDescFactory() {
		init();
		load();
	}

	private void init() {

		pgMovementDescs = new ArrayList<String>();
	}

	private void load() {
		
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");
		pgMovementDescs.add("");

	}

	public MovementDescFactory getInstance() {

		if (descFactory == null) {
			descFactory = new MovementDescFactory();
		}
		return descFactory;
	}
	public String getMovementDesc(Player player,Tactics tactics){
		

		return null;
		
	}
}
