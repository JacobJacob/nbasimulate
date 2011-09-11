set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

/******************************************************************************************/
/* 程序功能：	得到可报名杯赛信息	*/
/* 程序名称：	GetCupRowBySetID			*/
/* 设计人员：	KXT 						*/
/* 版本时间：	2005.08.29 					*/
/******************************************************************************************/
/*Suspend
Sample:
Exec NewBTP.dbo.GetCupRowBySetID 参数 int intUserID,int intSetID,string strCupIDs
*/
ALTER PROCEDURE [dbo].[GetCupRowBySetID]
/*传入参数*/
@UserID int,
@SetID int,
@CupIDs nvarchar(500)
AS

SET NOCOUNT ON	/*关闭执行讯息，以加快速度*/
BEGIN TRANSACTION BeginGetCupRowBySetID	/* 开始添加 */
	DECLARE @Level tinyint,@SQL nvarchar(500),@UnionID int
	
	SELECT @Level=Levels,@UnionID=UnionID FROM BTP_Account WHERE UserID=@UserID
	IF (@Level > 5 and @Level <=10)
        BEGIN
           SET @Level = 10
        END
    ELSE IF(@Level > 10 and @Level <=20)
        BEGIN
           SET @Level = 20
        END
    ELSE IF(@Level > 20 and @Level <=30)
        BEGIN
           SET @Level = 30
        END
    ELSE IF(@Level > 30 and @Level <=40)
        BEGIN
           SET @Level = 40
        END
    ELSE IF(@Level > 40 and @Level <=50)
        BEGIN
           SET @Level = 50
        END
    ELSE IF(@Level > 50 and @Level <=60)
        BEGIN
           SET @Level = 60
        END
    ELSE IF(@Level > 60 and @Level <=70)
        BEGIN
           SET @Level = 70
        END
    ELSE IF(@Level > 70 and @Level <=80)    
        BEGIN
           SET @Level = 80
        END

	SET @SQL='SELECT TOP 1 * FROM BTP_Cup WHERE SetID='+Convert(nvarchar(10),@SetID)+' AND EndRegTime>GetDate()
			AND RegCount<Capacity AND Levels>='+Convert(nvarchar(10),@Level)+' AND (Category=1 OR Category=2 OR Category=8 OR Category=4 OR (Category=3 AND UnionID='
			+Convert(nvarchar(10),@UnionID)+')) AND CupID NOT IN ('+@CupIDs+') ORDER BY Levels ASC'
	EXEC (@SQL)		
COMMIT TRANSACTION EndGetCupRowBySetID	/* 添加结束 */

SET NOCOUNT OFF	/* 回复执行讯息 */
/*传回数据*/

update btp_parameter set AddStatusWealth = 100
update btp_tool set amountinstock = 10000, CoinCost=1000 where toolid = 33

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

/******************************************************************************************/
/* 程序功能：	将球员从Player3表移动到Player5表	*/
/* 程序名称：	SetPlayer3To5				*/
/* 设计人员：	KXT 						*/
/* 版本时间：	2005.09.13 					*/
/******************************************************************************************/
/*Suspend
Sample:
Exec NewBTP.dbo.SetPlayer3To5 参数 int intClubID,long longPlayerID,int intNumber,string strMarketCode
*/
ALTER PROCEDURE [dbo].[SetPlayer3To5]
/*传入参数*/
@ClubID int,
@PlayerID bigint,
@Number tinyint,
@MarketCode char(20)
AS
SET NOCOUNT ON	/*关闭执行讯息，以加快速度*/
BEGIN TRANSACTION BeginSetPlayer3To5	/* 开始添加 */
	DECLARE @Name nchar(20),@Age tinyint,@Pos tinyint,@Face nvarchar(100),@Height tinyint,
			@Weight tinyint,@Speed int,@Jump int,@Strength int,@Stamina int,@Shot int,@Point3 int,@Dribble int,@Pass int,
			@Rebound int,@Steal int,@Block int,@Attack int,@Defense int,@Team int,@SpeedMax int,@JumpMax int,@StrengthMax int,
			@StaminaMax int,@ShotMax int,@Point3Max int,@DribbleMax int,@PassMax int,@ReboundMax int,@StealMax int,@BlockMax int,
			@AttackMax int,@DefenseMax int,@TeamMax int,@Ability int,@PV int,@Status tinyint,@Power tinyint,@Happy tinyint,
			@Salary int,@Contract tinyint,@HeightPoint tinyint,@WeightPoint tinyint,@TrainPoint int,@BodyPotential int,
			@SkillPotential int,@RetireAge tinyint,	@IsRetire bit,@IsDevision bit,@Event nvarchar(255),@Suspend tinyint,@IsHonor bit,
			@AgeF int,@BirthTurn tinyint,@DevID int,@IsStaff bit,@LeadShip int,@Hardness int,@BidPrice3 bigint
			
	SELECT @Name=OldName,@Age=Age,@Pos=Pos,@Face=Face,@Height=Height,@Weight=Weight,@Speed=Speed,@Jump=Jump,@Strength=Strength,
			@Stamina=Stamina,@Shot=Shot,@Point3=Point3,@Dribble=Dribble,@Pass=Pass,@Rebound=Rebound,@Steal=Steal,@Block=Block,
			@Attack=Attack,@Defense=Defense,@Team=Team,@SpeedMax=SpeedMax,@JumpMax=JumpMax,@StrengthMax=StrengthMax,
			@StaminaMax=StaminaMax,@ShotMax=ShotMax,@Point3Max=Point3Max,@DribbleMax=DribbleMax,@PassMax=PassMax,
			@ReboundMax=ReboundMax,@StealMax=StealMax,@BlockMax=BlockMax,@AttackMax=AttackMax,@DefenseMax=DefenseMax,
			@TeamMax=TeamMax,@Ability=Ability,@PV=PV,@Status=Status,@Power=[Power],@Happy=Happy,@HeightPoint=HeightPoint,
			@WeightPoint=WeightPoint,@TrainPoint=TrainPoint,@BodyPotential=BodyPotential,@SkillPotential=SkillPotential,@Event=Event,
			@Suspend=Suspend,@IsHonor=IsHonor,@BirthTurn=BirthTurn,@LeadShip=LeadShip,@Hardness=Hardness,@BidPrice3=BidPrice  
	FROM BTP_Player3 WHERE PlayerID=@PlayerID
	
	SET @DevID=-1
	
	SELECT @DevID=DevID FROM BTP_Dev WHERE ClubID=@ClubID
	
	IF(@DevID=-1)
		BEGIN
			SET @IsStaff=0
		END
	ELSE
		BEGIN
			SET @IsStaff=1
		END
	
	SET @Salary=@PV*(80+30*dbo.GetRandom())*110/100/100
	SET @Contract=26
	SET @IsDevision=1
	SET @RetireAge=28+6*dbo.GetRandom()
	SET @IsRetire=0
	SET @TrainPoint=0
	/*F=26-Age
	If(F<0)F=0
	F=F*2
	增长值：F+Ability.Power(1.5)/AbilityMax*Random(2,6)
	*/
	
	SET @AgeF=26-@Age
	IF(@AgeF<0)
		BEGIN
			SET @AgeF=0
		END
		
	SET @AgeF=@AgeF*2
    SET @AgeF=@AgeF + 1
	
	SET @SpeedMax=@SpeedMax+@AgeF+POWER(@Speed,1.5)/@SpeedMax*(2+4*dbo.GetRandom())
	SET @JumpMax=@JumpMax+@AgeF+POWER(@Jump,1.5)/@JumpMax*(2+4*dbo.GetRandom())
	SET @StrengthMax=@StrengthMax+@AgeF+POWER(@Strength,1.5)/@StrengthMax*(2+4*dbo.GetRandom())
	SET @StaminaMax=@StaminaMax+@AgeF+POWER(@Stamina,1.5)/@StaminaMax*(2+4*dbo.GetRandom())
	SET @ShotMax=@ShotMax+@AgeF+POWER(@Shot,1.5)/@ShotMax*(2+4*dbo.GetRandom())
	SET @Point3Max=@Point3Max+@AgeF+POWER(@Point3,1.5)/@Point3Max*(2+4*dbo.GetRandom())
	SET @DribbleMax=@DribbleMax+@AgeF+POWER(@Dribble,1.5)/@DribbleMax*(2+4*dbo.GetRandom())
	SET @PassMax=@PassMax+@AgeF+POWER(@Pass,1.5)/@PassMax*(2+4*dbo.GetRandom())
	SET @ReboundMax=@ReboundMax+@AgeF+POWER(@Rebound,1.5)/@ReboundMax*(2+4*dbo.GetRandom())
	SET @StealMax=@StealMax+@AgeF+POWER(@Steal,1.5)/@StealMax*(2+4*dbo.GetRandom())
	SET @BlockMax=@BlockMax+@AgeF+POWER(@Block,1.5)/@BlockMax*(2+4*dbo.GetRandom())
	SET @AttackMax=@AttackMax+@AgeF+POWER(@Attack,1.5)/@AttackMax*(2+4*dbo.GetRandom())
	SET @DefenseMax=@DefenseMax+@AgeF+POWER(@Defense,1.5)/@DefenseMax*(2+4*dbo.GetRandom())
	SET @TeamMax=@TeamMax+@AgeF+POWER(@Team,1.5)/@TeamMax*(2+4*dbo.GetRandom())
	
	IF(@SpeedMax>940)
		BEGIN
			SET @SpeedMax=940
		END
	IF(@JumpMax>940)
		BEGIN
			SET @JumpMax=940
		END
	IF(@StrengthMax>940)
		BEGIN
			SET @StrengthMax=940
		END
	IF(@StaminaMax>940)
		BEGIN
			SET @StaminaMax=940
		END
	IF(@ShotMax>940)
		BEGIN
			SET @ShotMax=940
		END
	IF(@Point3Max>940)
		BEGIN
			SET @Point3Max=940
		END
	IF(@DribbleMax>940)
		BEGIN
			SET @DribbleMax=940
		END
	IF(@PassMax>940)
		BEGIN
			SET @PassMax=940
		END
	IF(@ReboundMax>940)
		BEGIN
			SET @ReboundMax=940
		END
	IF(@StealMax>940)
		BEGIN
			SET @StealMax=940
		END
	IF(@BlockMax>940)
		BEGIN
			SET @BlockMax=940
		END
	IF(@AttackMax>940)
		BEGIN
			SET @AttackMax=940
		END
	IF(@DefenseMax>940)
		BEGIN
			SET @DefenseMax=940
		END
	IF(@TeamMax>940)
		BEGIN
			SET @TeamMax=940
		END
	IF(@RetireAge<30)
		BEGIN
			SET @RetireAge=30
		END
	IF(@Age>=(@RetireAge-2))
		BEGIN
			SET @RetireAge=@Age+2
		END
	IF(@Age=42)
		BEGIN
			SET @RetireAge=42
			SET @IsRetire=1
		END
		
	INSERT INTO BTP_Player5 (ClubID,Player3ID,Category,[Name],Age,Pos,Number,Face,Height,Weight,Speed,Jump,Strength,Stamina,Shot,Point3,
			Dribble,Pass,Rebound,Steal,Block,Attack,Defense,Team,SpeedMax,JumpMax,StrengthMax,StaminaMax,ShotMax,
			Point3Max,DribbleMax,PassMax,ReboundMax,StealMax,BlockMax,AttackMax,DefenseMax,TeamMax,Ability,PV,Status,[Power],Happy,
			Salary,Contract,HeightPoint,WeightPoint,TrainPoint,BodyPotential,SkillPotential,RetireAge,IsRetire,IsDevision,MarketCode,Event,
			Suspend,IsHonor,BidStatus,BirthTurn,OldName,IsStaff,LeadShip,Hardness,BidPrice) 
	VALUES (@ClubID,@PlayerID,1,@Name,@Age,@Pos,@Number,@Face,@Height,@Weight,@Speed,@Jump,@Strength,@Stamina,@Shot,@Point3,
			@Dribble,@Pass,@Rebound,@Steal,@Block,@Attack,@Defense,@Team,@SpeedMax,@JumpMax,@StrengthMax,@StaminaMax,@ShotMax,
			@Point3Max,@DribbleMax,@PassMax,@ReboundMax,@StealMax,@BlockMax,@AttackMax,@DefenseMax,@TeamMax,@Ability,@PV,@Status,
			@Power,@Happy,@Salary,@Contract,@HeightPoint,@WeightPoint,@TrainPoint,@BodyPotential,@SkillPotential,@RetireAge,
			@IsRetire,@IsDevision,@MarketCode,@Event,@Suspend,@IsHonor,0,@BirthTurn,@Name,@IsStaff,@LeadShip,@Hardness,@BidPrice3)
			
	DELETE FROM BTP_Player3 WHERE PlayerID=@PlayerID
	
COMMIT TRANSACTION EndSetPlayer3To5	/* 添加结束 */
SET NOCOUNT OFF	/* 回复执行讯息 */



