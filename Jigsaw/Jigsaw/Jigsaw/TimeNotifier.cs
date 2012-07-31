using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jigsaw
{
    class TimeNotifier
    {
        private double _notificationTime = 0;
        private bool _hasNotified = false;

        private double _defaultTimerTime = -1;

        public TimeNotifier()
        {
        }

        public TimeNotifier(double defaultTimerTime) : this()
        {
            _defaultTimerTime = defaultTimerTime;
        }

        public void NotifyMe()
        {
            NotifyMe(false);
        }

        public void NotifyMe(bool overwritePrevious)
        {
            if (_defaultTimerTime < 0)
            {
                throw new ArgumentOutOfRangeException("You must provide a default timer value in the constructor to use this method!");
            }

            NotifyMe(_defaultTimerTime, overwritePrevious);
        }

        public void NotifyMe(double howLong)
        {
            NotifyMe(howLong, false);
        }

        public void NotifyMe(double howLong, bool overwritePrevious)
        {
            if (_notificationTime == 0 || (hasTimeElapsed() && _hasNotified) || overwritePrevious)
            {
                //either:
                // no notification set yet
                // or timer has already elapsed AND we have already sent notification
                // or "overwrite" specified
                setTimer(howLong);
            }
        }

        private void setTimer(double howLong)
        {
            _notificationTime = Core.TotalTime + howLong;
            _hasNotified = false;
        }

        public bool Notify
        {
            get
            {
                if (hasTimeElapsed() && !_hasNotified)
                {
                    _hasNotified = true;
                    return true;
                }

                return false;
            }
        }

        //public double TimerFraction
        //{
        //    get
        //    {
        //        if (hasTimeElapsed())
        //        {
        //            return 1;
        //        }

        //        if (_notificationTime == 0)
        //        {
        //            return 0;
        //        }

                
        //    }
        //}

        private bool hasTimeElapsed()
        {
            if (_notificationTime > 0 && _notificationTime <= Core.TotalTime)
            {
                return true;
            }

            return false;
        }
    }
}