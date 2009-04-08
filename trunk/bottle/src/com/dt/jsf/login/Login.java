package com.dt.jsf.login;

import com.dt.jsf.login.dao.User;

public class Login {
	
	private User user = new User();
	
	private String message;
	
	public String login(){
		
		String result = null ;
		
		if(user.checkUser()){
		  result = "success";
		  message = user.getUserName() + "welcome" ;
		}else{
		  result = "failure";
		  message = "user name not exist or password error!";

		}
		return result;
		
		
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}
    
}
