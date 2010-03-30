package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.Player;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;

public interface ProfessionPlayerDao {

	public void save(ProfessionPlayer player);

	public Player load(String id);

	public List<Player> getPlayerWithTeamId(long teamId);
}
