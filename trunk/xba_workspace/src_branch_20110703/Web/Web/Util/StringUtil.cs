using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Util
{
    public class StringUtil
    {

        private static int[] BAD_CHAR = { 0xe779 };

        public static string CleanBadChar(string str)
        {
            if (str == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int c = (int)str[i];
                bool boolBadChar = false;
                for (int j = 0; j < BAD_CHAR.Length; j++)
                {
                    if (c == BAD_CHAR[j])
                    {
                        boolBadChar = true;
                        break;
                    }
                }

                if (!boolBadChar)
                {
                    sb.Append(str[i]);
                }

            }

            return sb.ToString();
        }
    }

}
