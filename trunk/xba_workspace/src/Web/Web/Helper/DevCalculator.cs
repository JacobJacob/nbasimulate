namespace Web.Helper
{
    using System;

    public class DevCalculator
    {
        public static string GetDev(string strDevCode)
        {
            if ((strDevCode == null) || (strDevCode.ToLower().Trim() == "null"))
            {
                return "无";
            }
            return string.Concat(new object[] { "", GetLevel(strDevCode), ".", GetDevIndex(strDevCode) });
        }

        public static string GetDevCode(int level, int index)
        {
            if (level == 1)
            {
                return "";
            }
            return Binary.FrontFillUp(Binary.DecToBinary(index - 1), level - 1, "0");
        }

        public static int GetDevIndex(string strDevCode)
        {
            string strBinary = strDevCode.Trim();
            if (strBinary == "")
            {
                return 1;
            }
            return (Binary.BinaryToDec(strBinary) + 1);
        }

        public static int GetLevel(string strDevCode)
        {
            return (strDevCode.Trim().Length + 1);
        }

        public static int GetPenalty(int intSalaryTop, int intSalarySum)
        {
            int num = intSalarySum - intSalaryTop;
            return (num * 3);
        }

        public static int GetSalaryTop(string strDevCode)
        {
            switch (GetLevel(strDevCode))
            {
                case 1:
                    return 0x35b60;

                case 2:
                    return 0x33450;

                case 3:
                    return 0x30d40;

                case 4:
                    return 0x2e630;

                case 5:
                    return 0x29810;

                case 6:
                    return 0x27100;

                case 7:
                    return 0x249f0;

                case 8:
                    return 0x1adb0;

                case 9:
                    return 0x17318;

                case 10:
                    return 0x11170;
            }
            return 0xc350;
        }
    }
}

