package com.ts.dt.dao.impl;

import java.util.List;

import com.ts.dt.dao.MatchNotInPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNotInPlayer;

public class MatchNotInPlayerDaoImpl extends BaseDao implements MatchNotInPlayerDao {

	public void saveMatchNotInPlayers(final List<MatchNotInPlayer> notInPlayers) throws MatchException {
		// TODO Auto-generated method stub
		super.saveMany(notInPlayers);
	}

}
