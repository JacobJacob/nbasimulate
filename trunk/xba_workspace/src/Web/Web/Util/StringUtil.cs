namespace Web.Util
{
    using System;
    using System.Text;

    public class StringUtil
    {
        private static int[] BAD_CHAR = new int[] { 0xe779 };

        public static string CleanBadChar(string str)
        {
            if (str == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int num2 = str[i];
                bool flag = false;
                for (int j = 0; j < BAD_CHAR.Length; j++)
                {
                    if (num2 == BAD_CHAR[j])
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    builder.Append(str[i]);
                }
            }
            return builder.ToString();
        }
    }
}

