package com.ts.dt.po;


public class MatchStat extends Persistence {

	private long teamId;

	private String playerNo;
	private double ability;
	private int age;
	private String name;
	private int no;
	private String position;

	private long matchId;

	private int point2ShootTimes;
	private int point2DoomTimes;

	private int point3ShootTimes;
	private int point3DoomTimes;

	private int point1ShootTimes;
	private int point1DoomTimes;

	private int offensiveRebound;
	private int defensiveRebound;

	private int assist;
	private int lapsus;
	private int block;

	private int foul;
	private int steals;
	private boolean isMain; // ÊÇ·ñÖ÷Á¦

	public long getTeamId() {
		return teamId;
	}

	public void setTeamId(long teamId) {
		this.teamId = teamId;
	}

	public String getPlayerNo() {
		return playerNo;
	}

	public void setPlayerNo(String playerNo) {
		this.playerNo = playerNo;
	}

	public int getPoint2ShootTimes() {
		return point2ShootTimes;
	}

	public void setPoint2ShootTimes(int point2ShootTimes) {
		this.point2ShootTimes = point2ShootTimes;
	}

	public int getPoint2DoomTimes() {
		return point2DoomTimes;
	}

	public void setPoint2DoomTimes(int point2DoomTimes) {
		this.point2DoomTimes = point2DoomTimes;
	}

	public int getPoint3ShootTimes() {
		return point3ShootTimes;
	}

	public void setPoint3ShootTimes(int point3ShootTimes) {
		this.point3ShootTimes = point3ShootTimes;
	}

	public int getPoint3DoomTimes() {
		return point3DoomTimes;
	}

	public void setPoint3DoomTimes(int point3DoomTimes) {
		this.point3DoomTimes = point3DoomTimes;
	}

	public int getPoint1ShootTimes() {
		return point1ShootTimes;
	}

	public void setPoint1ShootTimes(int point1ShootTimes) {
		this.point1ShootTimes = point1ShootTimes;
	}

	public int getPoint1DoomTimes() {
		return point1DoomTimes;
	}

	public void setPoint1DoomTimes(int point1DoomTimes) {
		this.point1DoomTimes = point1DoomTimes;
	}

	public int getOffensiveRebound() {
		return offensiveRebound;
	}

	public void setOffensiveRebound(int offensiveRebound) {
		this.offensiveRebound = offensiveRebound;
	}

	public int getDefensiveRebound() {
		return defensiveRebound;
	}

	public void setDefensiveRebound(int defensiveRebound) {
		this.defensiveRebound = defensiveRebound;
	}

	public long getMatchId() {
		return matchId;
	}

	public void setMatchId(long matchId) {
		this.matchId = matchId;
	}

	public int getFoul() {
		return foul;
	}

	public void setFoul(int foul) {
		this.foul = foul;
	}

	public int getAssist() {
		return assist;
	}

	public void setAssist(int assist) {
		this.assist = assist;
	}

	public int getLapsus() {
		return lapsus;
	}

	public void setLapsus(int lapsus) {
		this.lapsus = lapsus;
	}

	public int getBlock() {
		return block;
	}

	public void setBlock(int block) {
		this.block = block;
	}

	public int getSteals() {
		return steals;
	}

	public void setSteals(int steals) {
		this.steals = steals;
	}

	public boolean getIsMain() {
		return isMain;
	}

	public void setIsMain(boolean isMain) {
		this.isMain = isMain;
	}

	public double getAbility() {
		return ability;
	}

	public void setAbility(double ability) {
		this.ability = ability;
	}

	public int getAge() {
		return age;
	}

	public void setAge(int age) {
		this.age = age;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getNo() {
		return no;
	}

	public void setNo(int no) {
		this.no = no;
	}

	public String getPosition() {
		return position;
	}

	public void setPosition(String position) {
		this.position = position;
	}

	public void setMain(boolean isMain) {
		this.isMain = isMain;
	}

}
