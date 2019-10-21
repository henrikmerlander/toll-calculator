using System;
using TollCalculator.Domain.FeeSchedule;

namespace TollCalculator.Domain.Tests.Dummies
{
    public partial class MultiplePassesTests
    {
        public class CheapBeforeNoonExpensiveAfterNoonFeeSchedule : IFeeSchedule
        {
            public int GetFeeForTime(DateTime date)
            {
                return date.TimeOfDay.CompareTo(TimeSpan.Parse("12:00:00")) < 0 ? 10 : 20;
            }
        }
    }
}
