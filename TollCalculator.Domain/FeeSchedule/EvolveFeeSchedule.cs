using System;
using System.Collections.Generic;
using System.Linq;

namespace TollCalculator.Domain.FeeSchedule
{
    public class EvolveFeeSchedule : IFeeSchedule
    {
        private List<FeeInterval> FeeIntervals = new List<FeeInterval>
        {
            new FeeInterval(new TimeSpan(6, 0, 00), new TimeSpan(6, 29, 59), 8),
            new FeeInterval(new TimeSpan(6, 30, 00), new TimeSpan(6, 59, 59), 13),
            new FeeInterval(new TimeSpan(7, 00, 00), new TimeSpan(7, 59, 59), 18),
            new FeeInterval(new TimeSpan(8, 00, 00), new TimeSpan(8, 29, 59), 13),
            new FeeInterval(new TimeSpan(8, 30, 00), new TimeSpan(14, 59, 59), 8),
            new FeeInterval(new TimeSpan(15, 00, 00), new TimeSpan(15, 29, 59), 13),
            new FeeInterval(new TimeSpan(15, 30, 00), new TimeSpan(16, 59, 59), 18),
            new FeeInterval(new TimeSpan(17, 00, 00), new TimeSpan(17, 59, 59), 13),
            new FeeInterval(new TimeSpan(18, 00, 00), new TimeSpan(18, 29, 59), 13),
        };

        public int GetFeeForTime(DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;

            var feeInterval = FeeIntervals.FirstOrDefault(f => f.IsWithinInterval(date));

            return feeInterval == null ? 0 : feeInterval.Fee();
        }
    }
}
