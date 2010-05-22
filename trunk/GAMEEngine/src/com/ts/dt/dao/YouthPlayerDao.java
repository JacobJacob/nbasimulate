package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;

public interface YouthPlayerDao extends PlayerDao {

	public void save(YouthPlayer player) throws MatchException;

	public Player load(String id) throws MatchException;

	public List<Player> getPlayerWithTeamId(long teamId) throws MatchException;
}
