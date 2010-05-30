package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchReq;
import com.ts.dt.po.Matchs;

public interface MatchReqDao {

	public List<MatchReq> getAllNewReq() throws MatchException;

	public void update(MatchReq matchReq) throws MatchException;

	public MatchReq getOneNewReq() throws MatchException;

	public void update(List<MatchReq> matchReqs) throws MatchException;
}
