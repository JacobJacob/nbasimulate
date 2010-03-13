package com.ts.dt.dao;

import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;

public interface TacticalDao {

	public TeamTactical loadTeamTactical(long teamId, String matchType);

	public TeamTacticalDetail loadTeamTacticalDetail(long teamId, int seq);
}
