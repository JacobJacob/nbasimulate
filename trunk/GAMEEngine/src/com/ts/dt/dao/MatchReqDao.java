package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Matchs;

public interface MatchReqDao {

	public List<Matchs> getAllNewReq() throws MatchException;

	public void update(Matchs matchReq) throws MatchException;
}
