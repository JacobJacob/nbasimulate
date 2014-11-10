namespace Web.VMatchEngine
{
    using System;
    using Web.Util;

    internal class MatchHelper
    {
        public static bool AbilityShotCheck(int attackAbility, int defenseAbility, int attackArrangeAdd, int attackWinPoint)
        {
            int num = 0;
            if (attackAbility <= defenseAbility)
            {
                int num2 = defenseAbility - attackAbility;
                if (num2 > 5)
                {
                    if ((attackWinPoint < 0) && ((attackArrangeAdd == 0x5e) || (attackArrangeAdd == 0x61)))
                    {
                        num = 3;
                    }
                }
                else if (attackWinPoint < 0)
                {
                    if (attackArrangeAdd == 0x5e)
                    {
                        num = 5;
                    }
                    else if (attackArrangeAdd == 0x61)
                    {
                        num = 4;
                    }
                }
            }
            else
            {
                int num3 = attackAbility - defenseAbility;
                if (num3 > 5)
                {
                    if (attackWinPoint < 0)
                    {
                        if (attackArrangeAdd == 0x5e)
                        {
                            num = 15;
                        }
                        else if (attackArrangeAdd == 0x61)
                        {
                            num = 12;
                        }
                    }
                }
                else if (attackWinPoint < 0)
                {
                    if (attackArrangeAdd == 0x5e)
                    {
                        num = 8;
                    }
                    else if (attackArrangeAdd == 0x61)
                    {
                        num = 7;
                    }
                }
            }
            int num4 = new Random(Guid.NewGuid().GetHashCode()).Next(100);
            if (attackWinPoint >= 10)
            {
                num = 0;
            }
            Web.Util.Logger.Debug(string.Concat(new object[] { "attackAbility[", attackAbility, "], defenseAbility[", defenseAbility, "], attackArrangeAdd[", attackArrangeAdd, "], check[", num, "], rnd[", num4, "]" }));
            return false;
        }
    }
}

