package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.TeamTactical;
import com.ts.dt.po.TeamTacticalDetail;

public interface TacticalDao {

	public TeamTactical loadTeamTactical(long teamId, int matchType) throws MatchException;

	public TeamTacticalDetail loadTeamTacticalDetail(long id) throws MatchException;
}
