package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class TeamTactical extends Persistence {

	private long teamId;
	private int type;

	private long tactical_detail_1_id;
	private long tactical_detail_2_id;
	private long tactical_detail_3_id;
	private long tactical_detail_4_id;
	private long tactical_detail_5_id;
	private long tactical_detail_6_id;
	private long tactical_detail_7_id;
	private long tactical_detail_8_id;

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

	public long getTactical_detail_1_id() {
		return tactical_detail_1_id;
	}

	public void setTactical_detail_1_id(long tacticalDetail_1Id) {
		tactical_detail_1_id = tacticalDetail_1Id;
	}

	public long getTactical_detail_2_id() {
		return tactical_detail_2_id;
	}

	public void setTactical_detail_2_id(long tacticalDetail_2Id) {
		tactical_detail_2_id = tacticalDetail_2Id;
	}

	public long getTactical_detail_3_id() {
		return tactical_detail_3_id;
	}

	public void setTactical_detail_3_id(long tacticalDetail_3Id) {
		tactical_detail_3_id = tacticalDetail_3Id;
	}

	public long getTactical_detail_4_id() {
		return tactical_detail_4_id;
	}

	public void setTactical_detail_4_id(long tacticalDetail_4Id) {
		tactical_detail_4_id = tacticalDetail_4Id;
	}

	public long getTactical_detail_5_id() {
		return tactical_detail_5_id;
	}

	public void setTactical_detail_5_id(long tacticalDetail_5Id) {
		tactical_detail_5_id = tacticalDetail_5Id;
	}

	public long getTactical_detail_6_id() {
		return tactical_detail_6_id;
	}

	public void setTactical_detail_6_id(long tacticalDetail_6Id) {
		tactical_detail_6_id = tacticalDetail_6Id;
	}

	public long getTactical_detail_7_id() {
		return tactical_detail_7_id;
	}

	public void setTactical_detail_7_id(long tacticalDetail_7Id) {
		tactical_detail_7_id = tacticalDetail_7Id;
	}

	public long getTactical_detail_8_id() {
		return tactical_detail_8_id;
	}

	public void setTactical_detail_8_id(long tacticalDetail_8Id) {
		tactical_detail_8_id = tacticalDetail_8Id;
	}

	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}

}
