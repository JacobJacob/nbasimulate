namespace Web.TMatchEngine
{
    using System;

    public class ScriptGenerator
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);

        public static string GetBadOff(int intOffMethod, Player pOCP, Player pACP, Player pDCP, bool blnAssist)
        {
            if ((intOffMethod == 1) || (intOffMethod == 2))
            {
                return Language.BadOffenseInside(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if ((intOffMethod == 3) || (intOffMethod == 4))
            {
                return Language.BadOffenseShot(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if (intOffMethod == 5)
            {
                return Language.BadOffenseThree(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if ((intOffMethod == 6) || (intOffMethod == 8))
            {
                return Language.BadOffenseBreak(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if (intOffMethod == 7)
            {
                return Language.BadOffensePick(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if (intOffMethod == 9)
            {
                return Language.BadOffenseFast(pOCP.strName, pACP.strName, pDCP.strName);
            }
            return "警告信息";
        }

        public static string GetBlock(int intOffMethod, Player pACP, Player pOCP, Player pDCP)
        {
            if ((intOffMethod == 1) || (intOffMethod == 2))
            {
                return Language.BadOffenseInsideBlock(pOCP.strName, pDCP.strName);
            }
            if ((intOffMethod == 3) || (intOffMethod == 4))
            {
                return Language.BadOffenseShotBlock(pOCP.strName, pDCP.strName);
            }
            if (intOffMethod == 5)
            {
                return Language.BadOffenseThreeBlock(pOCP.strName, pDCP.strName);
            }
            if (((intOffMethod == 6) || (intOffMethod == 8)) || (intOffMethod == 9))
            {
                return Language.BadOffenseBreakBlock(pOCP.strName, pDCP.strName);
            }
            if (intOffMethod == 7)
            {
                return Language.BadOffenseBreakBlock(pOCP.strName, pDCP.strName);
            }
            return "警告信息";
        }

        public static string GetDefReb(Player pDRP)
        {
            return Language.DefRebound(pDRP.strName);
        }

        public static string GetGoodOff(int intOffMethod, Player pOCP, Player pACP, Player pDCP, bool blnAssist)
        {
            int num = (pOCP.intDunk / 10) - 250;
            if (blnAssist)
            {
                if (intOffMethod == 5)
                {
                    return Language.GoodOffenseThreeWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                if ((intOffMethod == 2) || (rnd.Next(0, 100) < 70))
                {
                    if ((num < 0) || (rnd.Next(0, 100) < 70))
                    {
                        return Language.GoodOffInsideWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    if (rnd.Next(0, 70) < num)
                    {
                        return Language.GoodOffenseInsideGoodDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    return Language.GoodOffenseInsideDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                if (intOffMethod == 4)
                {
                    return Language.GoodOffShotWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                if (intOffMethod == 7)
                {
                    if ((num < 0) || (rnd.Next(0, 100) < 70))
                    {
                        return Language.GoodOffensePickWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    if (rnd.Next(0, 70) < num)
                    {
                        return Language.GoodOffensePickDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    return Language.GoodOffensePickGoodDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                if (intOffMethod == 8)
                {
                    if ((num < 0) || (rnd.Next(0, 100) < 70))
                    {
                        return Language.GoodOffenseBreakWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    if (rnd.Next(0, 70) < num)
                    {
                        return Language.GoodOffenseBreakDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                    }
                    return Language.GoodOffenseBreakGoodDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                if (intOffMethod != 9)
                {
                    return "错误警告！";
                }
                if ((num >= 0) && (rnd.Next(0, 100) >= 70))
                {
                    return Language.GoodOffenseFastDunkWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
                }
                return Language.GoodOffenseFastWithAssist(pOCP.strName, pACP.strName, pDCP.strName);
            }
            if (intOffMethod == 5)
            {
                return Language.GoodOffThree(pOCP.strName, pDCP.strName);
            }
            if ((intOffMethod == 1) || (rnd.Next(0, 100) < 70))
            {
                if (num < 0)
                {
                    return Language.GoodOffenseInside(pOCP.strName, pDCP.strName);
                }
                if (rnd.Next(0, 70) < num)
                {
                    return Language.GoodOffenseInsideDunk(pOCP.strName, pDCP.strName);
                }
                return Language.GoodOffenseInsideGoodDunk(pOCP.strName, pDCP.strName);
            }
            if (intOffMethod == 3)
            {
                return Language.GoodOffShot(pOCP.strName, pDCP.strName);
            }
            if ((intOffMethod == 6) || (rnd.Next(0, 100) < 70))
            {
                if (num < 0)
                {
                    return Language.GoodOffenseBreak(pOCP.strName, pDCP.strName);
                }
                if (rnd.Next(0, 70) < num)
                {
                    return Language.GoodOffenseBreakDunk(pOCP.strName, pDCP.strName);
                }
                return Language.GoodOffenseBreakGoodDunk(pOCP.strName, pDCP.strName);
            }
            if ((intOffMethod == 7) || (rnd.Next(0, 100) < 70))
            {
                if (num < 0)
                {
                    return Language.GoodOffensePick(pOCP.strName, pDCP.strName);
                }
                if (rnd.Next(0, 70) < num)
                {
                    return Language.GoodOffensePickDunk(pOCP.strName, pDCP.strName);
                }
                return Language.GoodOffensePickGoodDunk(pOCP.strName, pDCP.strName);
            }
            if (intOffMethod != 9)
            {
                return "错误警告！";
            }
            if ((num >= 0) && (rnd.Next(0, 100) >= 70))
            {
                return Language.GoodOffenseFastDunk(pOCP.strName, pDCP.strName);
            }
            return Language.GoodOffenseFast(pOCP.strName, pDCP.strName);
        }

        public static string GetNormalFoul(Player pOCP, Player pDCP)
        {
            return Language.DefFoul(pOCP.strName, pDCP.strName);
        }

        public static string GetOffFoul(int intOffMethod, Player pOCP, Player pDCP)
        {
            return (pDCP.strName + "造成" + pOCP.strName + "的进攻犯规。");
        }

        public static string GetOffReb(Player pORP)
        {
            return Language.OffRebound(pORP.strName);
        }

        public static string GetSteal(Player pOCP, Player pDCP)
        {
            return Language.Steal(pOCP.strName, pDCP.strName);
        }

        public static string GetTo(Player pOCP, Player pDCP)
        {
            return Language.TO(pOCP.strName, pDCP.strName);
        }
    }
}

