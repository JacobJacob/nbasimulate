#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql

def create_account():
    
    sql = """
    INSERT INTO [dbo].[BTP_Account] ([UserID], [Category], [PayType], [LockTime], [UserName], [Password], [NickName], [Sex], [DiskURL], [ShortName], [ClubLogo], [Shirt], [Levels], [Score], [Money], [SecName], [SecFace], [UnionID], [UnionCategory], [UnionPosition], [MsgFlag], [FightFlag], [SMatchFlag], [CupIDs], [STrainMatch], [VTrainMatch], [IsChoose], [ChangeName], [QQ], [MemberExpireTime], [GuideCode], [DevLvl], [DevCode], [DevSay], [ActiveTime], [Province], [City], [Birth], [ClubName], [ClubID3], [ClubID5], [CreateTime], [ChangeClubTime], [Order], [UCount], [CCount], [Reputation], [Wealth], [DevCupIDs], [LogoLink], [ContinuePay], [RookieOp], [Email], [CanSendEmail], [DevIncome], [AutoTrain], [AutoTrainTime], [BoxFreeCount], [BoxPayCount], [AdvanceOp], [OnlyDayPoint], [OlnyDayWealth], [OlnyDayWin], [OnlyPoint], [OlnyWealth], [OlnyWin], [UnionReputation], [UnionTime], [UnionPolity], [AutoTrainDev], [UOldCount], [COldCount], [Ischild], [OldUser], [ActivityAdd], [UseStaff], [OnlyUnionPoint], [TeamAbility], [SPassword], [ToolBoxCount], [LoginDate], [SaveUser])
VALUES 
  (%s, 4, 0, 0, N'sxwailyc-%s', N'123', N'测试帐号-%s', 1, N'/', N'', N'', N'5', 1, 0, 0, N'Y.Najera', N'5', 0, 0, N'', 1, 0, 0, N'0', 1, 1, 0, 0, N'414609270', '20090326 16:26:00.667', N'2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,', 0, NULL, N'', '20090326 16:26:42.057', N'�㽭', N'����', N'1986-11-18', N'', 0, 0, '20090326 16:26:00.667', '20051025', 0, 802, 0, 0, 0, N'0', N'', 0, N'0,0,0,0,0,0', N'sales004chenke@gmail.com', 1, 24650, 0, '20090326 16:26:00.667', 0, 0, N'1,1,1,1,1,1,1', 0, 0, 0, 0, 0, 0, 1, '20090319 16:26:00.667', N'0', 0, 796, 4680, 0, 0, N'', 0, 0, 0, N'', 999999, 1, 0)
""" 

    start_id = 1949

    conn = pymssql.connect(host='127.0.0.1', user='BTPAdmin', password='BTPAdmin123', database='NewBTP', as_dict=True)
    try:
        cursor = conn.cursor()
        for i in range(100):
            s = sql % (start_id + i, i + 1, i + 1)
            s = s.decode("gbk").encode('utf8')
            print s
            cursor.execute(s)
        conn.commit()
    except Exception, e:
        a = e.message.decode("gbk")
        print a
        raise
    finally:
        conn.close()
        


if __name__ == "__main__":
    create_account()