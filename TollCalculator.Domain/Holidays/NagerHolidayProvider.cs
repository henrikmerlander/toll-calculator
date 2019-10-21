using System;
using Nager.Date;

namespace TollCalculator.Domain.Holidays
{
    public class NagerHolidayProvider : IHolidayProvider
    {
        public bool IsHoliday(DateTime date)
        {
            return DateSystem.IsPublicHoliday(date, CountryCode.SE);
        }
    }
}
