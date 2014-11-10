namespace Web.Util
{
    using System;

    internal class PageUtil
    {
        public static string GetViewPage(string url, int page, int pagesize, int total)
        {
            string[] strArray;
            object obj2;
            int num = (total / pagesize) + 1;
            if ((total % pagesize) == 0)
            {
                num--;
            }
            if (num == 0)
            {
                num = 1;
            }
            string str = "";
            if (page == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", url, "Page=", (page - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (page == num)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", url, "Page=", (page + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == page)
                {
                    str3 = str3 + " selected";
                }
                obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            obj2 = str + " " + str2 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str3 });
        }
    }
}

