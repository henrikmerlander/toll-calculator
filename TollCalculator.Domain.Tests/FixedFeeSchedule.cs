using System;
using TollCalculator.Domain.FeeSchedule;

namespace TollCalculator.Domain.Tests
{
    public class FixedFeeSchedule : IFeeSchedule
    {
        private readonly int _fixedFee;

        public FixedFeeSchedule(int fixedFee)
        {
            _fixedFee = fixedFee;
        }

        public int GetFeeForTime(DateTime date)
        {
            return _fixedFee;
        }
    }
}
