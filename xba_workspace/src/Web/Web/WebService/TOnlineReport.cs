﻿namespace Web.WebService
{
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
        private IContainer components;

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
            int num2 = 0x11;
            DateTime time = DateTime.Today.AddHours((double) num2).AddSeconds((double) (num * 480));
            if (DateTime.Now >= time)
            {
                base.Session["RepSession"] = null;
                DataRow oneStarMatchByID = BTPStarMatchManager.GetOneStarMatchByID(intTag);
                int num3 = (int) oneStarMatchByID["ScoreA"];
                int num4 = (int) oneStarMatchByID["ScoreB"];
                return string.Concat(new object[] { "全场比赛结束！<br>|", num3, ":", num4, "|END" });
            }
            if (base.Session["RepSession"] == null)
            {
                DataRow row2 = BTPStarMatchManager.GetOneStarMatchByID(intTag);
                if (row2 == null)
                {
                    quarterArrange = "暂无战报，请等待！|0:0|END";
                }
                else
                {
                    string url = row2["RepURL"].ToString();
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
                if ((now >= data.datStd) && (now >= data.datPre.AddSeconds((double) (data.intS * 5))))
                {
                    int num6 = 0;
                    TimeSpan span2 = (TimeSpan) (now - data.datStd);
                    int num7 = Convert.ToInt32((double) (span2.TotalSeconds / ((double) data.intS)));
                    int num8 = 1;
                    int length = 0;
                    for (int i = 0; i < data.ds.Tables["Quarter"].Rows.Count; i++)
                    {
                        DataRow row4 = data.ds.Tables["Quarter"].Rows[i];
                        num8 = (int) row4["QuarterID"];
                        length = data.ds.Tables["Script"].Select("QuarterID=" + num8).Length;
                        num7 -= length;
                        if (num7 < 0)
                        {
                            data.intQNum = num8;
                            num6 = length + num7;
                            break;
                        }
                    }
                    if (num7 >= 0)
                    {
                        data.intQNum = num8;
                        num6 = length;
                    }
                    string str7 = "";
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
                            DataRow row5;
                            if ((j > 0) && (j != (num6 - 1)))
                            {
                                row5 = XmlHelper.GetRow(data.ds.Tables["Script"], string.Concat(new object[] { "QuarterID=", data.intQNum, " AND ScriptID=", j }), "");
                                quarterArrange = quarterArrange + XmlHelper.GetOneScript(row5);
                                if (row5["Score"].ToString() != "")
                                {
                                    str7 = row5["Score"].ToString();
                                }
                            }
                            else
                            {
                                row5 = XmlHelper.GetRow(data.ds.Tables["Script"], string.Concat(new object[] { "QuarterID=", data.intQNum, " AND ScriptID=", j }), "");
                                quarterArrange = quarterArrange + XmlHelper.GetOneScript(row5);
                                data.intScriptID = j + 1;
                                if (row5["Score"].ToString() != "")
                                {
                                    str7 = row5["Score"].ToString();
                                }
                                quarterArrange = quarterArrange + "|" + str7 + "|DOING";
                            }
                        }
                    }
                    data.datPre = now;
                    return quarterArrange;
                }
                if ((now < data.datPre.AddSeconds((double) data.intS)) || (now < data.datStd))
                {
                    return quarterArrange;
                }
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
                            string str3 = XmlHelper.GetRow(data.ds.Tables["Club"], "Type=1", "")["Score"].ToString();
                            string str4 = XmlHelper.GetRow(data.ds.Tables["Club"], "Type=2", "")["Score"].ToString();
                            quarterArrange = "全场比赛结束！<br>";
                            string str5 = quarterArrange;
                            quarterArrange = str5 + "|" + str3 + ":" + str4 + "|END";
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
                        string str6 = "";
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
                                    str6 = row["Score"].ToString();
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
                                    str6 = row["Score"].ToString();
                                }
                            }
                        }
                        quarterArrange = quarterArrange + "|" + str6 + "|DOING";
                    }
                }
                data.datPre = now;
            }
            return quarterArrange;
        }
    }
}

