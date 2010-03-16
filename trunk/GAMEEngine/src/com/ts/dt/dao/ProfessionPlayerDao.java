package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.ProfessionPlayer;

public interface ProfessionPlayerDao {

	public void save(ProfessionPlayer player);

	public ProfessionPlayer load(String id);

	public List<ProfessionPlayer> getPlayerWithTeamId(long teamId);
}
