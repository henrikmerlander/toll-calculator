using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class EvolveTollCalculatorTests
    {
        [TestMethod]
        public void ThrowsWhenPassesAreOnDifferentDays()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var dates = new DateTime[]
            {
                DateTime.Parse("2019-10-09T15:00:00"),
                DateTime.Parse("2019-10-10T16:00:00")
            };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetTollFee(new Car(), dates));
            Assert.AreEqual("Dates must be on the same day", ex.Message);
        }

        [TestMethod]
        public void WeekdayIsCharged()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var date = DateTime.Parse("2019-10-04T15:00:00"); // Friday afternoon

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.IsTrue(tollFee > 0);
        }

        [DataTestMethod]
        [DataRow("2019-10-05T15:00:00")] // Saturday
        [DataRow("2019-10-06T15:00:00")] // Sunday
        public void WeekendsAreFeeFree(string dateString)
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var date = DateTime.Parse(dateString);

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [TestMethod]
        public void HolidaysAreFeeFree()
        {
            var sut = new EvolveTollCalculator(new AlwaysHolidayProvider());
            var date = DateTime.Parse("2019-10-10T15:00:00");

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [TestMethod]
        public void NoFeeDuringNightTime()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var date = DateTime.Parse("2019-10-04T23:00:00"); // Friday night

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [TestMethod]
        public void OnlyTheHighestFeeIsChargedPerHour()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var cheapPass = DateTime.Parse("2019-10-04T14:30:00"); // Before rush hour
            var expensivePass = DateTime.Parse("2019-10-04T15:00:00"); // Rush hour

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { cheapPass, expensivePass });

            Assert.AreEqual(13, tollFee);
        }

        [DataTestMethod]
        [DynamicData(nameof(FeeFreeVehicles), DynamicDataSourceType.Method)]
        public void FeeFreeVehichlesAreNotCharged(IVehicle vehicle)
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var date = DateTime.Parse("2019-10-04T15:00:00"); // Friday afternoon

            var tollFee = sut.GetTollFee(vehicle, new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        public static IEnumerable<object[]> FeeFreeVehicles()
        {
            yield return new object[] { new Motorbike() };
            yield return new object[] { new Tractor() };
            yield return new object[] { new Emergency() };
            yield return new object[] { new Diplomat() };
            yield return new object[] { new Foreign() };
            yield return new object[] { new Military() };
        }
    }
}
