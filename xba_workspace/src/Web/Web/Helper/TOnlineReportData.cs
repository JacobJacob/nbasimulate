﻿namespace Web.Helper
{
    using System;
    using System.Data;

    [Serializable]
    public class TOnlineReportData
    {
        public DateTime datPre;
        public DateTime datStd;
        public DataSet ds;
        public int intQNum;
        public int intS = ((byte) Global.drParameter["RepRefreshSec"]);
        public int intScriptID;
        public int intStatus;
        public int intTotalQuarter;

        public TOnlineReportData(DataSet ds)
        {
            int num = 9;
            this.datStd = DateTime.Today.AddHours((double) num);
            this.datPre = DateTime.Parse("2005-1-1");
            this.intQNum = 1;
            this.intScriptID = 0;
            this.ds = ds;
            this.intTotalQuarter = ds.Tables["Quarter"].Rows.Count;
            this.intStatus = 0;
        }
    }
}

