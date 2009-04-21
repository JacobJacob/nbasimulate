package com.ts.dt.dao.imp;

import java.util.List;

import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.po.ActionDesc;

public class ActionDescDaoImpl implements ActionDescDao {

	public static final String QUERY_SQL = "SELECT * FROM action_desc WHERE action_name = ? AND result = ? AND flg = ?";

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

	public List<Persistence> findWithActionAndResultAndFlg(String actionNm,
			String result, String flg) {
		// TODO Auto-generated method stub
		Session session = BottleUtil.currentSession();
		String[] parm = { actionNm, result, flg };
		List<Persistence> list = null;
		try {
			list = session.query(ActionDesc.class, QUERY_SQL, parm);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}
}
