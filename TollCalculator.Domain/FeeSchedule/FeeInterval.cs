using System;

namespace TollCalculator.Domain.FeeSchedule
{
    public class FeeInterval
    {
        private readonly TimeSpan _startTime;
        private readonly TimeSpan _endTime;
        private readonly int _fee;

        public FeeInterval(TimeSpan startTime, TimeSpan endTime, int fee)
        {
            _startTime = startTime;
            _endTime = endTime;
            _fee = fee;
        }

        public bool Includes(DateTime date)
        {
            return _startTime <= date.TimeOfDay && _endTime >= date.TimeOfDay;
        }

        public int Fee()
        {
            return _fee;
        }
    }
}
