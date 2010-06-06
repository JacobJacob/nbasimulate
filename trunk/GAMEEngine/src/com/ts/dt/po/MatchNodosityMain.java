package com.ts.dt.po;

import java.util.Date;
import java.util.HashSet;
import java.util.Set;

public class MatchNodosityMain {

	private static final long serialVersionUID = 1951394299151330989L;

	private long id;
	private int seq;
	private long matchId;
	private short homeOffensiveTactic;
	private short homeDefendTactic;
	private short guestOffensiveTactic;
	private short guestDefendTactic;
	private String point;
	private Date createdTime = new Date();
	private Set<MatchNodosityTacticalDetail> list;
	private Set<MatchNodosityDetail> detail;

	public void addDetail(MatchNodosityTacticalDetail detail) {
		if (list == null) {
			list = new HashSet<MatchNodosityTacticalDetail>();
		}
		list.add(detail);

	}

	public void addMatchDetailLog(MatchNodosityDetail matchdetail) {
		if (detail == null) {
			detail = new HashSet<MatchNodosityDetail>();
		}
		detail.add(matchdetail);
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

	public short getHomeOffsiveTactic() {
		return homeOffensiveTactic;
	}

	public void setHomeOffsiveTactic(short homeOffsiveTactic) {
		this.homeOffensiveTactic = homeOffsiveTactic;
	}

	public short getHomeDefendTactic() {
		return homeDefendTactic;
	}

	public void setHomeDefendTactic(short homeDefendTactic) {
		this.homeDefendTactic = homeDefendTactic;
	}

	public short getGuestOffsiveTactic() {
		return guestOffensiveTactic;
	}

	public void setGuestOffsiveTactic(short guestOffsiveTactic) {
		this.guestOffensiveTactic = guestOffsiveTactic;
	}

	public short getGuestDefendTactic() {
		return guestDefendTactic;
	}

	public void setGuestDefendTactic(short guestDefendTactic) {
		this.guestDefendTactic = guestDefendTactic;
	}

	public String getPoint() {
		return point;
	}

	public void setPoint(String point) {
		this.point = point;
	}

	public Set<MatchNodosityTacticalDetail> getList() {
		return list;
	}

	public void setList(Set<MatchNodosityTacticalDetail> list) {
		this.list = list;
	}

	public Set<MatchNodosityDetail> getDetail() {
		return detail;
	}

	public void setDetail(Set<MatchNodosityDetail> detail) {
		this.detail = detail;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public short getHomeOffensiveTactic() {
		return homeOffensiveTactic;
	}

	public void setHomeOffensiveTactic(short homeOffensiveTactic) {
		this.homeOffensiveTactic = homeOffensiveTactic;
	}

	public short getGuestOffensiveTactic() {
		return guestOffensiveTactic;
	}

	public void setGuestOffensiveTactic(short guestOffensiveTactic) {
		this.guestOffensiveTactic = guestOffensiveTactic;
	}

	public Date getCreatedTime() {
		return createdTime;
	}

	public void setCreatedTime(Date createdTime) {
		this.createdTime = createdTime;
	}

}
