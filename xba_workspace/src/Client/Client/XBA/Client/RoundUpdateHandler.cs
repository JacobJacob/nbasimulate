using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections;
using Web.DBData;
using Client.XBA.Common;
using Web.Helper;

/* ÿ�ָ��¿ͻ��� */

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
            /*ְԱ��ͬ����*/
            BTPStaffManager.StaffContract();

        }


        private void Player3UpdateStepOne()
        {
            Console.WriteLine("start player 3 update one");

            /*������ԱǱ������*/
            BTPPlayer3Manager.PlayerSkillMaxUP3();

            /*������Ա�����������(�ۼ���������)*/
            BTPPlayer3Manager.PlayerGrow3();

            /*������Ա���������������Ϣ*/
            BTPPlayer3Manager.Player3GrowMsg();

            /*������Ա�����������(ʵ������)*/
            BTPPlayer3Manager.MakePlayer3Grow();

            /*�˲��ָ�֮ǰ�ȷ���Ϣ*/
            BTPPlayer3Manager.SendHealthyMessage();

            /*����ָ�*/
            BTPPlayer3Manager.RecoverHappy3();

            /*�����ָ�*/
            BTPPlayer3Manager.RecoverHealthy3();

            /*�����ָ�*/
            BTPPlayer3Manager.RecoverPower3();

            Console.WriteLine("player 3 update one finish");

        }

        private void Player5UpdateStepOne()
        {
            Console.WriteLine("start player 5 update one");

            /*��Ա��������*/
            BTPGameManager.AddPlayerAge();

            /*��������Ա����һ������ͳ������ǰ��ʼ��*/
            BTPPlayer5Manager.ClearPlayer5Stas();

            /*��Ա��������*/
            BTPPlayer5Manager.Player5RetireMsg();

            /*������Ա�ڶ�����*/
            BTPPlayer5Manager.AddTeamDay();

            /*ְҵ��Ա�����ָ�����*/
            BTPPlayer5Manager.SendHealthyMessage();

            /*ְҵ��Ա�����ָ�*/
            BTPPlayer5Manager.RecoverHealthy5();

            /*ְҵ��Ա�����ָ�*/
            BTPPlayer5Manager.RecoverPower5();

            /*ְҵ��Ա��ͬ����*/
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


            /*������Ա��ʶ*/
            BTPPlayer5Manager.UpdateAwarenessTrain();

            /*������Ա�ܻ�ӭ��*/
            BTPPlayer5Manager.ResetAllPlayerPop();

            /*������Ա��������*/
            BTPPlayer5Manager.ResetAllPlayerShirt();

            /*������Աѵ����*/
            BTPPlayer5Manager.TrainPlayer5();

            /*֧����Ա����*/
            BTPPlayer5Manager.SendSalary();


            Console.WriteLine("player 5 update finish");

        }

        /*�򳡽���*/
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

        /*ÿ�ֲ�������*/
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
                            /*����*/
                            //TODO ��Ʊ����
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
                            BTPFinanceManager.AddFinance(userID, 1, 5, ticketPriceMoney, 1, "������Ʊ���롣");

                            BTPFinanceManager.AddFinance(userID, 1, 5, 1000, 2, "�������ά�����á�");
                            BTPFinanceManager.AddFinance(userID, 1, 5, 3600, 1, "�������Ϻ�ʳƷ�������롣");

                            this.SetTicketSoldInfoToMainXML(matchId, ticketSold, ticketPriceMoney);

                        }


                        /*��������*/
                        int shirtSumMoney = shirtSum * 20;

                        BTPFinanceManager.AddFinance(userID, 1, 5, shirtSumMoney, 1, "�����������롣");
                        
                        
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


            /*�������ӵ�MainXML*/
            string homeOldXml = null;
            if (homeClub["MainXML"] != DBNull.Value)
            {
                homeOldXml = (string)homeClub["MainXML"];
            }
            string homeNewXml = MainXmlHelper.GetNewMainXml(homeOldXml, info);
            BTPClubManager.SetMainXMLByClubID(clubIDA, homeNewXml);
            /*���¿Ͷӵ�MainXML*/
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
