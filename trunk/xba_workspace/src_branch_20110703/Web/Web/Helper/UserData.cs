namespace Web.Helper
{
    using System;

    public class UserData
    {
        public string strUserName;

        public UserData(string strUserName)
        {
            this.strUserName = strUserName;
        }

        public static string GetUserLevel(int intWealth)
        {
            if (intWealth < 30)
            {
                return "小小球迷";
            }
            if ((intWealth >= 30) && (intWealth < 70))
            {
                return "篮球新人";
            }
            if ((intWealth >= 70) && (intWealth < 200))
            {
                return "街球自由人";
            }
            if ((intWealth >= 200) && (intWealth < 400))
            {
                return "街球新人";
            }
            if ((intWealth >= 400) && (intWealth < 700))
            {
                return "街球替补";
            }
            if ((intWealth >= 700) && (intWealth < 0x3e8))
            {
                return "街球超级替补";
            }
            if ((intWealth >= 0x3e8) && (intWealth < 0x5dc))
            {
                return "街球主力";
            }
            if ((intWealth >= 0x5dc) && (intWealth < 0x7d0))
            {
                return "街球杯赛冠军";
            }
            if ((intWealth >= 0x7d0) && (intWealth < 0x9c4))
            {
                return "街球明星";
            }
            if ((intWealth >= 0x9c4) && (intWealth < 0xbb8))
            {
                return "职业选秀";
            }
            if ((intWealth >= 0xbb8) && (intWealth < 0xdac))
            {
                return "联赛新人";
            }
            if ((intWealth >= 0xdac) && (intWealth < 0xfa0))
            {
                return "联赛替补";
            }
            if ((intWealth >= 0xfa0) && (intWealth < 0x1388))
            {
                return "联赛第六人";
            }
            if ((intWealth >= 0x1388) && (intWealth < 0x1b58))
            {
                return "联赛主力";
            }
            if ((intWealth >= 0x1b58) && (intWealth < 0x2710))
            {
                return "球队核心";
            }
            if ((intWealth >= 0x2710) && (intWealth < 0x3a98))
            {
                return "联赛MVP";
            }
            if ((intWealth >= 0x3a98) && (intWealth < 0x4e20))
            {
                return "联赛冠军";
            }
            if ((intWealth >= 0x4e20) && (intWealth < 0x7530))
            {
                return "荣誉球员";
            }
            return "超级巨星";
        }
    }
}

