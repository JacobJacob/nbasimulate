package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;

public interface ProfessionPlayerDao extends PlayerDao {

	public void save(ProfessionPlayer player) throws MatchException;

	public void save(List<Player> players) throws MatchException;

	public Player load(String id) throws MatchException;

	public List<Player> getPlayerWithTeamId(long teamId) throws MatchException;
}
