using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Helper
{
	class Config
	{

        public static string GetDomain()
        {
            return IniUtil.ReadIniData("PathConfig", "Domain", "", "C:\\xba.ini");
        }

        /**
         *ÑµÁ·µã±¶Êý 
         */

        public static int GetTrainPointMultiple()
        {
            string trainPointMultiple = IniUtil.ReadIniData("GameConfig", "TrainPointMultiple", "1", "C:\\xba.ini");
            return Convert.ToInt32(trainPointMultiple);
        }

	}
}
