package com.ts.dt.po;

import com.dt.bottle.persistence.Persistence;

public class Team extends Persistence {

	private String name;
	private long image;
	private long userId;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public long getImage() {
		return image;
	}

	public void setImage(long image) {
		this.image = image;
	}

	public long getUserId() {
		return userId;
	}

	public void setUserId(long userId) {
		this.userId = userId;
	}

}
