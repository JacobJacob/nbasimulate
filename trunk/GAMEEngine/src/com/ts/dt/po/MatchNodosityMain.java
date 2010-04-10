package com.ts.dt.po;

import java.util.ArrayList;
import java.util.List;

import com.dt.bottle.persistence.Persistence;

public class MatchNodosityMain extends Persistence {

	private int seq;
	private long matchId;
	private long homeTacticId;
	private long visitingTacticId;
	private String point;

	private List<MatchNodosityTacticalDetail> list;
	private List<MatchNodosityDetail> detail;

	public void addDetail(MatchNodosityTacticalDetail detail) {
		if (list == null) {
			list = new ArrayList<MatchNodosityTacticalDetail>();
		}
		list.add(detail);

	}

	public void addMatchDetailLog(MatchNodosityDetail matchdetail) {
		if (detail == null) {
			detail = new ArrayList<MatchNodosityDetail>();
		}
		detail.add(matchdetail);
	}

	public void clear() {
		this.seq = 0;
		this.matchId = 0;
		this.homeTacticId = 0;
		this.visitingTacticId = 0;
		this.point = null;
		this.list.clear();
		this.detail.clear();
	}

	public int getSeq() {
		return seq;
	}

	public void setSeq(int seq) {
		this.seq = seq;
	}

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

	public long getHomeTacticId() {
		return homeTacticId;
	}

	public void setHomeTacticId(long homeTacticId) {
		this.homeTacticId = homeTacticId;
	}

	public long getVisitingTacticId() {
		return visitingTacticId;
	}

	public void setVisitingTacticId(long visitingTacticId) {
		this.visitingTacticId = visitingTacticId;
	}

	public String getPoint() {
		return point;
	}

	public void setPoint(String point) {
		this.point = point;
	}

	public List<MatchNodosityTacticalDetail> getList() {
		return list;
	}

	public void setList(List<MatchNodosityTacticalDetail> list) {
		this.list = list;
	}

	public List<MatchNodosityDetail> getDetail() {
		return detail;
	}

	public void setDetail(List<MatchNodosityDetail> detail) {
		this.detail = detail;
	}

}
