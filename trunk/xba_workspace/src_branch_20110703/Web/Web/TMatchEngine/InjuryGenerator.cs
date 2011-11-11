namespace Web.TMatchEngine
{
    using System;

    public class InjuryGenerator
    {
        private int intPower;
        public int intSuspend;
        private Random rnd = new Random(DateTime.Now.Millisecond);
        public string strEvent;

        public InjuryGenerator(int intPower)
        {
            this.intPower = intPower;
        }

        public void SetEvent()
        {
            this.SetSuspend();
            if (this.intSuspend < 4)
            {
                string[] strArray = new string[] { 
                    "轻微脑震荡", "眼部轻微擦伤", "鼻子遭到碰撞", "脖子轻微扭伤", "左肩部扭伤", "右肩部扭伤", "右臂拉伤", "左臂拉伤", "左手腕扭伤", "右手腕扭伤", "左手指挫伤", "右手指挫伤", "腰部轻微扭伤", "臀部轻微扭伤", "左膝轻微扭伤", "右膝轻微扭伤", 
                    "左腿拉伤", "右腿拉伤", "右脚踝扭伤", "左脚踝扭伤", "右脚指挫伤", "左脚指挫伤"
                 };
                int index = this.rnd.Next(0, 0x16);
                this.strEvent = strArray[index];
            }
            else if ((this.intSuspend >= 4) && (this.intSuspend < 10))
            {
                string[] strArray2 = new string[] { 
                    "中度脑震荡", "眼部被戳伤", "鼻骨受伤", "颈部扭伤", "左肩部脱臼", "右肩部脱臼", "右臂脱臼", "左臂脱臼", "左手严重扭伤", "右手严重扭伤", "左手指脱臼", "右手指脱臼", "腰部扭伤", "臀部扭伤", "左膝扭伤", "右膝扭伤", 
                    "左腿脱臼", "右腿脱臼", "右脚踝脱臼", "左脚踝脱臼", "右脚指脱臼", "左脚指脱臼"
                 };
                int num2 = this.rnd.Next(0, 0x16);
                this.strEvent = strArray2[num2];
            }
            else
            {
                string[] strArray3 = new string[] { 
                    "严重脑震荡", "眼部重伤", "鼻骨断裂", "颈部严重挫伤", "左肩骨折", "右肩骨折", "右臂骨折", "左臂骨折", "左手腕骨折", "右手腕骨折", "左手指骨折", "右手指骨折", "腰部严重扭伤", "臀部严重扭伤", "左膝严重扭伤", "右膝严重扭伤", 
                    "左腿骨折", "右腿骨折", "右脚踝骨折", "左脚踝骨折", "右脚指骨折", "左脚指骨折"
                 };
                int num3 = this.rnd.Next(0, 0x16);
                this.strEvent = strArray3[num3];
            }
        }

        public void SetSuspend()
        {
            int maxValue = (150 - this.intPower) / 10;
            if (maxValue > 3)
            {
                this.intSuspend = this.rnd.Next(3, maxValue);
            }
            else if (maxValue < 1)
            {
                this.intSuspend = 1;
            }
            else
            {
                this.intSuspend = maxValue;
            }
        }
    }
}

