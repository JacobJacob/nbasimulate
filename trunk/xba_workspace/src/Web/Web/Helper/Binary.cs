namespace Web.Helper
{
    using System;

    public class Binary
    {
        public static int BinaryToDec(string strBinary)
        {
            char[] chArray = strBinary.ToCharArray();
            int num = 0;
            int num2 = 1;
            for (int i = chArray.Length - 1; i >= 0; i--)
            {
                if (chArray[i] == '1')
                {
                    num += num2;
                }
                num2 *= 2;
            }
            return num;
        }

        public static string DecToBinary(int dec)
        {
            if (dec == 0)
            {
                return "0";
            }
            string str = "";
            while (dec > 1)
            {
                int num = dec / 2;
                str = (dec % 2) + str;
                dec = num;
            }
            return (1 + str);
        }

        public static string FrontFillUp(string code, int length, string c)
        {
            int num = code.Length;
            int num2 = length - num;
            for (int i = 0; i < num2; i++)
            {
                code = c + code;
            }
            return code;
        }

        public static int GetMax(int number)
        {
            int num = 1;
            while (num < number)
            {
                num *= 2;
                if (num >= number)
                {
                    return num;
                }
            }
            return -1;
        }

        public static int GetMin(int number)
        {
            number /= 2;
            return GetMax(number);
        }

        public static string ToReverse(string s)
        {
            string str = "";
            for (int i = s.Length; i > 0; i--)
            {
                str = str + s.Substring(i - 1, 1);
            }
            return str;
        }
    }
}

