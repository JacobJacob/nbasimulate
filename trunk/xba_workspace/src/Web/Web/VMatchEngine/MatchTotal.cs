namespace Web.VMatchEngine
{
    using System;
    using System.Collections.Generic;
    using Web.Lang;
    using Web.Util;

    public class MatchTotal
    {
        private MyDictionary<string, ASObject> arrangeInfo = new MyDictionary<string, ASObject>();
        private MyDictionary<string, List<ASObject>> awayAblityMap = new MyDictionary<string, List<ASObject>>();
        private List<ASObject> awayActionInfoList = new List<ASObject>();
        private float awaySuccessTotal;
        private float awayTotalRate;
        private MyDictionary<string, List<ASObject>> homeAblityMap = new MyDictionary<string, List<ASObject>>();
        private List<ASObject> homeActionInfoList = new List<ASObject>();
        private float homeSuccessTotal;
        private float homeTotalRate;
        private int matchId;
        private ASObject property = new ASObject();
        private int type;

        public MatchTotal(int type, int matchId)
        {
            this.type = type;
            this.matchId = matchId;
        }

        public int Check(bool isHome)
        {
            return 0;
        }

        public void Save()
        {
            try
            {
                ASObject obj2 = new ASObject();
                obj2["property"] = this.property;
                obj2["homeAblityMap"] = this.homeAblityMap;
                obj2["awayAblityMap"] = this.awayAblityMap;
                obj2["arrangeInfo"] = this.arrangeInfo;
                obj2["homeActionInfoList"] = this.homeActionInfoList;
                obj2["awayActionInfoList"] = this.awayActionInfoList;
                string msg = Json.Dump(obj2);
                Web.VMatchEngine.Logger.WriteRawLog(Web.VMatchEngine.Logger.GetTotalLogPath(this.type, this.matchId), msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void SetAbility(bool isHome, int quarter, ASObject aso)
        {
            string key = Convert.ToString(quarter);
            MyDictionary<string, List<ASObject>> homeAblityMap = null;
            if (isHome)
            {
                homeAblityMap = this.homeAblityMap;
            }
            else
            {
                homeAblityMap = this.awayAblityMap;
            }
            List<ASObject> list = null;
            bool flag = false;
            if (homeAblityMap.ContainsKey(key))
            {
                list = homeAblityMap[key];
            }
            else
            {
                flag = true;
                list = new List<ASObject>();
            }
            list.Add(aso);
            if (flag)
            {
                homeAblityMap[key] = list;
            }
        }

        public void SetActionTotal(bool isHome, string name, int offCheckValue, int defCheckValue, int teamValueCheckRnd, int currentOffCheckValue, int playerValueCheckRnd)
        {
            ASObject item = new ASObject();
            item["name"] = name;
            item["teamValueCheckRnd"] = teamValueCheckRnd;
            item["offCheckValue"] = offCheckValue;
            item["currentOffCheckValue"] = currentOffCheckValue;
            item["playerValueCheckRnd"] = playerValueCheckRnd;
            item["defCheckValue"] = defCheckValue;
            float num = ((float) offCheckValue) / ((float) (offCheckValue + defCheckValue));
            float num2 = ((float) currentOffCheckValue) / ((float) (currentOffCheckValue + defCheckValue));
            float num3 = num * num2;
            if ((offCheckValue > teamValueCheckRnd) && (currentOffCheckValue > playerValueCheckRnd))
            {
                if (isHome)
                {
                    this.homeSuccessTotal++;
                }
                else
                {
                    this.awaySuccessTotal++;
                }
            }
            if (isHome)
            {
                this.homeActionInfoList.Add(item);
                this.homeTotalRate += num3;
            }
            else
            {
                this.awayActionInfoList.Add(item);
                this.awayTotalRate += num3;
            }
        }

        public void SetArrangeInfo(int quarter, ASObject aso)
        {
            string key = Convert.ToString(quarter);
            if (!this.arrangeInfo.ContainsKey(key))
            {
                this.arrangeInfo[key] = aso;
            }
        }

        public void SetProperty(string attr, object value)
        {
            this.property[attr] = value;
        }
    }
}

