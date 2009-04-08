package com.dt.jsf.hire.action;

import com.dt.bottle.session.Session;
import com.dt.jsf.common.BaseAction;
import com.dt.jsf.hire.dao.HireMessage;

public class HireMessageAction extends BaseAction  {
    private HireMessage  hireMessage = new HireMessage();;
	public void create(){
		hireMessage = new HireMessage();
	}
	public void save(){
		Session session = Session.getInstance();
		session.beginTransaction();
		hireMessage.save();
		session.endTransaction();
	}
	public HireMessage getHireMessage() {
		return hireMessage;
	}
	public void setHireMessage(HireMessage hireMessage) {
		this.hireMessage = hireMessage;
	}
	
}
