package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.MatchReq;

public interface MatchReqDao {

	public List<MatchReq> filter(String condition, Object[] parms);

	public void remove(MatchReq matchReq);
}
