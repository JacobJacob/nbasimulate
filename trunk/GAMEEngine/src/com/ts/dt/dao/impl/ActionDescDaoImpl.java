package com.ts.dt.dao.impl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import jpersist.DatabaseManager;

import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.po.ActionDesc;
import com.ts.dt.util.DatabaseManagerUtil;

public class ActionDescDaoImpl implements ActionDescDao {

	public List<ActionDesc> findWithActionAndResultAndFlg(String actionNm, String result, String flg) {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		List<ActionDesc> list = new ArrayList<ActionDesc>();
		try {
			Collection<ActionDesc> collection = dbm.loadObjects(new ArrayList<ActionDesc>(), ActionDesc.class, "where :result=? and :actionName=? and :flg=?",
					result, actionNm, flg);
			list.addAll(collection);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}
}
