package com.ts.dt.dao;

import com.ts.dt.po.Player;

public interface PlayerDao {

	public void save(Player player);

	public Player load(long id);
}
