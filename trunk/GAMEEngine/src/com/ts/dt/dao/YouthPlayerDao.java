package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;

public interface YouthPlayerDao extends PlayerDao {

    public void save(YouthPlayer player);

    public Player load(String id);

    public List<Player> getPlayerWithTeamId(long teamId);
}
