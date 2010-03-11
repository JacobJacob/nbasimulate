package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.Matchs;

public interface MatchReqDao {

	public List<Matchs> getAllNewReq();

	public void remove(Matchs matchReq);
}
