namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPPlayerManager
    {
        public static void AddPlayer3(int intCategory, string strName, int intAge, int intPos, int intNumber, string strFace, int intPower, int intHappy, int intHeight, int intWeight, int intTrainPoint, int intBodyPotential, int intSkillPotential, int intSpeed, int intJump, int intStrength, int intStamina, int intShot, int intPoint3, int intDribble, int intPass, int intRebound, int intSteal, int intBlock, int intAttack, int intDefense, int intTeam, int intSpeedMax, int intJumpMax, int intStrengthMax, int intStaminaMax, int intShotMax, int intPoint3Max, int intDribbleMax, int intPassMax, int intReboundMax, int intStealMax, int intBlockMax, int intAttackMax, int intDefenseMax, int intTeamMax)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.AddPlayer3 ", intCategory, ",'", strName, "',", intAge, ",", intPos, ",", intNumber, ",'", strFace, "',", intPower, ",", intHappy, 
                ",", intHeight, ",", intWeight, ",", intTrainPoint, ",", intBodyPotential, ",", intSkillPotential, ",", intSpeed, ",", intJump, ",", intStrength, 
                ",", intStamina, ",", intShot, ",", intPoint3, ",", intDribble, ",", intPass, ",", intRebound, ",", intSteal, ",", intBlock, 
                ",", intAttack, ",", intDefense, ",", intTeam, ",", intSpeedMax, ",", intJumpMax, ",", intStrengthMax, ",", intStaminaMax, ",", intShotMax, 
                ",", intPoint3Max, ",", intDribbleMax, ",", intPassMax, ",", intReboundMax, ",", intStealMax, ",", intBlockMax, ",", intAttackMax, ",", intDefenseMax, 
                ",", intTeamMax
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GeneratorFace(int intNumber)
        {
            string[] strArray = "6|9|16|11|16|4|11|33|11|20|2|20|10".Split(new char[] { '|' });
            string str = "";
            int num = 11;
            foreach (string str2 in strArray)
            {
                int num5;
                int maxValue = Convert.ToInt16(str2);
                if (((num != 0x15) && (num != 0x11)) && ((num != 12) && (num != 0x12)))
                {
                    num5 = (num * 0x3e8) + RandomItem.rnd.Next(0, maxValue);
                    str = str + num5.ToString();
                }
                else if (num == 12)
                {
                    if (RandomItem.rnd.Next(0, 100) < 50)
                    {
                        num5 = (num * 0x3e8) + RandomItem.rnd.Next(3, 6);
                        str = str + num5.ToString();
                    }
                    else
                    {
                        num5 = (num * 0x3e8) + RandomItem.rnd.Next(0, maxValue);
                        str = str + num5.ToString();
                    }
                }
                else if (num == 0x11)
                {
                    if (RandomItem.rnd.Next(0, 100) < 80)
                    {
                        num5 = num * 0x3e8;
                        str = str + num5.ToString();
                    }
                    else
                    {
                        num5 = (num * 0x3e8) + RandomItem.rnd.Next(0, maxValue);
                        str = str + num5.ToString();
                    }
                }
                else if (num == 0x12)
                {
                    if (RandomItem.rnd.Next(0, 100) < 30)
                    {
                        num5 = num * 0x3e8;
                        str = str + num5.ToString();
                    }
                    else
                    {
                        num5 = (num * 0x3e8) + RandomItem.rnd.Next(0, maxValue);
                        str = str + num5.ToString();
                    }
                }
                else
                {
                    str = str + (((num * 0x3e8) + intNumber)).ToString();
                }
                if (num < 0x17)
                {
                    str = str + "|";
                }
                num++;
            }
            return str;
        }

        public static DataRow GetPlayerRowByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerRowByPlayerID3 " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void NewPlayer(int intCount, int intCategory)
        {
            NameGenerator generator = new NameGenerator();
            int num22 = 0;
            int num38 = 0;
            for (int i = 0; i < intCount; i++)
            {
                string name = generator.GetName();
                int intAge = RandomItem.rnd.Next(15, 0x16);
                int intNumber = RandomItem.rnd.Next(0, 0x33);
                string strFace = GeneratorFace(intNumber);
                int intTrainPoint = RandomItem.rnd.Next(0xfa0, 0x1770);
                int[] numArray = NewPlayerBody(intAge);
                int intHeight = numArray[0];
                int intWeight = numArray[1];
                int intPos = numArray[2];
                int intBodyPotential = numArray[3];
                int[] numArray2 = NewPlayerSkill(intAge, intHeight, intWeight, intPos);
                int intSpeed = numArray2[0];
                int intJump = numArray2[1];
                int intStrength = numArray2[2];
                int intStamina = numArray2[3];
                int intShot = numArray2[4];
                int num13 = numArray2[5];
                int intDribble = numArray2[6];
                int intPass = numArray2[7];
                int intRebound = numArray2[8];
                int intSteal = numArray2[9];
                int intBlock = numArray2[10];
                int intAttack = numArray2[11];
                int intDefense = numArray2[12];
                int intTeam = numArray2[13];
                int intSkillPotential = numArray2[14];
                for (int j = 0; j < numArray2.Length; j++)
                {
                    num22 += numArray2[j];
                }
                num22 /= 14;
                int[] numArray3 = NewPlayerSkillMax(intAge, intHeight, intWeight);
                int intSpeedMax = numArray3[0];
                int intJumpMax = numArray3[1];
                int intStrengthMax = numArray3[2];
                int intStaminaMax = numArray3[3];
                int intShotMax = numArray3[4];
                int num28 = numArray3[5];
                int intDribbleMax = numArray3[6];
                int intPassMax = numArray3[7];
                int intReboundMax = numArray3[8];
                int intStealMax = numArray3[9];
                int intBlockMax = numArray3[10];
                int intAttackMax = numArray3[11];
                int intDefenseMax = numArray3[12];
                int intTeamMax = numArray3[13];
                for (int k = 0; k < numArray3.Length; k++)
                {
                    num38 += numArray3[k];
                }
                num38 /= 14;
                if (intSpeed > intSpeedMax)
                {
                    intSpeed = intSpeedMax;
                }
                if (intJump > intJumpMax)
                {
                    intJump = intJumpMax;
                }
                if (intStrength > intStrengthMax)
                {
                    intStrength = intStrengthMax;
                }
                if (intStamina > intStaminaMax)
                {
                    intStamina = intStaminaMax;
                }
                if (intShot > intShotMax)
                {
                    intShot = intShotMax;
                }
                if (num13 > num28)
                {
                    num13 = num28;
                }
                if (intDribble > intDribbleMax)
                {
                    intDribble = intDribbleMax;
                }
                if (intPass > intPassMax)
                {
                    intPass = intPassMax;
                }
                if (intRebound > intReboundMax)
                {
                    intRebound = intReboundMax;
                }
                if (intSteal > intStealMax)
                {
                    intSteal = intStealMax;
                }
                if (intBlock > intBlockMax)
                {
                    intBlock = intBlockMax;
                }
                if (intAttack > intAttackMax)
                {
                    intAttack = intAttackMax;
                }
                if (intDefense > intDefenseMax)
                {
                    intDefense = intDefenseMax;
                }
                if (intTeam > intTeamMax)
                {
                    intTeam = intTeamMax;
                }
                AddPlayer3(intCategory, name, intAge, intPos, intNumber, strFace, 100, 100, intHeight, intWeight, intTrainPoint, intBodyPotential, intSkillPotential, intSpeed, intJump, intStrength, intStamina, intShot, num13, intDribble, intPass, intRebound, intSteal, intBlock, intAttack, intDefense, intTeam, intSpeedMax, intJumpMax, intStrengthMax, intStaminaMax, intShotMax, num28, intDribbleMax, intPassMax, intReboundMax, intStealMax, intBlockMax, intAttackMax, intDefenseMax, intTeamMax);
            }
        }

        public static int[] NewPlayerBody(int intAge)
        {
            int num3;
            int num4;
            int num5;
            int num6 = 0;
            int num = RandomItem.rnd.Next(0, 0x3e8);
            int num2 = RandomItem.rnd.Next(0, 100);
            if (intAge < 0x13)
            {
                if (num < 10)
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(80, 90);
                }
                else if ((10 <= num) && (num < 50))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(90, 100);
                }
                else if ((50 <= num) && (num < 100))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(100, 110);
                }
                else if ((100 <= num) && (num < 240))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(0x55, 90);
                }
                else if ((240 <= num) && (num < 660))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(90, 100);
                }
                else if ((660 <= num) && (num < 800))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(100, 0x73);
                }
                else if ((800 <= num) && (num < 900))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(90, 0x5f);
                }
                else if ((900 <= num) && (num < 960))
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(0x5f, 0x69);
                }
                else
                {
                    num3 = 0xa7 + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(0x69, 110);
                }
                if (num3 >= 0xcd)
                {
                    num5 = 1;
                }
                else if ((200 <= num3) && (num3 < 0xcd))
                {
                    if (num2 < 60)
                    {
                        num5 = 1;
                    }
                    else
                    {
                        num5 = 2;
                    }
                }
                else if ((0xc3 <= num3) && (num3 < 200))
                {
                    if (num2 < 40)
                    {
                        num5 = 2;
                    }
                    else
                    {
                        num5 = 3;
                    }
                }
                else if ((190 <= num3) && (num3 < 0xc3))
                {
                    if (num2 < 30)
                    {
                        num5 = 3;
                    }
                    else
                    {
                        num5 = 4;
                    }
                }
                else if ((0xac <= num3) && (num3 < 190))
                {
                    if (num2 < 20)
                    {
                        num5 = 4;
                    }
                    else
                    {
                        num5 = 5;
                    }
                }
                else
                {
                    num5 = 5;
                }
                num6 = RandomItem.rnd.Next(300, (0xda - num3) * 100);
            }
            else
            {
                if (num < 10)
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(0x4b, 0x55);
                }
                else if ((10 <= num) && (num < 50))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(0x55, 0x5f);
                }
                else if ((50 <= num) && (num < 100))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x21, 0x2c);
                    num4 = num3 - RandomItem.rnd.Next(0x5f, 0x69);
                }
                else if ((100 <= num) && (num < 240))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(80, 0x55);
                }
                else if ((240 <= num) && (num < 660))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(0x55, 0x5f);
                }
                else if ((660 <= num) && (num < 800))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0x15, 0x21);
                    num4 = num3 - RandomItem.rnd.Next(0x5f, 110);
                }
                else if ((800 <= num) && (num < 900))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(0x55, 90);
                }
                else if ((900 <= num) && (num < 960))
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(90, 100);
                }
                else
                {
                    num3 = 0xaf + RandomItem.rnd.Next(0, 0x15);
                    num4 = num3 - RandomItem.rnd.Next(100, 0x69);
                }
                if (num3 >= 0xd5)
                {
                    num5 = 1;
                }
                else if ((0xd0 <= num3) && (num3 < 0xd5))
                {
                    if (num2 < 60)
                    {
                        num5 = 1;
                    }
                    else
                    {
                        num5 = 2;
                    }
                }
                else if ((0xcb <= num3) && (num3 < 0xd0))
                {
                    if (num2 < 40)
                    {
                        num5 = 2;
                    }
                    else
                    {
                        num5 = 3;
                    }
                }
                else if ((0xc6 <= num3) && (num3 < 0xcb))
                {
                    if (num2 < 30)
                    {
                        num5 = 3;
                    }
                    else
                    {
                        num5 = 4;
                    }
                }
                else if ((180 <= num3) && (num3 < 0xc6))
                {
                    if (num2 < 20)
                    {
                        num5 = 4;
                    }
                    else
                    {
                        num5 = 5;
                    }
                }
                else
                {
                    num5 = 5;
                }
            }
            return new int[] { num3, num4, num5, num6 };
        }

        public static int[] NewPlayerSkill(int intAge, int intHeight, int intWeight, int intPos)
        {
            int num;
            int num2;
            int num3;
            int num7;
            int num9;
            int num11;
            int num15 = RandomItem.rnd.Next(0xfa0, 0x1b58);
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num3 = (RandomItem.rnd.Next(30, 80) + ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num3 = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num9 = (RandomItem.rnd.Next(30, 80) + ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num9 = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num11 = (RandomItem.rnd.Next(30, 80) + ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num11 = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num = (RandomItem.rnd.Next(160, 210) - ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num2 = (RandomItem.rnd.Next(160, 210) - ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num2 = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                num7 = (RandomItem.rnd.Next(160, 210) - ((((intHeight + intWeight) - 200) * 7) / 10)) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            else
            {
                num7 = RandomItem.rnd.Next(130, 210) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            }
            int num4 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num5 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num6 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num8 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num10 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num12 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num13 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            int num14 = RandomItem.rnd.Next(50, 160) + (((0x144 - ((intAge - 0x21) * (intAge - 0x21))) * RandomItem.rnd.Next(60, 100)) / 100);
            if (RandomItem.rnd.Next(0, 100) < 70)
            {
                switch (intPos)
                {
                    case 1:
                        num7 = (num7 * RandomItem.rnd.Next(70, 80)) / 100;
                        num9 = (num9 * RandomItem.rnd.Next(120, 140)) / 100;
                        num11 = (num11 * RandomItem.rnd.Next(120, 150)) / 100;
                        num6 = (num6 * RandomItem.rnd.Next(20, 40)) / 100;
                        num8 = (num8 * RandomItem.rnd.Next(80, 90)) / 100;
                        num10 = (num10 * RandomItem.rnd.Next(70, 80)) / 100;
                        num14 = (num14 * RandomItem.rnd.Next(70, 80)) / 100;
                        break;

                    case 2:
                        num7 = (num7 * RandomItem.rnd.Next(70, 80)) / 100;
                        num9 = (num9 * RandomItem.rnd.Next(110, 130)) / 100;
                        num11 = (num11 * RandomItem.rnd.Next(100, 120)) / 100;
                        num6 = (num6 * RandomItem.rnd.Next(70, 80)) / 100;
                        num8 = (num8 * RandomItem.rnd.Next(90, 100)) / 100;
                        num10 = (num10 * RandomItem.rnd.Next(70, 80)) / 100;
                        num14 = (num14 * RandomItem.rnd.Next(90, 100)) / 100;
                        break;

                    case 4:
                        num7 = (num7 * RandomItem.rnd.Next(110, 120)) / 100;
                        num9 = (num9 * RandomItem.rnd.Next(70, 90)) / 100;
                        num11 = (num11 * RandomItem.rnd.Next(60, 70)) / 100;
                        num6 = (num6 * RandomItem.rnd.Next(110, 140)) / 100;
                        num14 = (num14 * RandomItem.rnd.Next(100, 110)) / 100;
                        break;

                    case 5:
                        num7 = (num7 * RandomItem.rnd.Next(120, 140)) / 100;
                        num9 = (num9 * RandomItem.rnd.Next(40, 60)) / 100;
                        num11 = (num11 * RandomItem.rnd.Next(30, 40)) / 100;
                        num8 = (num8 * RandomItem.rnd.Next(120, 140)) / 100;
                        num10 = (num10 * RandomItem.rnd.Next(100, 120)) / 100;
                        num14 = (num14 * RandomItem.rnd.Next(120, 140)) / 100;
                        break;
                }
            }
            return new int[] { num, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14, num15 };
        }

        public static int[] NewPlayerSkillMax(int intAge, int intHeight, int intWeight)
        {
            int num;
            int num2;
            int num3;
            int num7;
            int num9;
            int num11;
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num3 = RandomItem.rnd.Next(0xd7, 0x159) + ((intHeight + intWeight) - 200);
            }
            else
            {
                num3 = RandomItem.rnd.Next(360, 520);
            }
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num9 = RandomItem.rnd.Next(0xd7, 0x159) + ((intHeight + intWeight) - 200);
            }
            else
            {
                num9 = RandomItem.rnd.Next(360, 520);
            }
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num11 = RandomItem.rnd.Next(0xd7, 0x159) + ((intHeight + intWeight) - 200);
            }
            else
            {
                num11 = RandomItem.rnd.Next(360, 520);
            }
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num = RandomItem.rnd.Next(400, 530) - ((intHeight + intWeight) - 200);
            }
            else
            {
                num = RandomItem.rnd.Next(360, 520);
            }
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num2 = RandomItem.rnd.Next(400, 530) - ((intHeight + intWeight) - 200);
            }
            else
            {
                num2 = RandomItem.rnd.Next(360, 520);
            }
            if (RandomItem.rnd.Next(0, 100) < 90)
            {
                num7 = RandomItem.rnd.Next(400, 530) - ((intHeight + intWeight) - 200);
            }
            else
            {
                num7 = RandomItem.rnd.Next(360, 520);
            }
            int num4 = RandomItem.rnd.Next(0xef, 520);
            int num5 = RandomItem.rnd.Next(0xef, 520);
            int num6 = RandomItem.rnd.Next(0xef, 520);
            int num8 = RandomItem.rnd.Next(0xef, 520);
            int num10 = RandomItem.rnd.Next(0xef, 520);
            int num12 = RandomItem.rnd.Next(0xef, 520);
            int num13 = RandomItem.rnd.Next(0xef, 520);
            int num14 = RandomItem.rnd.Next(0xef, 520);
            if ((intHeight > 210) && (RandomItem.rnd.Next(0, 100) < 80))
            {
                num -= RandomItem.rnd.Next(0, (intHeight - 210) + 1) * 10;
                num2 -= RandomItem.rnd.Next(0, (intHeight - 210) + 1) * 5;
                num7 -= RandomItem.rnd.Next(0, (intHeight - 210) + 1) * 10;
                num6 -= RandomItem.rnd.Next(0, (intHeight - 210) + 1) * 10;
                num4 -= RandomItem.rnd.Next(0, (intHeight - 210) + 1) * 5;
            }
            return new int[] { num, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14 };
        }
    }
}

