using System;
using TollCalculator.Domain.Holidays;

namespace TollCalculator.Domain.Tests
{
    public class AlwaysHolidayProvider : IHolidayProvider
    {
        public bool IsHoliday(DateTime date)
        {
            return true;
        }
    }
}
