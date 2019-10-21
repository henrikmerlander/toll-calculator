using System;
using System.Linq;
using TollCalculator.Domain;
using TollCalculator.Domain.FeeSchedule;
using TollCalculator.Domain.Holidays;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.ConsoleExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var holidayProvider = new NagerHolidayProvider();
            var feeSchedule = new EvolveFeeSchedule();
            var tollCalculator = new EvolveTollCalculator(holidayProvider, feeSchedule);

            var vehicle = new Car();
            var dates = new DateTime[]
            {
                DateTime.Parse("2019-10-04T05:50:00"),
                DateTime.Parse("2019-10-04T06:10:00"),
                DateTime.Parse("2019-10-04T12:00:00"),
                DateTime.Parse("2019-10-04T16:50:00"),
                DateTime.Parse("2019-10-04T17:10:00"),
            };

            Console.WriteLine($"Fee schedule:\n{feeSchedule.Print()}");
            Console.WriteLine($"Vehicle: {vehicle.GetVehicleType()}");
            Console.WriteLine($"Passes:\n{string.Join("\n", dates)}");
            Console.WriteLine($"Total toll fee: {tollCalculator.GetTollFee(vehicle, dates)}");
        }
    }
}
