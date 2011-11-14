namespace Web.WebService
{
    using LoginParameter;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.Services;
    using System.Xml;
    using Web;
    using Web.DBData;
    using Web.Helper;

    public class TOnlineReport : WebService
    {
        private IContainer components = null;

        public TOnlineReport()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private void InitializeComponent()
        {
        }

        [WebMethod(EnableSession=true)]
        public string TOnlineReportDetail(int intTag)
        {
            TOnlineReportData data;
            string quarterArrange = "";
            int num = (byte) Global.drParameter["RepRefreshSec"];
            int num2 = 17;//(byte) Global.drParameter["RepStartTime"];
            DateTime time2 = DateTime.Today.AddHours((double) num2).AddSeconds((double) (num * 480));
            if (DateTime.Now >= time2)
            {
                base.Session["RepSession"] = null;
                //DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(intTag);

                DataRow starMathcRow = BTPStarMatchManager.GetOneStarMatchByID(intTag);

                int intClubAScore = (int)starMathcRow["ScoreA"];
                int intClubBScore = (int)starMathcRow["ScoreB"];
                return string.Concat(new object[] { "全场比赛结束！<br>|", intClubAScore, ":", intClubBScore, "|END" });
            }
            if (base.Session["RepSession"] == null)
            {
                DataRow starMathcRow = BTPStarMatchManager.GetOneStarMatchByID(intTag);

                if (starMathcRow == null)
                {
                    quarterArrange = "暂无战报，请等待！|0:0|END";
                }
                else
                {
                    string url = starMathcRow["RepURL"].ToString();
                    if ((url.ToLower() == "null") || (url == ""))
                    {
                        quarterArrange = "暂无战报，请等待！|0:0|END";
                    }
                    else
                    {
                        url = Config.GetDomain() + url;
                        XmlDataDocument document = new XmlDataDocument();
                        document.DataSet.ReadXmlSchema(base.Server.MapPath("../MatchXML/RepSchema.xsd"));
                        XmlTextReader reader = new XmlTextReader(url);
                        reader.MoveToContent();
                        document.Load(reader);
                        data = new TOnlineReportData(document.DataSet);
                        base.Session["RepSession"] = data;
                    }
                }
            }
            if (quarterArrange == "")
            {
                data = (TOnlineReportData) base.Session["RepSession"];
                DateTime now = DateTime.Now;
                if (now < data.datStd)
                {
                    TimeSpan span = (TimeSpan) (data.datStd - now);
                    if (span.TotalSeconds < 1.0)
                    {
                        return "|PREPARE|PREPARE";
                    }
                    return string.Concat(new object[] { "据比赛开始时间还有：", span.Hours, ":", span.Minutes, ":", span.Seconds, "|PREPARE|PREPARE" });
                }
                if ((now < data.datStd) || (now < data.datPre.AddSeconds((double) (data.intS * 5))))
                {
                    if ((now >= data.datPre.AddSeconds((double) data.intS)) && (now >= data.datStd))
                    {
                        if (data.intScriptID == 0)
                        {
                            quarterArrange = XmlHelper.GetQuarterArrange(data);
                            data.intScriptID++;
                            quarterArrange = quarterArrange + "|START|DOING";
                        }
                        else
                        {
                            DataView view = XmlHelper.GetView(data.ds.Tables["Script"], string.Concat(new object[] { "QuarterID=", data.intQNum, " AND ScriptID>=", data.intScriptID }), "");
                            if (view.Count <= 0)
                            {
                                if (data.intQNum >= data.intTotalQuarter)
                                {
                                    string str5 = XmlHelper.GetRow(data.ds.Tables["Club"], "Type=1", "")["Score"].ToString();
                                    string str6 = XmlHelper.GetRow(data.ds.Tables["Club"], "Type=2", "")["Score"].ToString();
                                    quarterArrange = "全场比赛结束！<br>";
                                    string str7 = quarterArrange;
                                    quarterArrange = str7 + "|" + str5 + ":" + str6 + "|END";
                                    data.datPre = data.datPre.AddSeconds((double) (data.intS * 10));
                                    base.Session["RepSession"] = null;
                                }
                                else
                                {
                                    quarterArrange = "本节比赛结束！<br>";
                                    quarterArrange = quarterArrange + "|QUARTEREND|DOING";
                                    data.datPre = data.datPre.AddSeconds((double) (data.intS * 3));
                                    data.intQNum++;
                                    data.intScriptID = 0;
                                }
                            }
                            else
                            {
                                bool flag = false;
                                string str4 = "";
                                quarterArrange = "";
                                for (int k = 0; k < view.Count; k++)
                                {
                                    DataRow row;
                                    if (view[k].Row["Score"].ToString() != "")
                                    {
                                        if (flag)
                                        {
                                            break;
                                        }
                                        row = view[k].Row;
                                        quarterArrange = quarterArrange + XmlHelper.GetOneScript(row);
                                        data.intScriptID = ((int) row["ScriptID"]) + 1;
                                        if (row["Score"].ToString() != "")
                                        {
                                            str4 = row["Score"].ToString();
                                        }
                                        flag = true;
                                    }
                                    else
                                    {
                                        row = view[k].Row;
                                        quarterArrange = quarterArrange + XmlHelper.GetOneScript(row);
                                        data.intScriptID = ((int) row["ScriptID"]) + 1;
                                        if (row["Score"].ToString() != "")
                                        {
                                            str4 = row["Score"].ToString();
                                        }
                                    }
                                }
                                quarterArrange = quarterArrange + "|" + str4 + "|DOING";
                            }
                        }
                        data.datPre = now;
                    }
                    return quarterArrange;
                }
                int num6 = 0;
                TimeSpan span2 = (TimeSpan) (now - data.datStd);
                int num5 = Convert.ToInt32((double) (span2.TotalSeconds / ((double) data.intS)));
                int num7 = 1;
                int length = 0;
                for (int i = 0; i < data.ds.Tables["Quarter"].Rows.Count; i++)
                {
                    DataRow row3 = data.ds.Tables["Quarter"].Rows[i];
                    num7 = (int) row3["QuarterID"];
                    length = data.ds.Tables["Script"].Select("QuarterID=" + num7).Length;
                    num5 -= length;
                    if (num5 < 0)
                    {
                        data.intQNum = num7;
                        num6 = length + num5;
                        break;
                    }
                }
                if (num5 >= 0)
                {
                    data.intQNum = num7;
                    num6 = length;
                }
                string str3 = "";
                for (int j = 0; j < num6; j++)
                {
                    if (j == 0)
                    {
                        quarterArrange = XmlHelper.GetQuarterArrange(data);
                        data.intScriptID++;
                        if (num6 == 0)
                        {
                            quarterArrange = quarterArrange + "|0:0|DOING";
                        }
                    }
                    else
                    {
                        DataRow row4;
                        if ((j > 0) && (j != (num6 - 1)))
                        {
                            row4 = XmlHelper.GetRow(data.ds.Tables["Script"], string.Concat(new object[] { "QuarterID=", data.intQNum, " AND ScriptID=", j }), "");
                            quarterArrange = quarterArrange + XmlHelper.GetOneScript(row4);
                            if (row4["Score"].ToString() != "")
                            {
                                str3 = row4["Score"].ToString();
                            }
                        }
                        else
                        {
                            row4 = XmlHelper.GetRow(data.ds.Tables["Script"], string.Concat(new object[] { "QuarterID=", data.intQNum, " AND ScriptID=", j }), "");
                            quarterArrange = quarterArrange + XmlHelper.GetOneScript(row4);
                            data.intScriptID = j + 1;
                            if (row4["Score"].ToString() != "")
                            {
                                str3 = row4["Score"].ToString();
                            }
                            quarterArrange = quarterArrange + "|" + str3 + "|DOING";
                        }
                    }
                }
                data.datPre = now;
            }
            return quarterArrange;
        }
    }
}

