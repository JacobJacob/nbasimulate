package com.ts.dt.dao.impl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import jpersist.DatabaseManager;
import jpersist.JPersistException;

import com.ts.dt.constants.MatchStatus;
import com.ts.dt.dao.MatchReqDao;
import com.ts.dt.po.Matchs;
import com.ts.dt.util.DatabaseManagerUtil;

public class MatchReqDaoImpl implements MatchReqDao {

	public List<Matchs> getAllNewReq() {

		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		List<Matchs> list = new ArrayList<Matchs>();
		try {
			Collection<Matchs> collection = dbm.loadObjects(new ArrayList<Matchs>(), Matchs.class, "where :status=? limit 5", MatchStatus.ACCP);
			list.addAll(collection);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}

		return list;
	}

	public void save(Matchs matchReq) {
		// TODO Auto-generated method stub
		DatabaseManager dbm = DatabaseManagerUtil.getDatabaseManager();
		try {
			dbm.saveObject(matchReq);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			// try {
			// dbm.close();
			// } catch (JPersistException je) {
			// je.printStackTrace();
			// }
		}
	}

	public static void main(String[] args) {
		MatchReqDaoImpl matchReqDaoImpl = new MatchReqDaoImpl();
		List list = matchReqDaoImpl.getAllNewReq();
		System.out.println(list.size());
	}
}
