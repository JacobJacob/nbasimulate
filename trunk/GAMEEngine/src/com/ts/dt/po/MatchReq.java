package com.ts.dt.po;


public class MatchReq {

	public static final long serialVersionUID = -2805454678543428303L;

	private long id;
	private int status;
	private String point;

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public int getStatus() {
		return status;
	}

	public void setStatus(int status) {
		this.status = status;
	}

	public String getPoint() {
		return point;
	}

	public void setPoint(String point) {
		this.point = point;
	}

}
