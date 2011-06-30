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

	}
}
