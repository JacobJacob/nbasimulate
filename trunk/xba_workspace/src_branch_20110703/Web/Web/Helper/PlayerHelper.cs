using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Helper
{
	class PlayerHelper
	{
        public static int GetUsePoint3(int point, int ability ,int abilityMax, int skillPotential, int age)
        {
            int i = 0;
	        int allPoint = 0;
	        if(point > 30)
            {
                point = 30;
            }
		    while(i < point)
            {
                int abilityAdd = ability;
                int ageFactor = (30-age)*100/20+100;
                if(ageFactor<70)
                {
                    ageFactor = 70;
                }
                int abilityC = abilityAdd*40/abilityMax;
                int ageC= ((400 - ageFactor )*200-5000)/400;
                int abilityLvlC = abilityAdd / 12;
                int potentialFactor = (140000-skillPotential) / 500;
                if(potentialFactor > 15)
                {
                    potentialFactor = 15;
                }
                else if(potentialFactor < 7)
                {
                    potentialFactor = 7;
                }
                allPoint = allPoint + (((abilityC*abilityLvlC+(ageC*10))*potentialFactor) / 700);
                i = i + 1;

            }

            return allPoint / 10;

         }
	}
}
