using System;
using TollCalculator.Domain.FeeSchedule;

namespace TollCalculator.Domain.Tests.Dummies
{
    public class NoFeeSchedule : IFeeSchedule
    {
        public int GetFeeForTime(DateTime date)
        {
            return 0;
        }
    }
}
