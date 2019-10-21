using System;

namespace TollCalculator.Domain.Holidays
{
    public interface IHolidayProvider
    {
        bool IsHoliday(DateTime date);
    }
}
