package com.ts.dt.po;

import java.io.Serializable;

public class User extends Persistence implements Serializable {

	public static final long serialVersionUID = -2805454943653427093L;

	private String name;
	private String password;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

}
