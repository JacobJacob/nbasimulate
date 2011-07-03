namespace Web.Helper
{
    using System;

    public class UpdateTimer
    {
        private DateTime datUpdate;
        private int month;
        private TimeSpan span;

        public UpdateTimer(DateTime datUpdate, int month)
        {
            this.month = 0;
            this.datUpdate = datUpdate;
            this.month = month;
        }

        public UpdateTimer(DateTime datUpdate, TimeSpan span)
        {
            this.month = 0;
            this.datUpdate = datUpdate;
            this.span = span;
        }

        public bool IsUpdateTime()
        {
            if (this.datUpdate <= DateTime.Now)
            {
                this.NextTimer();
                return true;
            }
            return false;
        }

        private void NextTimer()
        {
            if (this.month != 0)
            {
                this.datUpdate = this.datUpdate.AddMonths(this.month);
            }
            else
            {
                this.datUpdate = this.datUpdate.Add(this.span);
            }
        }

        public void SetDayTime()
        {
            this.datUpdate = DateTime.Now;
        }
    }
}

