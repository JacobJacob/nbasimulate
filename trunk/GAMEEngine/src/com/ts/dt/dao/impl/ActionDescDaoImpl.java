package com.ts.dt.dao.impl;

import java.util.List;

import jpersist.DatabaseManager;

import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.po.ActionDesc;
import com.ts.dt.util.DatabaseManagerUtil;

public class ActionDescDaoImpl implements ActionDescDao {

	public static final String QUERY_SQL = "select * from action_desc where action_name = ? and result = ? and flg = ?";

	public ActionDesc find(long id) {
		// TODO Auto-generated method stub
		return null;
	}

	public List<ActionDesc> findAll() {
		// TODO Auto-generated method stub
		return null;
	}

	public void remove(long id) {
		// TODO Auto-generated method stub

	}

	public void save(ActionDesc actionDesc) {
		// TODO Auto-generated method stub

	}

	public List<ActionDesc> findWithActionAndResultAndFlg(String actionNm, String result, String flg) {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		List<ActionDesc> list = null;
		try {
			ActionDesc actionDesc = new ActionDesc();
			actionDesc.setResult(result);
			actionDesc.setActionName(actionNm);
			actionDesc.setFlg(flg);
			dbm.loadObjects(list, actionDesc);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}
}
