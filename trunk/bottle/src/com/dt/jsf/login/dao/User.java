package com.dt.jsf.login.dao;

public class User {
	
	private String userName ;
	
	private String password ;

	public String getUserName() {
		return userName;
	}

	public void setUserName(String userName) {
		this.userName = userName;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}
	
	public boolean checkUser(){
		
		if ("Jacky".equals(userName)&&"821015".equals(password)){
			return true;
		}else{
			return false;
		}
		
	}

}
