package com.ts.dt.dao;

import java.util.List;

import com.dt.bottle.persistence.Persistence;
import com.ts.dt.po.ActionDesc;

public interface ActionDescDao {

	public List<ActionDesc> findAll();

	public void save(ActionDesc actionDesc);

	public void remove(long id);

	public ActionDesc find(long id);

	public List<Persistence> findWithActionAndResultAndFlg(String actionNm, String result, String flg);

}
