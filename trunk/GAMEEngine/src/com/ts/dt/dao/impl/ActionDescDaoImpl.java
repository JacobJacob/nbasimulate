package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.Query;
import org.hibernate.Session;

import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ActionDesc;
import com.ts.dt.util.HibernateUtil;

public class ActionDescDaoImpl extends BaseDao implements ActionDescDao {

	public List<ActionDesc> findWithActionAndResultAndFlg(String actionNm, String result, String flg) throws MatchException {
		// TODO Auto-generated method stub
		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from ActionDesc a where a.result = :result and a.actionName=:actionName and a.flg=:flg");
		q.setString("result", result);
		q.setString("actionName", actionNm);
		q.setString("flg", flg);
		List<ActionDesc> list = q.list();
		if (list.isEmpty()) {
			throw new MatchException("出现比赛描述为空的情况[" + actionNm + "][" + result + "][" + flg + "]");
		}
		return list;

	}

	public static void main(String[] args) throws MatchException {

		long start = System.currentTimeMillis();
		for (int i = 0; i < 10000; i++) {
			List<ActionDesc> list = new ActionDescDaoImpl().findWithActionAndResultAndFlg("ShortShoot", "success", "not_foul");
			System.out.println(list.size());
		}
		long end = System.currentTimeMillis();
		long useTime = end - start;
		System.out.println("user time[" + useTime + "]");
	}
}
