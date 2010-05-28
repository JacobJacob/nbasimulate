package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.Query;
import org.hibernate.Session;

import com.ts.dt.dao.YouthPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.YouthPlayer;
import com.ts.dt.util.HibernateUtil;

public class YouthPlayerDaoImpl extends BaseDao implements YouthPlayerDao {

	public void update(YouthPlayer player) throws MatchException {
		// TODO Auto-generated method stub
		super.update(player);
	}

	public Player load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Player player = (Player) super.load(YouthPlayer.class, id);
		return player;
	}

	public Player load(String no) throws MatchException {
		// TODO Auto-generated method stub
		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from YouthPlayer a where a.no = :no");
		q.setString("no", no);
		Player player = (Player) q.uniqueResult();
		return player;
	}

	public void update(List<Player> players) throws MatchException {
		// TODO Auto-generated method stub
		super.updateMany(players);

	}

	public List<Player> getPlayerWithTeamId(long teamId) {

		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from YouthPlayer a where a.teamId = :teamId");
		q.setLong("teamId", teamId);
		List<Player> list = q.list();
		return list;
	}
}
