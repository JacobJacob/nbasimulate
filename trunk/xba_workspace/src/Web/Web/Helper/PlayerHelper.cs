namespace Web.Helper
{
    using System;

    internal class PlayerHelper
    {
        public static int GetUsePoint3(int point, int ability, int abilityMax, int skillPotential, int age)
        {
            int num = 0;
            int num2 = 0;
            if (point > 30)
            {
                point = 30;
            }
            while (num < point)
            {
                int num3 = ability;
                int num4 = (((30 - age) * 100) / 20) + 100;
                if (num4 < 70)
                {
                    num4 = 70;
                }
                int num5 = (num3 * 40) / abilityMax;
                int num6 = (((400 - num4) * 200) - 0x1388) / 400;
                int num7 = num3 / 12;
                int num8 = (0x222e0 - skillPotential) / 500;
                if (num8 > 15)
                {
                    num8 = 15;
                }
                else if (num8 < 7)
                {
                    num8 = 7;
                }
                num2 += (((num5 * num7) + (num6 * 10)) * num8) / 700;
                num++;
            }
            return (num2 / 10);
        }
    }
}

