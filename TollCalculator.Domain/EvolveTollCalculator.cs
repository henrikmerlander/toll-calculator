using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Domain.FeeSchedule;
using TollCalculator.Domain.Holidays;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain
{
    public class EvolveTollCalculator : ITollCalculator
    {
        private readonly IHolidayProvider _holidayProvider;
        private readonly IFeeSchedule _feeSchedule;

        private const int MaxPricePerDay = 60;
        private TimeSpan FeeGracePeriod = TimeSpan.FromHours(1);

        public EvolveTollCalculator(IHolidayProvider holidayProvider, IFeeSchedule feeSchedule)
        {
            _holidayProvider = holidayProvider;
            _feeSchedule = feeSchedule;
        }

        /// <summary>
        /// Calculate the total toll fee for one day
        /// </summary>
        /// <param name="vehicle">The vehicle</param>
        /// <param name="dates">Date and time of all passes on one day</param>
        /// <returns>
        /// The total toll fee for that day
        /// </returns>
        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (dates.Select(date => date.Date).Distinct().Count() > 1)
            {
                throw new ArgumentException("Dates must be on the same day");
            }

            var orderedDates = dates.OrderBy(date => date);
            var firstDate = orderedDates.First();

            if (IsTollFreeDate(firstDate) || vehicle.IsTollFree())
            {
                return 0;
            }

            var feeablePeriods = GetFeeablePeriods(orderedDates);

            var totalFee = feeablePeriods.Sum(period => period.Max(date => _feeSchedule.GetFeeForTime(date)));

            return Math.Min(totalFee, MaxPricePerDay);
        }

        private bool IsTollFreeDate(DateTime date)
        {
            return
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday ||
                _holidayProvider.IsHoliday(date);
        }

        private List<List<DateTime>> GetFeeablePeriods(IOrderedEnumerable<DateTime> orderedDates)
        {
            var feeablePeriods = new List<List<DateTime>>();

            for (int i = 0; i < orderedDates.Count(); i++)
            {
                var currentDate = orderedDates.ElementAt(i);
                var isNewPeriodInitiated = i == 0 || currentDate - LastPeriodStartDate(feeablePeriods) >= FeeGracePeriod;

                if (isNewPeriodInitiated)
                {
                    feeablePeriods.Add(new List<DateTime>
                    {
                        orderedDates.ElementAt(i)
                    });
                }
                else
                {
                    feeablePeriods.Last().Add(currentDate);
                }
            }

            return feeablePeriods;
        }

        private DateTime LastPeriodStartDate(List<List<DateTime>> periods)
        {
            return periods.Last().First();
        }
    }
}
