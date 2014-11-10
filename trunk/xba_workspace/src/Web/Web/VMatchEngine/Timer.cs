namespace Web.VMatchEngine
{
    using System;

    public class Timer
    {
        private int intMinute;
        private int intSecond;
        private Random rnd = new Random(DateTime.Now.Millisecond);

        public Timer(int intMinute)
        {
            this.intMinute = intMinute;
        }

        private int GetFastLength()
        {
            return (4 + this.rnd.Next(0, 6));
        }

        private int GetNormalLength(bool blnIsNextOff)
        {
            if (blnIsNextOff)
            {
                return ((6 + this.rnd.Next(0, 10)) + this.rnd.Next(0, 3));
            }
            return (((9 + this.rnd.Next(0, 10)) + this.rnd.Next(0, 6)) + this.rnd.Next(0, 2));
        }

        public string GetTime()
        {
            string str;
            string str2;
            if (this.intMinute < 10)
            {
                str = "0" + this.intMinute.ToString();
            }
            else
            {
                str = this.intMinute.ToString();
            }
            if (this.intSecond < 10)
            {
                str2 = "0" + this.intSecond.ToString();
            }
            else
            {
                str2 = this.intSecond.ToString();
            }
            if (this.intMinute < 0)
            {
                return "00:00";
            }
            return (str + ":" + str2);
        }

        public bool IsFinished()
        {
            return ((this.intMinute == 0) && (this.intSecond == 0));
        }

        public void Next(bool blnFast, bool blnIsNextOff)
        {
            int fastLength;
            if (blnFast)
            {
                fastLength = this.GetFastLength();
            }
            else
            {
                fastLength = this.GetNormalLength(blnIsNextOff);
            }
            this.intSecond -= fastLength;
            if (this.intSecond < 0)
            {
                this.intMinute--;
                this.intSecond += 60;
            }
            if (this.intMinute < 0)
            {
                this.intMinute = 0;
                this.intSecond = 0;
            }
        }

        public void SetFinished()
        {
            this.intMinute = 0;
            this.intSecond = 0;
        }
    }
}

