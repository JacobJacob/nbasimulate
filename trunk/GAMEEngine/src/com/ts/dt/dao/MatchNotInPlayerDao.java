package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNotInPlayer;

public interface MatchNotInPlayerDao {

	public void saveMatchNotInPlayers(List<MatchNotInPlayer> notInPlayers) throws MatchException;

}
