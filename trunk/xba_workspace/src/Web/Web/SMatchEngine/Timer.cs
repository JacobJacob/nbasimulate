namespace Web.SMatchEngine
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

        private int GetNormalLength()
        {
            return (((6 + this.rnd.Next(0, 5)) + this.rnd.Next(0, 6)) + this.rnd.Next(0, 7));
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

        public void Next()
        {
            int normalLength = this.GetNormalLength();
            this.intSecond -= normalLength;
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

