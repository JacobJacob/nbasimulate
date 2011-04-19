namespace Web.OneByOneEngine
{
    using System;
    using System.Text;
    using Web.Helper;

    public class Quarter
    {
        public bool blnEnd;
        public bool blnStart;
        public bool blnTurn;
        private int intScriptID = 1;
        public Player pA;
        public Player pH;
        public StringBuilder sbPlayer = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();

        public Quarter(Player pH, Player pA)
        {
            this.pH = pH;
            this.pA = pA;
            this.blnStart = false;
            this.blnTurn = true;
            this.blnEnd = false;
        }

        public void AddBlockStatus()
        {
            if (this.blnTurn)
            {
                this.pA.intBlk++;
            }
            else
            {
                this.pH.intBlk++;
            }
        }

        public bool AddFoulStatus(bool blnIsOff)
        {
            if (blnIsOff)
            {
                if (this.blnTurn)
                {
                    this.pH.intFoul++;
                    this.sbScript.Append(string.Concat(new object[] { this.pH.strName, "犯规", this.pH.intFoul, "次。<br>" }));
                    this.intScriptID++;
                }
                else
                {
                    this.pA.intFoul++;
                    this.sbScript.Append(string.Concat(new object[] { this.pA.strName, "犯规", this.pA.intFoul, "次。<br>" }));
                    this.intScriptID++;
                }
            }
            else if (this.blnTurn)
            {
                this.pA.intFoul++;
                this.sbScript.Append(string.Concat(new object[] { this.pA.strName, "犯规", this.pA.intFoul, "次。<br>" }));
                this.intScriptID++;
            }
            else
            {
                this.pH.intFoul++;
                this.sbScript.Append(string.Concat(new object[] { this.pH.strName, "犯规", this.pH.intFoul, "次。<br>" }));
                this.intScriptID++;
            }
            return true;
        }

        public void AddOffStatus(int intOffMethod, bool blnGood)
        {
            if (blnGood)
            {
                if (intOffMethod == 5)
                {
                    if (this.blnTurn)
                    {
                        this.pH.intScore += 2;
                        this.pH.int3P++;
                        this.pH.int3Ps++;
                    }
                    else
                    {
                        this.pA.intScore += 2;
                        this.pA.int3P++;
                        this.pA.int3Ps++;
                    }
                }
                else if (this.blnTurn)
                {
                    this.pH.intScore++;
                    this.pH.intFG++;
                    this.pH.intFGs++;
                }
                else
                {
                    this.pA.intScore++;
                    this.pA.intFG++;
                    this.pA.intFGs++;
                }
            }
            else if (intOffMethod == 5)
            {
                if (this.blnTurn)
                {
                    this.pH.int3Ps++;
                }
                else
                {
                    this.pA.int3Ps++;
                }
            }
            else if (this.blnTurn)
            {
                this.pH.intFGs++;
            }
            else
            {
                this.pA.intFGs++;
            }
        }

        public void AddRebStatus(bool blnOff)
        {
            if (blnOff)
            {
                if (this.blnTurn)
                {
                    this.pH.intOReb++;
                }
                else
                {
                    this.pA.intOReb++;
                }
            }
            else if (this.blnTurn)
            {
                this.pA.intDReb++;
            }
            else
            {
                this.pH.intDReb++;
            }
        }

        public void AddStealStatus()
        {
            if (this.blnTurn)
            {
                this.pA.intStl++;
            }
            else
            {
                this.pH.intStl++;
            }
        }

        public void AddToStatus()
        {
            if (this.blnTurn)
            {
                this.pH.intTo++;
            }
            else
            {
                this.pA.intTo++;
            }
        }

        public bool GetAfterBlk()
        {
            bool flag;
            if (this.blnTurn)
            {
                int num = this.pH.intOffAbility;
                int num2 = this.pA.intDefAbility;
                if (num > RandomItem.rnd.Next(0, num + (num2 * 5)))
                {
                    flag = true;
                    this.sbScript.Append(this.pH.strName + "得到球，继续进攻！<br>");
                    this.intScriptID++;
                    return flag;
                }
                return false;
            }
            int intOffAbility = this.pA.intOffAbility;
            int intDefAbility = this.pH.intDefAbility;
            if (intOffAbility > RandomItem.rnd.Next(0, intOffAbility + (intDefAbility * 5)))
            {
                flag = true;
                this.sbScript.Append(this.pA.strName + "得到球，继续进攻！<br>");
                this.intScriptID++;
                return flag;
            }
            return false;
        }

        public bool GetReb()
        {
            if (this.blnTurn)
            {
                int num = this.pH.intORebAbility;
                int num2 = this.pA.intDRebAbility;
                if (num > RandomItem.rnd.Next(0, num + num2))
                {
                    this.AddRebStatus(true);
                    return true;
                }
                this.AddRebStatus(false);
                return false;
            }
            int intORebAbility = this.pA.intORebAbility;
            int intDRebAbility = this.pH.intDRebAbility;
            if (intORebAbility > RandomItem.rnd.Next(0, intORebAbility + intDRebAbility))
            {
                this.AddRebStatus(true);
                return true;
            }
            this.AddRebStatus(false);
            return false;
        }

        public bool IsFinished()
        {
            if ((Math.Abs((int) (this.pH.intScore - this.pA.intScore)) <= 1) || ((this.pH.intScore <= 6) && (this.pA.intScore <= 6)))
            {
                return false;
            }
            this.sbScript.Append(string.Concat(new object[] { "比赛结束！", this.pH.strName, "：", this.pH.intScore, " VS ", this.pA.strName, "：", this.pA.intScore, "<br>" }));
            this.intScriptID++;
            return true;
        }

        public void Run()
        {
            while (!this.IsFinished())
            {
                this.RunOneRound(false);
            }
        }

        public void RunOneRound(bool blnIsNextOff)
        {
            if (!this.IsFinished())
            {
                if (!this.blnStart)
                {
                    this.SetPlayerAbility();
                }
                if (!this.blnStart)
                {
                    this.sbScript.Append(this.pH.strName + "三分弧顶试投三分。<br>");
                    this.intScriptID++;
                    if (this.pH.intPoint3 > RandomItem.rnd.Next(0, 0x3e8))
                    {
                        this.blnTurn = true;
                        this.sbScript.Append("球进，" + this.pH.strName + "获得球权。<br>");
                        this.intScriptID++;
                    }
                    else
                    {
                        this.blnTurn = false;
                        this.sbScript.Append("球没进，" + this.pA.strName + "获得球权。<br>");
                        this.intScriptID++;
                    }
                    this.blnStart = true;
                }
                else if (this.blnTurn)
                {
                    int num = this.pH.intOffAbility * 2;
                    int intDefAbility = this.pA.intDefAbility;
                    if (num > RandomItem.rnd.Next(0, num + intDefAbility))
                    {
                        int num3 = RandomItem.rnd.Next(0, 100);
                        int offMethod = this.pH.GetOffMethod();
                        if (num3 < 0x58)
                        {
                            if (offMethod == 5)
                            {
                                if (this.pH.intPoint3 > RandomItem.rnd.Next(0, 100))
                                {
                                    this.AddOffStatus(offMethod, true);
                                    this.sbScript.Append(this.pH.strName + "投中二分！<br>");
                                    this.intScriptID++;
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pH.strName + "二分不中。<br>");
                                    this.intScriptID++;
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append(this.pH.strName + "抢到进攻篮板！<br>");
                                        this.intScriptID++;
                                    }
                                    else
                                    {
                                        this.sbScript.Append(this.pA.strName + "抢下防守篮板。<br>");
                                        this.intScriptID++;
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                            else
                            {
                                this.AddOffStatus(offMethod, true);
                                this.sbScript.Append(this.pH.strName + "投中一分。<br>");
                                this.intScriptID++;
                                this.blnTurn = !this.blnTurn;
                            }
                        }
                        else if ((num3 >= 0x58) && (num3 < 90))
                        {
                            this.AddOffStatus(offMethod, true);
                            if (offMethod == 5)
                            {
                                this.sbScript.Append(this.pH.strName + "投中二分，同时造成" + this.pA.strName + "的犯规。<br>");
                                this.intScriptID++;
                            }
                            else
                            {
                                this.sbScript.Append(this.pH.strName + "投中一分，同时造成" + this.pA.strName + "的犯规。<br>");
                                this.intScriptID++;
                            }
                            this.AddFoulStatus(false);
                        }
                        else
                        {
                            this.AddOffStatus(offMethod, false);
                            if (offMethod == 5)
                            {
                                this.sbScript.Append(this.pH.strName + "二分没有投中，但造成" + this.pA.strName + "的犯规。<br>");
                                this.sbScript.Append(this.pH.strName + "继续开球。<br>");
                                this.intScriptID++;
                                this.AddFoulStatus(false);
                            }
                            else
                            {
                                this.sbScript.Append(this.pH.strName + "没有投中，但造成" + this.pA.strName + "的犯规。<br>");
                                this.sbScript.Append(this.pH.strName + "继续开球。<br>");
                                this.intScriptID++;
                                this.AddFoulStatus(false);
                            }
                        }
                    }
                    else
                    {
                        int num5 = RandomItem.rnd.Next(0, 100);
                        int intOffMethod = this.pH.GetOffMethod();
                        if (num5 < 3)
                        {
                            this.AddFoulStatus(true);
                            this.sbScript.Append(this.pH.strName + "进攻犯规，交换球权。<br>");
                            this.intScriptID++;
                            this.blnTurn = !this.blnTurn;
                        }
                        else if ((num5 >= 3) && (num5 < 10))
                        {
                            this.AddFoulStatus(false);
                            this.sbScript.Append(this.pA.strName + "犯规，" + this.pH.strName + "继续拥有球权。<br>");
                            this.intScriptID++;
                        }
                        else if ((num5 >= 10) && (num5 < 0x19))
                        {
                            int num7 = RandomItem.rnd.Next(0, 100);
                            if (this.pA.intPos > 2)
                            {
                                if (num7 < 30)
                                {
                                    this.sbScript.Append(this.pA.strName + "将" + this.pH.strName + "的球断下。<br>");
                                    this.intScriptID++;
                                    this.AddStealStatus();
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pH.strName + "进攻失误。<br>");
                                    this.intScriptID++;
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else if (num7 < 8)
                            {
                                this.sbScript.Append(this.pA.strName + "将" + this.pH.strName + "的球断下。<br>");
                                this.intScriptID++;
                                this.AddStealStatus();
                                this.AddToStatus();
                                this.blnTurn = !this.blnTurn;
                            }
                            else
                            {
                                this.sbScript.Append(this.pH.strName + "进攻失误。<br>");
                                this.intScriptID++;
                                this.AddToStatus();
                                this.blnTurn = !this.blnTurn;
                            }
                        }
                        else
                        {
                            int num8 = RandomItem.rnd.Next(0, 100);
                            if (this.pA.intPos < 4)
                            {
                                if ((num8 < 0x12) && (this.pA.intBlockAbility > RandomItem.rnd.Next(500, 0x7d0)))
                                {
                                    this.AddOffStatus(intOffMethod, false);
                                    this.sbScript.Append(this.pH.strName + "被" + this.pA.strName + "盖帽。<br>");
                                    this.intScriptID++;
                                    this.AddBlockStatus();
                                    if (!this.GetAfterBlk())
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.AddOffStatus(intOffMethod, false);
                                    this.sbScript.Append(this.pH.strName + "投篮不中。<br>");
                                    this.intScriptID++;
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append(this.pH.strName + "抢到进攻篮板！<br>");
                                        this.intScriptID++;
                                    }
                                    else
                                    {
                                        this.sbScript.Append(this.pA.strName + "抢到防守篮板！<br>");
                                        this.intScriptID++;
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                            else if ((num8 < 3) && (this.pA.intBlockAbility > RandomItem.rnd.Next(500, 0x7d0)))
                            {
                                this.AddOffStatus(intOffMethod, false);
                                this.sbScript.Append(this.pH.strName + "被" + this.pA.strName + "盖帽。<br>");
                                this.intScriptID++;
                                this.AddBlockStatus();
                                if (!this.GetAfterBlk())
                                {
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                this.AddOffStatus(intOffMethod, false);
                                this.sbScript.Append(this.pH.strName + "投篮不中。<br>");
                                this.intScriptID++;
                                if (this.GetReb())
                                {
                                    this.sbScript.Append(this.pH.strName + "抢到进攻篮板！<br>");
                                    this.intScriptID++;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pA.strName + "抢到防守篮板！<br>");
                                    this.intScriptID++;
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                        }
                    }
                    this.pH.intPower -= MatchItem.GetOneByOneLosePower(this.pH.intStamina, 100);
                    if (this.pH.intPower < 30)
                    {
                        this.pH.intPower = 30;
                    }
                    this.pA.intPower -= MatchItem.GetOneByOneLosePower(this.pA.intStamina, 100);
                    if (this.pA.intPower < 30)
                    {
                        this.pA.intPower = 30;
                    }
                }
                else
                {
                    int num9 = this.pA.intOffAbility * 2;
                    int num10 = this.pH.intDefAbility;
                    if (num9 > RandomItem.rnd.Next(0, num9 + num10))
                    {
                        int num11 = RandomItem.rnd.Next(0, 100);
                        int num12 = this.pA.GetOffMethod();
                        if (num11 < 0x58)
                        {
                            if (num12 == 5)
                            {
                                if (this.pA.intPoint3 > RandomItem.rnd.Next(0, 100))
                                {
                                    this.AddOffStatus(num12, true);
                                    this.sbScript.Append(this.pA.strName + "投中二分！<br>");
                                    this.intScriptID++;
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pA.strName + "二分不中。<br>");
                                    this.intScriptID++;
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append(this.pA.strName + "抢到进攻篮板！<br>");
                                        this.intScriptID++;
                                    }
                                    else
                                    {
                                        this.sbScript.Append(this.pH.strName + "抢下防守篮板。<br>");
                                        this.intScriptID++;
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                            else
                            {
                                this.AddOffStatus(num12, true);
                                this.sbScript.Append(this.pA.strName + "投中一分。<br>");
                                this.intScriptID++;
                                this.blnTurn = !this.blnTurn;
                            }
                        }
                        else if ((num11 >= 0x58) && (num11 < 90))
                        {
                            this.AddOffStatus(num12, true);
                            if (num12 == 5)
                            {
                                this.sbScript.Append(this.pA.strName + "投中二分，同时造成" + this.pH.strName + "的犯规。<br>");
                                this.intScriptID++;
                            }
                            else
                            {
                                this.sbScript.Append(this.pA.strName + "投中一分，同时造成" + this.pH.strName + "的犯规。<br>");
                                this.intScriptID++;
                            }
                            this.AddFoulStatus(false);
                        }
                        else
                        {
                            this.AddOffStatus(num12, false);
                            if (num12 == 5)
                            {
                                this.sbScript.Append(this.pA.strName + "二分没有投中，但造成" + this.pH.strName + "的犯规。<br>");
                                this.sbScript.Append(this.pA.strName + "继续开球。<br>");
                                this.intScriptID++;
                                this.AddFoulStatus(false);
                            }
                            else
                            {
                                this.sbScript.Append(this.pA.strName + "没有投中，但造成" + this.pH.strName + "的犯规。<br>");
                                this.sbScript.Append(this.pA.strName + "继续开球。<br>");
                                this.intScriptID++;
                                this.AddFoulStatus(false);
                            }
                        }
                    }
                    else
                    {
                        int num13 = RandomItem.rnd.Next(0, 100);
                        int num14 = this.pA.GetOffMethod();
                        if (num13 < 3)
                        {
                            this.AddFoulStatus(true);
                            this.sbScript.Append(this.pA.strName + "进攻犯规，交换球权。<br>");
                            this.intScriptID++;
                            this.blnTurn = !this.blnTurn;
                        }
                        else if ((num13 >= 3) && (num13 < 10))
                        {
                            this.AddFoulStatus(false);
                            this.sbScript.Append(this.pH.strName + "犯规，" + this.pA.strName + "继续拥有球权。<br>");
                            this.intScriptID++;
                        }
                        else if ((num13 >= 10) && (num13 < 0x19))
                        {
                            int num15 = RandomItem.rnd.Next(0, 100);
                            if (this.pH.intPos > 2)
                            {
                                if (num15 < 30)
                                {
                                    this.sbScript.Append(this.pH.strName + "将" + this.pA.strName + "的球断下。<br>");
                                    this.intScriptID++;
                                    this.AddStealStatus();
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pA.strName + "进攻失误。<br>");
                                    this.intScriptID++;
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else if (num15 < 8)
                            {
                                this.sbScript.Append(this.pH.strName + "将" + this.pA.strName + "的球断下。<br>");
                                this.intScriptID++;
                                this.AddStealStatus();
                                this.AddToStatus();
                                this.blnTurn = !this.blnTurn;
                            }
                            else
                            {
                                this.sbScript.Append(this.pA.strName + "进攻失误。<br>");
                                this.intScriptID++;
                                this.AddToStatus();
                                this.blnTurn = !this.blnTurn;
                            }
                        }
                        else
                        {
                            int num16 = RandomItem.rnd.Next(0, 100);
                            if (this.pA.intPos < 4)
                            {
                                if ((num16 < 0x12) && (this.pH.intBlockAbility > RandomItem.rnd.Next(500, 0x7d0)))
                                {
                                    this.AddOffStatus(num14, false);
                                    this.sbScript.Append(this.pA.strName + "被" + this.pH.strName + "盖帽。<br>");
                                    this.intScriptID++;
                                    this.AddBlockStatus();
                                    if (!this.GetAfterBlk())
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.AddOffStatus(num14, false);
                                    this.sbScript.Append(this.pA.strName + "投篮不中。<br>");
                                    this.intScriptID++;
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append(this.pA.strName + "抢到进攻篮板！<br>");
                                        this.intScriptID++;
                                    }
                                    else
                                    {
                                        this.sbScript.Append(this.pH.strName + "抢到防守篮板！<br>");
                                        this.intScriptID++;
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                            else if ((num16 < 3) && (this.pH.intBlockAbility > RandomItem.rnd.Next(500, 0x7d0)))
                            {
                                this.AddOffStatus(num14, false);
                                this.sbScript.Append(this.pA.strName + "被" + this.pH.strName + "盖帽。<br>");
                                this.intScriptID++;
                                this.AddBlockStatus();
                                if (!this.GetAfterBlk())
                                {
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                this.AddOffStatus(num14, false);
                                this.sbScript.Append(this.pA.strName + "投篮不中。<br>");
                                this.intScriptID++;
                                if (this.GetReb())
                                {
                                    this.sbScript.Append(this.pA.strName + "抢到进攻篮板！<br>");
                                    this.intScriptID++;
                                }
                                else
                                {
                                    this.sbScript.Append(this.pH.strName + "抢到防守篮板！<br>");
                                    this.intScriptID++;
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                        }
                    }
                    this.pA.intPower -= MatchItem.GetOneByOneLosePower(this.pA.intStamina, 100);
                    if (this.pA.intPower < 30)
                    {
                        this.pA.intPower = 30;
                    }
                    this.pH.intPower -= MatchItem.GetOneByOneLosePower(this.pH.intStamina, 100);
                    if (this.pH.intPower < 30)
                    {
                        this.pH.intPower = 30;
                    }
                }
            }
        }

        public void SetPlayerAbility()
        {
            this.pH.SetAbility(this.pH.intPos);
            this.pA.SetAbility(this.pA.intPos);
        }

        public bool SetPlayerInjure()
        {
            int num = RandomItem.rnd.Next(0, 0x2ee0);
            int num2 = 80;
            if (num < 15)
            {
                if (!this.blnTurn)
                {
                    if (RandomItem.rnd.Next(this.pH.intPower, 100) < num2)
                    {
                        this.pH.blnInjured = true;
                        this.sbScript.Append(this.pH.strName + "受伤！<br>");
                        this.intScriptID++;
                    }
                }
                else if (RandomItem.rnd.Next(this.pA.intPower, 100) < num2)
                {
                    this.pA.blnInjured = true;
                    this.sbScript.Append(this.pA.strName + "受伤！<br>");
                    this.intScriptID++;
                }
            }
            else if ((num >= 15) && (num < 30))
            {
                if (this.blnTurn)
                {
                    if (RandomItem.rnd.Next(this.pH.intPower, 100) < num2)
                    {
                        this.pH.blnInjured = true;
                        this.sbScript.Append(this.pH.strName + "受伤！<br>");
                        this.intScriptID++;
                    }
                }
                else if (RandomItem.rnd.Next(this.pA.intPower, 100) < num2)
                {
                    this.pA.blnInjured = true;
                    this.sbScript.Append(this.pA.strName + "受伤！<br>");
                    this.intScriptID++;
                }
            }
            return true;
        }
    }
}

