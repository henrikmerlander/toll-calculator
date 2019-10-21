using System;

namespace TollCalculator.Domain.FeeSchedule
{
    public interface IFeeSchedule
    {
        int GetFeeForTime(DateTime date);
        string Print();
    }
}
