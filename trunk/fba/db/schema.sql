/*
由 Aqua Data Studio 7.0.12 于 一月-18-2009 04:57:46 下午
 日生成的脚本数据库：fba
架构：<所有架构>
对象：TABLE
*/
DROP TABLE "player"
GO

CREATE TABLE "player" ( 
	"id"         	bigint(15) AUTO_INCREMENT NOT NULL,
	"name"       	varchar(25) NULL,
    "teamId"     	int(15) NULL,
    "no"         	int(15) NULL,
	"age"        	int(15) NULL,
	"position"   	varchar(25) NULL,
	"stature"    	int(15) NULL,
	"avoirdupois"	int(15) NULL,
	"ability"    	int(15) NULL,
	"shooting"   	int(15) NULL,
	"speed"      	int(15) NULL,
	"strength"   	int(15) NULL,
	"bounce"     	int(15) NULL,
	"stamina"    	int(15) NULL,
	"trisection" 	int(15) NULL,
	"dribble"    	int(15) NULL,
	"pass"       	int(15) NULL,
	"backboard"  	int(15) NULL,
	"steal"      	int(15) NULL,
	"blocked"    	int(15) NULL,
    "shooting_max"   	int(15) NULL,
	"speed_max"      	int(15) NULL,
	"strength_max"   	int(15) NULL,
	"bounce_max"     	int(15) NULL,
	"stamina_max"    	int(15) NULL,
	"trisection_max" 	int(15) NULL,
	"dribble_max"    	int(15) NULL,
	"pass_max"       	int(15) NULL,
	"backboard_max"  	int(15) NULL,
	"steal_max"      	int(15) NULL,
	"blocked_max"    	int(15) NULL,
	PRIMARY KEY("id")
)
CREATE TABLE free_player ( 
    id         	bigint(20) AUTO_INCREMENT NOT NULL,
    name       	varchar(255) NULL,
    age        	int(11) NULL,
    position   	varchar(2) NULL,
    stature    	int(11) NULL,
    avoirdupois	int(11) NULL,
    team_Id    	bigint(20) NULL,
    ability    	double NULL,
    shooting   	smallint(6) NULL,
    speed      	smallint(6) NULL,
    strength   	smallint(6) NULL,
    bounce     	smallint(6) NULL,
    stamina    	smallint(6) NULL,
    trisection 	smallint(6) NULL,
    dribble    	smallint(6) NULL,
    pass       	smallint(6) NULL,
    backboard  	smallint(6) NULL,
    steal      	smallint(6) NULL,
    blocked    	smallint(6) NULL,
    shooting_max   	smallint(6) NULL,
	speed_max    	smallint(6) NULL,
	strength_max   	smallint(6) NULL,
	bounce_max    	smallint(6) NULL,
	stamina_max    	smallint(6) NULL,
	trisection_max 	smallint(6) NULL,
	dribble_max   	smallint(6) NULL,
	pass_max     	smallint(6) NULL,
	backboard_max  	smallint(6) NULL,
	steal_max     	smallint(6) NULL,
	blocked_max    	smallint(6) NULL,
    PRIMARY KEY(id)
)
GO

