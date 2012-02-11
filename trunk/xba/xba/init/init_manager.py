#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection

TRUNCATE_TABLES = ["BTP_Account",
                "BTP_ADLink",
                "BTP_Arrange3",
                "BTP_Arrange5",
                "BTP_ArrangeLvl",
                "BTP_BidAuto",
                "BTP_Bidder",
                "BTP_BidFocus",
                "BTP_BidRecord",
                "BTP_Box",
                "BTP_Cheat",
                "BTP_Cup",
                "BTP_CupMatch",
                "BTP_CupReg",
                "BTP_DevCup",
                "BTP_DevCupMatch",
                "BTP_DevCupReg",
                "BTP_DevMatch",
                "BTP_DevMatchLink",
                "BTP_DevMessage",
                "BTP_DFinance",
                "BTP_Error",
                "BTP_Friend",
                "BTP_FriMatch",
                "BTP_FriMatchMsg",
                "BTP_GameTotal",
                "BTP_Guess",
                "BTP_GuessRecord",
                "BTP_Honor",
                "BTP_InviteCode",
                "BTP_Log",
                "BTP_LoginUserCount",
                "BTP_Message",
                "BTP_MoneyPresent",
                "BTP_MVPPlayer",
                "BTP_Message",
                "BTP_OldPlayer",
                "BTP_Online",
                "BTP_OnlineStatistic",
                "BTP_OnlyOneCenterReg",
                "BTP_Order",
                "BTP_OrderBusiness",
                "BTP_Player3",
                "BTP_Player5",
                "BTP_Player5Plink",
                "BTP_PlayerC",
                "BTP_PointFinance",
                "BTP_Stadium",
                "BTP_StarArrange5",
                "BTP_StarPlayer",
                "BTP_StarPlayerShape",
                "BTP_StarVoteRecord",
                "BTP_TFinance",
                "BTP_ToolLink",
                "BTP_TrainCenterReg",
                "BTP_Union",
                "BTP_UnionBoard",
                "BTP_UnionField",
                "BTP_UnionMessage",
                "BTP_UnionMsgSend",
                "BTP_UnionPolity",
                "BTP_UnionReputation",
                "BTP_UnionTopic",
                "BTP_UWealthFinance",
                "BTP_WealthFinance",
                "BTP_XBATop",
                "BTP_XCupMatch",
                "BTP_XCupReg",
                "BTP_XGame",
                "BTP_XGroupMatch",
                "BTP_XGroupTeam",
                "BTP_XGuess",
                "BTP_XGuessRecord"]

                   


def init():
    """初始化"""
    cursor = connection.cursor()
    try:
        for table in TRUNCATE_TABLES:
            cursor.execute("truncate table %s" % table)
            
        cursor.execute("update btp_game set season=1, turn=1, days=1, oldturn=1 ")
        cursor.execute("update btp_dev set clubid=0,win=0,lose=0,score=0,hasnewmsg=0")    
    finally:
        cursor.close()
    
if __name__ == "__main__":
    init()    
    
    
