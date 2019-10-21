﻿using System;
using System.Linq;
using TollCalculator.Domain.Holidays;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain
{
    public class EvolveTollCalculator : ITollCalculator
    {
        private IHolidayProvider _holidayProvider;

        public EvolveTollCalculator(IHolidayProvider holidayProvider)
        {
            _holidayProvider = holidayProvider;
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
            if (dates.GroupBy(date => date.Date).Count() > 1)
            {
                throw new ArgumentException("Dates must be on the same day");
            }

            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(vehicle, date);
                int tempFee = GetTollFee(vehicle, intervalStart);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

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
            if (totalFee > 60)
            {
                totalFee = 60;
            }

            return totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null)
            {
                return false;
            }

            string vehicleType = vehicle.GetVehicleType();
            return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        private int GetTollFee(IVehicle vehicle, DateTime date)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            {
                return 0;
            }

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29)
            {
                return 8;
            }
            else if (hour == 6 && minute >= 30 && minute <= 59)
            {
                return 13;
            }
            else if (hour == 7 && minute >= 0 && minute <= 59)
            {
                return 18;
            }
            else if (hour == 8 && minute >= 0 && minute <= 29)
            {
                return 13;
            }
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59)
            {
                return 8;
            }
            else if (hour == 15 && minute >= 0 && minute <= 29)
            {
                return 13;
            }
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59)
            {
                return 18;
            }
            else if (hour == 17 && minute >= 0 && minute <= 59)
            {
                return 13;
            }
            else if (hour == 18 && minute >= 0 && minute <= 29)
            {
                return 8;
            }
            else
            {
                return 0;
            }
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            if (_holidayProvider.IsHoliday(date))
            {
                return true;
            }

            return false;
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}