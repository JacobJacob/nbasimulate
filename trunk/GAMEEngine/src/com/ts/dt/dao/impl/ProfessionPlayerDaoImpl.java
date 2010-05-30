package com.ts.dt.dao.impl;

import java.util.List;

import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;

import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;
import com.ts.dt.po.ProfessionPlayer;
import com.ts.dt.util.HibernateUtil;

public class ProfessionPlayerDaoImpl extends BaseDao implements ProfessionPlayerDao {

	public void update(ProfessionPlayer player) throws MatchException {
		// TODO Auto-generated method stub
		super.update(player);
	}

	public Player load(long id) throws MatchException {
		// TODO Auto-generated method stub
		Player player = (Player) super.load(ProfessionPlayer.class, id);
		if (player == null) {
			throw new MatchException("球员不存在[" + id + "]");
		}
		return player;
	}

	public Player load(String no) throws MatchException {
		// TODO Auto-generated method stub
		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from ProfessionPlayer a where a.no = :no");
		q.setString("no", no);
		Player player = (Player) q.uniqueResult();
		if (player == null) {
			throw new MatchException("球员不存在[" + no + "]");
		}
		return player;

	}

	public void update(List<Player> players) throws MatchException {
		// TODO Auto-generated method stub
		super.updateMany(players);
	}

	public List<Player> getPlayerWithTeamId(long teamId) throws MatchException {

		Session session = HibernateUtil.currentSession();
		Query q = session.createQuery("from ProfessionPlayer a where a.teamId = :teamId");
		q.setLong("teamId", teamId);
		List<Player> list = q.list();
		return list;
	}

	public static void main(String[] args) throws MatchException {
		Player player = new ProfessionPlayerDaoImpl().load("");
		System.out.println(player);
	}
}
