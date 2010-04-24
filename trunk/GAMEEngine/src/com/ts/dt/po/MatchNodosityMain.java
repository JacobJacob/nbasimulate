package com.ts.dt.po;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import com.dt.bottle.exception.SessionException;
import com.dt.bottle.persistence.Persistence;

public class MatchNodosityMain extends Persistence {

    public static final String INSERT_SQL = "insert into match_nodosity_main(seq, match_id, home_offensive_tactic, home_defend_tactic, guest_offensive_tactic, guest_defend_tactic,point, created_time) values(?,?,?,?,?,?,?,now())";
    public static final String GET_LAST_ID = "select LAST_INSERT_ID() as id from match_nodosity_main limit 1";
    public static final String INSERT_LIST_SQL = "insert into match_nodosity_tactical_detail (match_nodosity_main_id,position, player_no, player_name, colligate, created_time) values(?,?,?,?,?, now())";
    public static final String INSERT_DETAIL_SQL = "insert into match_nodosity_detail (match_id, seq, description, time_msg, point_msg, match_nodosity_main_id, created_time, is_new_line) values(?,?,?,?,?,?,now(),?)";

    private int seq;
    private long matchId;
    private short homeOffensiveTactic;
    private short homeDefendTactic;
    private short guestOffensiveTactic;
    private short guestDefendTactic;
    private String point;

    private List<MatchNodosityTacticalDetail> list;
    private List<MatchNodosityDetail> detail;

    public boolean save(Connection connection) {
	try {
	    PreparedStatement statement = connection.prepareStatement(INSERT_SQL);
	    statement.setInt(1, this.seq);
	    statement.setLong(2, this.matchId);
	    statement.setShort(3, this.homeOffensiveTactic);
	    statement.setShort(4, this.homeDefendTactic);
	    statement.setShort(5, this.guestOffensiveTactic);
	    statement.setShort(6, this.guestDefendTactic);
	    statement.setString(7, this.point);
	    statement.execute();

	    statement = connection.prepareStatement(GET_LAST_ID);
	    ResultSet rs = statement.executeQuery();
	    if (!rs.next()) {
		throw new SessionException("error occor while get last id");
	    }
	    Long id = rs.getLong("id");

	    if (this.list.size() > 0) {
		statement = connection.prepareStatement(INSERT_LIST_SQL);
		Iterator<MatchNodosityTacticalDetail> iterator = this.list.iterator();
		while (iterator.hasNext()) {
		    MatchNodosityTacticalDetail detail = iterator.next();
		    statement.setLong(1, id);
		    statement.setString(2, detail.getPosition());
		    statement.setString(3, detail.getPlayerNo());
		    statement.setString(4, detail.getPlayerName());
		    statement.setFloat(5, detail.getColligate());
		    statement.addBatch();
		}
		statement.executeBatch();
	    }

	    if (this.detail.size() > 0) {
		statement = connection.prepareStatement(INSERT_DETAIL_SQL);
		Iterator<MatchNodosityDetail> iterator = this.detail.iterator();
		while (iterator.hasNext()) {
		    MatchNodosityDetail detail = iterator.next();
		    statement.setLong(1, detail.getMatchId());
		    statement.setLong(2, detail.getSeq());
		    statement.setString(3, detail.getDescription());
		    statement.setString(4, detail.getTimeMsg());
		    statement.setString(5, detail.getPointMsg());
		    statement.setFloat(6, id);
		    statement.setBoolean(7, detail.isNewLine());
		    statement.addBatch();
		}
		statement.executeBatch();
	    }

	} catch (Exception e) {
	    e.printStackTrace();
	    return false;
	}
	return true;
    }

    public void addDetail(MatchNodosityTacticalDetail detail) {
	if (list == null) {
	    list = new ArrayList<MatchNodosityTacticalDetail>();
	}
	list.add(detail);

    }

    public void addMatchDetailLog(MatchNodosityDetail matchdetail) {
	if (detail == null) {
	    detail = new ArrayList<MatchNodosityDetail>();
	}
	detail.add(matchdetail);
    }

    public void clear() {
	this.seq = 0;
	this.matchId = 0;
	this.homeDefendTactic = 0;
	this.homeOffensiveTactic = 0;
	this.guestDefendTactic = 0;
	this.guestOffensiveTactic = 0;
	this.point = null;
	this.list.clear();
	this.detail.clear();
    }

    public int getSeq() {
	return seq;
    }

    public void setSeq(int seq) {
	this.seq = seq;
    }

    public long getMatchId() {
	return matchId;
    }

    public void setMatchId(long matchId) {
	this.matchId = matchId;
    }

    public short getHomeOffsiveTactic() {
	return homeOffensiveTactic;
    }

    public void setHomeOffsiveTactic(short homeOffsiveTactic) {
	this.homeOffensiveTactic = homeOffsiveTactic;
    }

    public short getHomeDefendTactic() {
	return homeDefendTactic;
    }

    public void setHomeDefendTactic(short homeDefendTactic) {
	this.homeDefendTactic = homeDefendTactic;
    }

    public short getGuestOffsiveTactic() {
	return guestOffensiveTactic;
    }

    public void setGuestOffsiveTactic(short guestOffsiveTactic) {
	this.guestOffensiveTactic = guestOffsiveTactic;
    }

    public short getGuestDefendTactic() {
	return guestDefendTactic;
    }

    public void setGuestDefendTactic(short guestDefendTactic) {
	this.guestDefendTactic = guestDefendTactic;
    }

    public String getPoint() {
	return point;
    }

    public void setPoint(String point) {
	this.point = point;
    }

    public List<MatchNodosityTacticalDetail> getList() {
	return list;
    }

    public void setList(List<MatchNodosityTacticalDetail> list) {
	this.list = list;
    }

    public List<MatchNodosityDetail> getDetail() {
	return detail;
    }

    public void setDetail(List<MatchNodosityDetail> detail) {
	this.detail = detail;
    }

}
