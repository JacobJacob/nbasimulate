package com.dt.jsf.hire.dao;

import java.util.Date;

import com.dt.bottle.persistence.Persistence;
import com.dt.jsf.common.DaoInterface;

public class HireMessage extends Persistence implements DaoInterface{
	
   private String title ;
   private String content;
   private long amount;
   private Date issue_date;
   private Date effect_date;
   private String house_type;
   
public String getTitle() {
	return title;
}
public void setTitle(String title) {
	this.title = title;
}
public String getContent() {
	return content;
}
public void setContent(String content) {
	this.content = content;
}
public long getAmount() {
	return amount;
}
public void setAmount(long amount) {
	this.amount = amount;
}
public Date getIssue_date() {
	return issue_date;
}
public void setIssue_date(Date issue_date) {
	this.issue_date = issue_date;
}
public Date getEffect_date() {
	return effect_date;
}
public void setEffect_date(Date effect_date) {
	this.effect_date = effect_date;
}
public String getHouse_type() {
	return house_type;
}
public void setHouse_type(String house_type) {
	this.house_type = house_type;
}
   
   
}
