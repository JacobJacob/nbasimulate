package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.ActionDesc;

public interface ActionDescDao {

	public List<ActionDesc> findWithActionAndResultAndFlg(String actionNm, String result, String flg);

}
