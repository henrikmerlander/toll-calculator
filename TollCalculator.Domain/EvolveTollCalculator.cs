using System;
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

        public EvolveTollCalculator(IHolidayProvider holidayProvider, IFeeSchedule feeSchedule)
        {
            _holidayProvider = holidayProvider;
            _feeSchedule = feeSchedule;
        }

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (dates.Select(date => date.Date).Distinct().Count() > 1)
            {
                throw new ArgumentException("Dates must be on the same day");
            }

            var intervalStart = dates[0];
            var totalFee = 0;
            foreach (var date in dates)
            {
                var nextFee = GetTollFee(vehicle, date);
                var tempFee = GetTollFee(vehicle, intervalStart);

                var minutes = (date - intervalStart).TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0)
                    {
                        totalFee -= tempFee;
                    }

                    if (nextFee >= tempFee)
                    {
                        tempFee = nextFee;
                    }

                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }

            return Math.Min(totalFee, MaxPricePerDay);
        }

        private int GetTollFee(IVehicle vehicle, DateTime date)
        {
            if (IsTollFreeDate(date) || vehicle.IsTollFree())
            {
                return 0;
            }

            return _feeSchedule.GetFeeForTime(date);
        }

        private bool IsTollFreeDate(DateTime date)
        {
            return
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday ||
                _holidayProvider.IsHoliday(date);
        }
    }
}
