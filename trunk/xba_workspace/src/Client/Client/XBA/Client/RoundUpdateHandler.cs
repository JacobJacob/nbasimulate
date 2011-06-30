using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections;
using Web.DBData;
using Client.XBA.Common;
using Web.Helper;

/* 每轮更新客户端 */

namespace Client.XBA.Client
{
    class RoundUpdateHandler:BaseClient
    {
        private int step;
        private int turn;

        public RoundUpdateHandler(int step)
        {
            this.step = step;
        }

        public RoundUpdateHandler()
        {
            this.step = 1;
        }

        private void BeforeRun()
        {
            this.turn = BTPGameManager.GetTurn();

        }

        protected override void run()
        {
            this.BeforeRun();
            if (this.step == 1)
            {
                Console.WriteLine("run step 1");
                this.Player3UpdateStepOne();
                //this.Player5UpdateStepOne();
            }
            else
            {
                Console.WriteLine("run step 2");
                this.TurnFinanceUpdate();
                this.StaffUpdate();
                this.NextTurn();
                this.BuildStadiumUpdate();
                this.Player3UpdateStepTwo();
                this.Player5UpdateStepTwo();
            }

            this.go = false;
          
        }

        private void StaffUpdate()
        {
            /*职员合同处理*/
            BTPStaffManager.StaffContract();

        }


        private void Player3UpdateStepOne()
        {
            Console.WriteLine("start player 3 update one");

            /*年轻球员潜力增长*/
            BTPPlayer3Manager.PlayerSkillMaxUP3();

            /*年轻球员身高体重增长(累计增长因素)*/
            BTPPlayer3Manager.PlayerGrow3();

            /*年轻球员身高体重增长发消息*/
            BTPPlayer3Manager.Player3GrowMsg();

            /*年轻球员身高体重增长(实际增长)*/
            BTPPlayer3Manager.MakePlayer3Grow();

            /*伤病恢复之前先发消息*/
            BTPPlayer3Manager.SendHealthyMessage();

            /*心情恢复*/
            BTPPlayer3Manager.RecoverHappy3();

            /*健康恢复*/
            BTPPlayer3Manager.RecoverHealthy3();

            /*体力恢复*/
            BTPPlayer3Manager.RecoverPower3();

            Console.WriteLine("player 3 update one finish");

        }

        private void Player5UpdateStepOne()
        {
            Console.WriteLine("start player 5 update one");

            /*球员年龄增长*/
            BTPGameManager.AddPlayerAge();

            /*将受伤球员的上一场数据统计在赛前初始化*/
            BTPPlayer5Manager.ClearPlayer5Stas();

            /*球员退役提醒*/
            BTPPlayer5Manager.Player5RetireMsg();

            /*增加球员在队天数*/
            BTPPlayer5Manager.AddTeamDay();

            /*职业球员健康恢复提醒*/
            BTPPlayer5Manager.SendHealthyMessage();

            /*职业球员健康恢复*/
            BTPPlayer5Manager.RecoverHealthy5();

            /*职业球员体力恢复*/
            BTPPlayer5Manager.RecoverPower5();

            /*职业球员合同更新*/
            BTPPlayer5Manager.Player5Contract();

            Console.WriteLine("player 5 update one finish");

        }

        private void Player3UpdateStepTwo()
        {
            Console.WriteLine("start player 3 update two");



            Console.WriteLine("player 3 update two finish");

        }


        private void Player5UpdateStepTwo()
        {
            Console.WriteLine("start player 5 update");


            /*更新球员意识*/
            BTPPlayer5Manager.UpdateAwarenessTrain();

            /*更新球员受欢迎度*/
            BTPPlayer5Manager.ResetAllPlayerPop();

            /*更新球员球衣销售*/
            BTPPlayer5Manager.ResetAllPlayerShirt();

            /*更新球员训练点*/
            BTPPlayer5Manager.TrainPlayer5();

            /*支付球员工资*/
            BTPPlayer5Manager.SendSalary();


            Console.WriteLine("player 5 update finish");

        }

        /*球场建设*/
        private void BuildStadiumUpdate()
        {
            Console.WriteLine("start build stadum update");
            BTPStadiumManager.BuildStadium();
            Console.WriteLine("finish build stadum update");
        }

        private void NextTurn()
        {
            Console.WriteLine("start to next turn update...");
            BTPADLinkManager.NextTurn();
            Console.WriteLine("finish to next turn update...");
        }

        /*每轮财政更新*/
        private void TurnFinanceUpdate()
        {
            Console.WriteLine("start turn finance update...");
            try
            {
                DataTable table = BTPClubManager.GetDevClub5Table();
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        int clubID = (int)row["ClubID"];
                        int userID = (int)row["UserID"];
                        int shirtSum = BTPPlayer5Manager.GetShirtSum(clubID);

                        DataRow match = BTPDevMatchManager.GetDevMRowByClubIDRound(clubID, this.turn);

                        if (match == null)
                        {
                            continue;
                        }

                        int matchId = (int)match["DevMatchID"];
                        int clubIDA = (int)match["ClubHID"];
                        int clubIDB = (int)match["ClubAID"];
                        int homeClubHScore = (int)match["ClubHScore"];
                        int homeClubAScore = (int)match["ClubAScore"];

                        if (clubIDA == 0 || clubIDB == 0)
                        {
                            continue;
                        }

                        if (clubIDA == clubID)
                        {
                            /*主队*/
                            //TODO 球票收入
                            bool win = false;
                            if (homeClubHScore > homeClubAScore)
                            {
                                win = true;
                            }
                            DataRow stadium = BTPStadiumManager.GetStadiumRowByClubID(clubID);
                            int ticketPrice = Convert.ToInt32(stadium["TicketPrice"]);
                            int fansR = Convert.ToInt32(stadium["FansR"]);
                            int fansT = Convert.ToInt32(stadium["FansT"]);
                            int capacity = Convert.ToInt32(stadium["Capacity"]);
                            int ticketSold = this.GetTicketSoldCount(ticketPrice, capacity, fansR + fansT, win);
                            int ticketPriceMoney = ticketSold * ticketPrice;
                            BTPDevMatchManager.UpdateTicketsPrice(matchId, ticketSold, ticketPrice);
                            BTPFinanceManager.AddFinance(userID, 1, 5, ticketPriceMoney, 1, "主场球票收入。");

                            BTPFinanceManager.AddFinance(userID, 1, 5, 1000, 2, "主场球馆维护费用。");
                            BTPFinanceManager.AddFinance(userID, 1, 5, 3600, 1, "主场饮料和食品销售收入。");

                            this.SetTicketSoldInfoToMainXML(matchId, ticketSold, ticketPriceMoney);

                        }


                        /*球衣收入*/
                        int shirtSumMoney = shirtSum * 20;

                        BTPFinanceManager.AddFinance(userID, 1, 5, shirtSumMoney, 1, "球衣销售收入。");
                        
                        
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
            }

            Console.WriteLine("finish turn finance update...");

        }

        private int GetTicketSoldCount(int price, int capacity, int fansCount, bool win)
        {
            int count = 120 - price;
            if (win)
            {
                count = (int)(fansCount * 0.3 * count / 100);
            }
            else
            {
                count = (int)(fansCount * 0.2 * count / 100);
            }

            if (count > capacity)
            {
                count = capacity;
            }
            return count;
        }

        private void SetTicketSoldInfoToMainXML(int matchID, int tickets, int income)
        {
            Hashtable info = new Hashtable();
            info.Add("Tickets", tickets);
            info.Add("Income", income);

            DataRow match = BTPDevMatchManager.GetDevMRowByDevMatchID(matchID);

            if (match == null)
            {
                Console.WriteLine(string.Format("error the match not exist:{0}", matchID));
                return;
            }

            int clubIDA = (int)match["ClubHID"];
            int clubIDB = (int)match["ClubAID"];

            DataRow homeClub = BTPClubManager.GetClubRowByID(clubIDA);
            DataRow awayClub = BTPClubManager.GetClubRowByID(clubIDB);


            /*更新主队的MainXML*/
            string homeOldXml = null;
            if (homeClub["MainXML"] != DBNull.Value)
            {
                homeOldXml = (string)homeClub["MainXML"];
            }
            string homeNewXml = MainXmlHelper.GetNewMainXml(homeOldXml, info);
            BTPClubManager.SetMainXMLByClubID(clubIDA, homeNewXml);
            /*更新客队的MainXML*/
            string awayOldXml = null;
            if (awayClub["MainXML"] != DBNull.Value)
            {
                awayOldXml = (string)awayClub["MainXML"];
            }
            string awayNewXml = MainXmlHelper.GetNewMainXml(awayOldXml, info);
            BTPClubManager.SetMainXMLByClubID(clubIDB, awayNewXml);

        }

    }
}
