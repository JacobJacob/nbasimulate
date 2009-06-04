package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.Player;

public interface PlayerDao {

	public void save(Player player);

	public Player load(long id);

	public List<Player> getPlayerWithTeamId(long teamId);
}
