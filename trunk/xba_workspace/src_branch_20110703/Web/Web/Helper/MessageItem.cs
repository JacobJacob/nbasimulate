namespace Web.Helper
{
    using System;

    public class MessageItem
    {
        public static string GetClubLogo(string strLogo, int intLevels)
        {
            return ("<img src='" + strLogo + "' width='46' height='46' border=0>");
        }

        public static string GetContentInfo(string strContent)
        {
            return ("<div class='DIVContent'>" + strContent + "</div>");
        }

        public static string GetDevMessageInfo(string strContent)
        {
            return ("<div class='DIVDevMessage' valign='middle'>" + strContent + "</div>");
        }

        public static string GetNameInfo(string strNickName)
        {
            return ("<div class=\"DIVPlayerName\">" + strNickName.Trim() + "</div>");
        }

        public static string GetNickNameInfo(int intUserID, string strNickName, int intCategory)
        {
            if (intCategory == 1)
            {
                return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowClub.aspx?UserID=", intUserID, "&Type=3\" target=\"Right\">", strNickName, "</a></div>" });
            }
            return ("<div class=\"DIVPlayerName\"><font color='#FF0000'>" + strNickName.Trim() + "</font></div>");
        }

        public static string GetNickNameInfoMessage(int intUserID, string strNickName, int intCategory)
        {
            if (intCategory == 1)
            {
                return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowClub.aspx?UserID=", intUserID, "&Type=7\" target=\"Right\">", strNickName, "</a></div>" });
            }
            return ("<div class=\"DIVPlayerName\"><font color='#FF0000'>" + strNickName.Trim() + "</font></div>");
        }

        public static string GetNickNameInfoTitle(int intUserID, string strNickName, int intCategory, string strTitle)
        {
            if (intCategory == 1)
            {
                return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a title=\"", strTitle, "\" href=\"ShowClub.aspx?UserID=", intUserID, "&Type=3\" target=\"Right\">", strNickName, "</a></div>" });
            }
            return ("<div class=\"DIVPlayerName\"><font color='#FF0000'>" + strNickName.Trim() + "</font></div>");
        }

        public static string GetNickNameLink(int intUserID, string strNickName, int intCategory)
        {
            if (intCategory == 1)
            {
                return string.Concat(new object[] { "<a href=\"ShowClub.aspx?UserID=", intUserID, "&Type=3\" target=\"Right\">", strNickName, "</a>" });
            }
            return ("<font color='#FF0000'>" + strNickName.Trim() + "</font>");
        }

        public static string GetOptionInfo(int intMessageID, int intCategory, int intUserID, string strNickName)
        {
            if (intCategory == 1)
            {
                return string.Concat(new object[] { "<a href=\"ShowClub.aspx?UserID=", intUserID, "&Type=7\" target=\"Right\">回复</a> | <a href='SecretaryPage.aspx?Type=ADDFRIEND&NickName=", strNickName, "'>加为好友</a> | <a href='MessageDel.aspx?MessageID=", intMessageID, "' onclick='return MessageDel(1);'>删除</a>" });
            }
            if (((intCategory != 3) && (intCategory != 4)) && (intCategory != 5))
            {
                return ("<font color='#999999'>回复</font> | <font color='#999999'>加为好友</font> | <a href='MessageDel.aspx?MessageID=" + intMessageID + "' onclick='return MessageDel(1);'>删除</a>");
            }
            return "<font color='#999999'>回复</font> | <font color='#999999'>加为好友</font> | <font color='#999999'>删除</font>";
        }

        public static string GetRookieMessage(int intKind)
        {
            string str;
            switch (intKind)
            {
                case 1:
                    str = "　您好！欢迎您加入XBA篮球大联盟。<br>　您可以从<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><font color=red>经理手册</font></a>中获得一些经营球队的基本知识。一切由此开始，购买职业队、加入职业联赛、培养球员、球员交易、参加各种比赛磨练球队，最终夺得总冠军！<br>　首先让我们来看看球队管理中的“<font color='blue'>球员管理</font>”，在这里可以查看你的所有球员的基本信息并且可以通过不同的选项卡切换街球队与职业队，点击球员姓名可以看到球员更详细的信息。关于球员管理的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('1');><font color=red>查看帮助</font></a>";
                    break;

                case 2:
                    str = "　查看球队管理中的“<font color='blue'>球员训练</font>”，在这里可以将街球球员的训练点转化为其能力值。在你拥有职业队后，还可以设定职业球员的训练项目。关于球员训练的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('2');><font color=red>查看帮助</font></a>";
                    break;

                case 3:
                    str = "　查看球队管理中的“<font color='blue'>职员管理</font>”，在这里可以雇佣您中意的职员，包括训练员，营养师或者队医，也可以解雇您不满意的职员。关于职员管理的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('3');><font color=red>查看帮助</font></a>";
                    break;

                case 4:
                    str = "　在“<font color='blue'>经理道具</font>”中，可以查看你拥有的道具信息，对于新注册的经理会有新人杯邀请函，凭邀请函，可以参加新人杯街球赛。关于经理道具的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('4');><font color=red>查看帮助</font></a>";
                    break;

                case 5:
                    str = "　查看赛程管理中的“<font color='blue'>街球杯赛</font>”，在这里你可以看到可以报名的新人杯，小杯赛，大杯赛等的赛事，也可以查看你报名的赛事的比赛情况。关于街球杯赛的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('5');><font color=red>查看帮助</font></a>";
                    break;

                case 6:
                    str = "　查看战术设定中的“<font color='blue'>街球战术</font>”，在这里经理可以配备自己球队的三名上场球员，组成一套球队的阵容战术。一共可以为街球队配置3套不同的阵容战术并可以给其取名字，以方便更换。确认后才可生效。关于街球战术的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('6');><font color=red>查看帮助</font></a>";
                    break;

                case 7:
                    str = "　查看“<font color='blue'>转会市场</font>”，在这里可以竞拍您所想要的球员，街头自由球员采用暗拍形式进行，街头选秀只有在你拥有职业队以后才可以在里面进行交易，选秀到的球员将进入职业队，其它转会市场也是针对于职业队的转会市场。每个转会市场都有自己的转会交易制度。关于转会市场的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('7');><font color=red>查看帮助</font></a>";
                    break;

                case 8:
                    str = "　查看球队管理中的“<font color='blue'>球队财政</font>”，在这里你可以看到本赛季的详细财政以及每个赛季的综合财政记录。在本赛季财政记录中还可以查看每轮的财政记录的详细信息。关于球队财政的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('8');><font color=red>查看帮助</font></a>";
                    break;

                case 9:
                    str = "　在首页右边挑选一支职业队“<font color='blue'>购买</font>”，当经理积攒球队资金到足够的金额时，便可以考虑购买一只职业队伍了，并申请参加职业联赛。关于购买职业队的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('9');><font color=red>查看帮助</font></a>";
                    break;

                case 10:
                    str = "　在首页右边点击“<font color='blue'>申请联赛</font>”，在购买到职业队以后可以选择申请进入职业联赛，只要联赛有空位，就可以报名。关于申请联赛的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('10');><font color=red>查看帮助</font></a>";
                    break;

                case 11:
                    str = "　查看“<font color='blue'>约战大厅</font>”，在这里可以向其他经理发起比赛请求，也可以观看比赛结果。进入训练大厅可以选择对手进行训练赛，提高球员的能力。关于约战大厅的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('11');><font color=red>查看帮助</font></a>";
                    break;

                case 12:
                    str = "　查看“<font color='blue'>篮球联盟</font>”，当你的街球等级达到2等级时，如果有联盟的人员向你发出邀请，你就可以加入联盟了。加入联盟可以认识更多的朋友以及参加了联盟内部进行的一系列的赛事。关于篮球联盟的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('12');><font color=red>查看帮助</font></a>";
                    break;

                case 13:
                    str = "　查看赛程管理中的“<font color='blue'>职业联赛</font>”，当你拥有职业队并已经进入职业联赛的某个赛区以后，你在这里可以看到关于联赛的所有信息。包括联赛赛程，联赛排名，技术统计等。关于职业联赛的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('13');><font color=red>查看帮助</font></a>";
                    break;

                case 14:
                    str = "　查看战术设定中的“<font color='blue'>职业战术</font>”，在这里你可以为你的职业队伍设置4种常用的阵容战术，并可以配置自己的5名职业队球员组成一套阵容，安排阵容时只需点击在场上的球衣号码就可将其放入替补席，<br>拖动替补席位上的球衣号码到相应的位置即可将此球员安排进入阵容。还可以更改战术名称以方便使用和更换阵容。战术制订完后需要确认才能生效。关于职业战术的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('14');><font color=red>查看帮助</font></a>";
                    break;

                case 15:
                    str = "　查看球队管理中的“<font color='blue'>球队建设</font>”，当你进入联赛以后，你会拥有一个自己的球场，你在自己的主场打球会获得一定的门票等的收入，还可以悬挂广告来赚取球队运营资金。您球队的表现直接会影响到球迷数量，从而会影响到球队收入。关于球队建设的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('15');><font color=red>查看帮助</font></a>";
                    break;

                case 0x10:
                    str = "　查看球队管理中的“<font color='blue'>球员管理</font>”，在这里可以查看你的所有球员的基本信息并且可以通过不同的选项卡切换街球队与职业队，点击球员姓名可以看到球员更详细的信息。关于球员管理的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('16');><font color=red>查看帮助</font></a>";
                    break;

                case 0x11:
                    str = "　查看“<font color='blue'>短信</font>”，在短信里你可以添加自己的好友，并看到系统或好友给你发送的短信，以及好友列表。关于短信的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('17');><font color=red>查看帮助</font></a>";
                    break;

                case 0x12:
                    str = "　查看球队管理中的“<font color='blue'>球队荣誉</font>”，在这里你可以看到球队所得到的各种奖杯陈列，并且可以在这里添加对你的球队有过重大贡献的值得纪念的球员，是他们成为名人堂的成员。关于球队荣誉的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('18');><font color=red>查看帮助</font></a>";
                    break;

                case 0x13:
                    str = "　查看赛程管理中的“<font color='blue'>XBA杯</font>”，在这里你可以看到职业赛场上最盛大的比赛，所有联赛的精英球队在这里进行竞技。关于XBA杯的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('19');><font color=red>查看帮助</font></a>";
                    break;

                case 20:
                    str = "　查看赛程管理中的“<font color='blue'>至尊杯</font>”，在这里可以看到各大联盟之间的精英参加的代表联盟荣誉的联盟至尊杯。联盟至尊杯只有拥有联盟以及邀请函的经理才有资格参加。至尊杯邀请函，会在联盟杯的优胜者中有系统发放。关于至尊杯的更多内容请点击<a style='cursor:hand;' onclick=NewGuideWin('20');><font color=red>查看帮助</font></a>";
                    break;

                case 0x15:
                    str = "　XBA董事会已经决定聘用您为[ClubName]的总经理兼主教练。董事会相信您可以在新赛季中取得让人满意的成绩。同时，董事会提出了一些中肯的意见和建议，相信会对您有所帮助。<a>点此阅读董事会的来信</a>";
                    break;

                default:
                    str = "";
                    break;
            }
            return ("<font style='line-height:120%'>" + str + "</font>");
        }
    }
}

