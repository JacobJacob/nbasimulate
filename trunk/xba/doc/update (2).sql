set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

/******************************************************************************************/
/* �����ܣ�	�õ�ϵͳ������	*/
/* �������ƣ�	GetSellMoneyPlayer5				*/
/* �����Ա��	fuhang					*/
/* �汾ʱ�䣺	2006.06.30					*/
/******************************************************************************************/

ALTER PROCEDURE [dbo].[GetSellMoneyPlayer5]
/*�������*/
@UserID int,
@PlayerID bigint
AS

SET NOCOUNT ON	/*�ر�ִ��ѶϢ���Լӿ��ٶ�*/
BEGIN TRANSACTION BeginGetSellMoneyPlayer5	/* ��ʼ��� */
	DECLARE @UserIDA int,@ClubID int,@Type int,@PV int
	DECLARE @AgePoint int,@CapitalPoint int,@Capital bigint,@Age int,@BidPrice int
	DECLARE @Height int,@Ability int
	DECLARE @HeightMarket int,@AbilityMarket int,@AgeMarket int,@HeightMarketP int,@AbilityMarketP int,@AgeMarketP int,@MarketLvl tinyint 
	DECLARE @MarketCount int,@DevCode char(20),@IsRetire bit,@StarType tinyint,@StarAllAdd int,@StarLvl int
	
	SELECT @ClubID=ClubID ,@PV=PV,@Age=Age,@Height=Height,@Ability=Ability,@IsRetire=IsRetire,@StarType=StarType,@StarAllAdd=StarAllAdd,@StarLvl=StarLvl FROM BTP_Player5 WITH (NOLOCK) WHERE PlayerID=@PlayerID
	--���ӵ��Ƿ�������Ա���ж�
	IF(@StarType=1)
		BEGIN
			SET @Ability=@Ability+@StarAllAdd/14
		END
	--����
	
	SELECT @DevCode=DevCode FROM BTP_Dev WHERE ClubID=@ClubID
	SET @MarketLvl=LEN(RTRIM(@DevCode))
	IF(@MarketLvl<8)
		BEGIN
			SET @MarketLvl=8
		END
	
	SELECT @UserIDA=UserID FROM BTP_Club WHERE ClubID=@ClubID
	SELECT @Capital=Capital FROM BTP_Game WITH (NOLOCK)
	
	IF(@UserID=@UserIDA)
		BEGIN
			SET @CapitalPoint=@Capital/1000000 + 60
			IF(@CapitalPoint<50)
				BEGIN
					SET @CapitalPoint=50
				END
			ELSE IF(@CapitalPoint>130)
				BEGIN
					SET @CapitalPoint=130
				END
				
			SET @AgePoint=100 + (28-@Age)*10 
			IF(@AgePoint<70)
				BEGIN 
					SET @AgePoint=70
				END
			ELSE IF(@AgePoint>150)
				BEGIN
					SET @AgePoint=150
				END
				
			SELECT @HeightMarket=COUNT(PlayerID) FROM BTP_Player5 WITH (NOLOCK) WHERE Category=2 AND Height<=@Height AND MarketLvl=@MarketLvl
			SELECT @AbilityMarket=COUNT(PlayerID) FROM BTP_Player5 WITH (NOLOCK) WHERE Category=2 AND Ability<=@Ability AND MarketLvl=@MarketLvl
			SELECT @AgeMarket=COUNT(PlayerID) FROM BTP_Player5 WITH (NOLOCK) WHERE Category=2 AND Age>=@Age AND MarketLvl=@MarketLvl
			SELECT @MarketCount=COUNT(PlayerID) FROM BTP_Player5 WITH (NOLOCK) WHERE Category=2 AND MarketLvl=@MarketLvl
			
			SET @HeightMarketP=((@HeightMarket+1)*100/(@MarketCount+1)+50)*15/10 --������100��
			SET @AbilityMarketP=((@AbilityMarket+1)*100/(@MarketCount+1)+50)*13/10 --������100��
			SET @AgeMarketP=(@AgeMarket+1)*100/(@MarketCount+1)+50 --������100��
			
			SET @BidPrice=@HeightMarketP*@AbilityMarketP/100
			SET @BidPrice=@BidPrice*@AgeMarketP/100
			SET @BidPrice=@BidPrice * @PV /100
			SET @BidPrice=@BidPrice * 25 * @AgePoint/100
			SET @BidPrice=@BidPrice * @CapitalPoint / 100
			IF(@Ability<500)
				BEGIN
					SET @BidPrice=@BidPrice*75/100
				END
			ELSE IF(@Ability>=500 AND @Ability<600)
				BEGIN
					SET @BidPrice=@BidPrice
				END
			ELSE IF(@Ability>=600 AND @Ability<650)
				BEGIN
					SET @BidPrice=@BidPrice*15/10
				END
			ELSE IF(@Ability>=650 AND @Ability<700)
				BEGIN
					SET @BidPrice=@BidPrice*2
				END
			ELSE IF(@Ability>=700 AND @Ability<750)
				BEGIN
					SET @BidPrice=@BidPrice*3
				END
			ELSE IF(@Ability>=750)
				BEGIN
					SET @BidPrice=@BidPrice*5
				END

			--���ӵ��Ƿ�������Ա���ж�
			IF(@StarType=1)
				BEGIN
					SET @BidPrice=@BidPrice+@BidPrice*2*@StarLvl/10
				END
			--����

			IF(@IsRetire=1)
				BEGIN
					SET @BidPrice=@BidPrice/3
				END
		END
	ELSE
		BEGIN
			SET @BidPrice=-1
		END
			
COMMIT TRANSACTION EndGetSellMoneyPlayer5	/* ��ӽ��� */

SET NOCOUNT OFF	/* �ظ�ִ��ѶϢ */
/*��������*/
SELECT @BidPrice AS BidPrice



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


/******************************************************************************************/
/* �����ܣ�	ʹ����ʶ��������	*/
/* �������ƣ�	UseHide				*/
/* �����Ա��	fuhang 						*/
/* �汾ʱ�䣺	2006.07.11					*/
/******************************************************************************************/
/*Suspend
Sample:
Exec NewBTP.dbo.UseHide ���� int intUserID,long longPlayerID,int intMarket
*/
ALTER PROCEDURE [dbo].[UseHide]
/*�������*/
@UserID int,
@PlayerID bigint,
@Type int,
@Category int,
@PlayType int
AS

SET NOCOUNT ON	/*�ر�ִ��ѶϢ���Լӿ��ٶ�*/
BEGIN TRANSACTION BeginUseHide	/* ��ʼ��� */
	DECLARE @HideSkill int,@Status tinyint,@UserWealth bigint,@NickName nvarchar(20)
	
	SET @Status=0
	
	SELECT @HideSkill=HideSkill FROM BTP_Parameter
	SELECT @UserWealth=Wealth,@NickName=NickName FROM BTP_Account WHERE UserID=@UserID
	
	IF(@UserWealth>=@HideSkill)
		BEGIN
			INSERT INTO BTP_Player5PLink (UserID,PlayerID,Type,Category)VALUES(@UserID,@PlayerID,@PlayType,@Category)	
	
			/*UPDATE BTP_Account SET [Money]=[Money]-@HideSkill WHERE UserID=@UserID
			Exec NewBTP.dbo.AddFinance @UserID,1,5,@HideSkill,2,'ʹ����ʶ��������'*/
            UPDATE BTP_Account SET Wealth=Wealth-@HideSkill WHERE UserID=@UserID
			Exec NewBTP.dbo.SetWealthFinance @UserID,@NickName,2,@HideSkill,'ʹ����ʶ��������','ʹ����ʶ��������'

			IF(@Type=1 OR @Type=2)
				BEGIN
					SET @Type=1
				END
			ELSE IF(@Type=3)
				BEGIN
					SET @Type=2
				END
			ELSE IF(@Type=4)
				BEGIN
					SET @Type=3
				END
			ELSE IF(@Type=5)
				BEGIN
					SET @Type=5
				END
			ELSE IF(@Type=6)
				BEGIN
					SET @Type=4
				END
			IF (@Type>0)
				BEGIN
					EXEC NewBTP.dbo.AddFocus @UserID,@PlayerID,@PlayType,@Type
				END
		END
COMMIT TRANSACTION EndUseHide	/* ��ӽ��� */

SET NOCOUNT OFF	/* �ظ�ִ��ѶϢ */




/****************************************************************************************/
/* �����ܣ�	��Ϸ�Ҷһ���Ϸ�ʽ�													*/
/* �������ƣ�	ExchangeWealth															*/
/* �����Ա��	KXT 																	*/
/* �汾ʱ�䣺	2007.09.20 																*/
/****************************************************************************************/
CREATE PROCEDURE [dbo].[ExchangeOnlyPoint]
/*�������*/
@UserID int,
@Amount int
AS

SET NOCOUNT ON	/*�ر�ִ��ѶϢ���Լӿ��ٶ�*/
BEGIN TRANSACTION BeginExchangeOnlyPoint	/* ��ʼ��� */
	DECLARE @Type int,@OnlyPoint int,@OnlyUnionPoint int,@Event nvarchar(300),@NickName nvarchar(20),@EventMoney nvarchar(100)
	SET @Type=-1
	
	IF(@Amount>0)
		BEGIN
			SELECT @OnlyPoint=OnlyPoint,@OnlyUnionPoint=OnlyUnionPoint,@NickName=NickName FROM BTP_Account WHERE UserID=@UserID
			IF(@OnlyPoint + @OnlyUnionPoint >=@Amount)
                IF(@OnlyPoint >= @Amount)
                    BEGIN
                        UPDATE BTP_Account SET Wealth=Wealth+@Amount,OnlyPoint=OnlyPoint-@Amount WHERE UserID=@UserID
                        SET @Event='�һ�'+CONVERT(NVARCHAR(12),@Amount)+'���߻��֡�'
                        Exec NewBTP.dbo.SetWealthFinance @UserID,@NickName,2,@Amount,@Event,@Event
                        SET @Type=1
                    END
                ELSE
                    BEGIN
                        UPDATE BTP_Account SET Wealth=Wealth+@Amount,OnlyPoint=0,OnlyUnionPoint=OnlyUnionPoint+OnlyPoint-@Amount WHERE UserID=@UserID
                        SET @Event='�һ�'+CONVERT(NVARCHAR(12),@Amount)+'���߻��֡�'
                        Exec NewBTP.dbo.SetWealthFinance @UserID,@NickName,2,@Amount,@Event,@Event
                        SET @Type=1
                    END
			ELSE
				BEGIN
					SET @Type=-1
				END
		END
	ELSE
		BEGIN
			SET @Type=-2
		END
	
COMMIT TRANSACTION EndExchangeOnlyPoint	/* ��ӽ��� */

SET NOCOUNT OFF	/* �ظ�ִ��ѶϢ */
SELECT @Type AS Type
GO


update BTP_Parameter set HideSkill = 200 

update btp_tool set AmountInStock = 0 where toolid <> 28

update btp_tool set CoinCost = 1500 where toolid = 28
