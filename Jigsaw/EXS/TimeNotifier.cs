using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EXS
{
    public class TimeNotifier
    {
        private double _notificationTime = 0;
        private bool _hasNotified = false;

        private double _defaultTimerTime = -1;

        private double _currentTimerLength = 0;

        public TimeNotifier()
        {
        }

        public TimeNotifier(double defaultTimerTime)
            : this()
        {
            _defaultTimerTime = defaultTimerTime;
        }

        public bool NotifyMe()
        {
            return NotifyMe(false);
        }

        public bool NotifyMe(bool overwritePrevious)
        {
            if (_defaultTimerTime < 0)
            {
                throw new ArgumentOutOfRangeException("You must provide a default timer value in the constructor to use this method!");
            }

            return NotifyMe(_defaultTimerTime, overwritePrevious);
        }

        public bool NotifyMe(double howLong)
        {
            return NotifyMe(howLong, false);
        }

        public bool NotifyMe(double howLong, bool overwritePrevious)
        {
            if (_notificationTime == 0 || (hasTimeElapsed() && _hasNotified) || overwritePrevious)
            {
                //either:
                // no notification set yet
                // or timer has already elapsed AND we have already sent notification
                // or "overwrite" specified
                setTimer(howLong);
                return true;
            }

            return false;
        }

        private void setTimer(double howLong)
        {
            if (howLong <= 0)
            {
                throw new ArgumentOutOfRangeException("Timer must have a positive, non-zero length.");
            }

            _notificationTime = Core.TotalTime + howLong;
            _currentTimerLength = howLong;
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

        public double TimerFraction
        {
            get
            {
                if (hasTimeElapsed())
                {
                    return 1;
                }

                if (_notificationTime == 0)
                {
                    return 0;
                }

                return (1.0 - ((_notificationTime - Core.TotalTime) / _currentTimerLength));
            }
        }

        private bool hasTimeElapsed()
        {
            if (_notificationTime > 0 && _notificationTime <= Core.TotalTime)
            {
                return true;
            }

            return false;
        }

        public bool StillGoing
        {
            get
            {
                if (_notificationTime > 0 && _notificationTime > Core.TotalTime)
                {
                    return true;
                }

                return false;
            }
        }

        public bool Initialized
        {
            get
            {
                return _notificationTime > 0;
            }
        }
    }
}