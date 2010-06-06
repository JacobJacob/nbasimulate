package com.ts.dt.util;

import java.sql.ResultSet;
import java.sql.SQLException;

import com.ts.dt.po.Player;

public class PlayerUtil {

	public static void result2Object(Player player, ResultSet set) throws SQLException {

		player.setName(set.getString("name"));
		player.setAge(set.getInt("age"));
		player.setPower(set.getInt("power"));
		player.setPosition(set.getString("position"));
		player.setStature(set.getInt("stature"));
		player.setAvoirdupois(set.getInt("avoirdupois"));
		player.setTeamid(set.getLong("team_id"));
		player.setAbility(set.getFloat("ability"));
		player.setShooting(set.getFloat("shooting"));
		player.setId(set.getLong("id"));
		player.setSpeed(set.getFloat("speed"));
		player.setStrength(set.getFloat("strength"));
		player.setBounce(set.getFloat("bounce"));
		player.setStamina(set.getFloat("stamina"));
		player.setTrisection(set.getFloat("trisection"));
		player.setDribble(set.getFloat("dribble"));
		player.setPass(set.getFloat("pass"));
		player.setBackboard(set.getFloat("backboard"));
		player.setSteal(set.getFloat("steal"));
		player.setBlocked(set.getFloat("blocked"));
		player.setDefencons(set.getFloat("defencons"));
		player.setOffencons(set.getFloat("offencons"));
		player.setBuildupcons(set.getFloat("buildupcons"));
		player.setLeadcons(set.getFloat("leadcons"));
		player.setBackbone(set.getFloat("backbone"));
		player.setNo(set.getString("no"));
		player.setTeamId(set.getLong("team_id"));
		player.setMatchPower(set.getInt("match_power"));
		player.setLeaguePower(set.getInt("league_power"));
	}
}
