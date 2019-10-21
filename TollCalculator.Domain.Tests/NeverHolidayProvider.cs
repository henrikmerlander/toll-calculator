using System;
using TollCalculator.Domain.Holidays;

namespace TollCalculator.Domain.Tests
{
    public class NeverHolidayProvider : IHolidayProvider
    {
        public bool IsHoliday(DateTime date)
        {
            return false;
        }
    }
}
