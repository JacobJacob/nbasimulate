namespace Web.Util
{
    using System;

    internal class DropOffUtil
    {
        public static string GetDropOffName(int type)
        {
            switch (type)
            {
                case 2:
                    return "双倍训练卡";

                case 3:
                    return "街球位置卡";

                case 4:
                    return "会员卡";

                case 5:
                    return "意识双倍卡";

                case 6:
                    return "街球潜力增长";

                case 7:
                    return "职业战术点";
            }
            return ("资金" + type);
        }
    }
}

