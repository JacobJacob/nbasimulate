package com.ts.dt.dao;

import com.ts.dt.po.TeamTactics;
import com.ts.dt.po.TeamTacticsDetail;

public interface TacticsDao {

	public TeamTactics loadTeamTactics(long teamId, String matchType);

	public TeamTacticsDetail loadTeamTacticsDetail(long teamId, int seq);
}
