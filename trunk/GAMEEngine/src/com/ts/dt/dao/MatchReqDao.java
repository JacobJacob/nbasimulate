package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.MatchReq;

public interface MatchReqDao {

	public List<MatchReq> getAllNewReq();

	public void remove(MatchReq matchReq);
}
