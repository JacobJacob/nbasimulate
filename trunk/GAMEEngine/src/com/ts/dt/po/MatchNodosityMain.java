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

	private List<MatchNodosityDetail> list;

	public void addDetail(MatchNodosityDetail detail) {
		if (list == null) {
			list = new ArrayList<MatchNodosityDetail>();
		}
		list.add(detail);

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

	public List<MatchNodosityDetail> getList() {
		return list;
	}

	public void setList(List<MatchNodosityDetail> list) {
		this.list = list;
	}

}
