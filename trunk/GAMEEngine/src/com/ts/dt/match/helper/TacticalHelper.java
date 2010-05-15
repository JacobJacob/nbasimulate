package com.ts.dt.match.helper;

/*
           盯内       盯人      盯外       23   212  32  
 强     75     45    30    55   30   20  
 
 中     40     30    20    75   55   35 
 
  外     35     55    75    15   30   45  

 快     40     75    55    20   30   35  

 整     20     30    15    60   75   55

 掩     20     30    35    35   65   75
 */

public class TacticalHelper {

	public static int[][] TACTICAL_RESTRAINT_TABLE = { 
		{ 75, 45, 30, 55, 30, 20 },
	    { 40, 30, 20, 75, 55, 35 }, 
	    { 35, 55, 75, 15, 30, 45 },
		{ 40, 75, 55, 20, 30, 35 }, 
	    { 20, 30, 15, 60, 75, 55 }, 
	    { 20, 30, 35, 35, 65, 75 }
	};

	// 计算进攻战术对防守战术的克制值
	public static int checkOffensivePoint(int offensiveTactical, int defendTactical) {
		return 100 - TACTICAL_RESTRAINT_TABLE[offensiveTactical-1][defendTactical-7];
	}
}
