using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class FreeDaysTests
    {
        [DataTestMethod]
        [DataRow("2019-10-05T15:00:00")] // Saturday
        [DataRow("2019-10-06T15:00:00")] // Sunday
        public void WeekendsAreFeeFree(string dateString)
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new FixedFeeSchedule(10));
            var date = DateTime.Parse(dateString);

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [TestMethod]
        public void HolidaysAreFeeFree()
        {
            var sut = new EvolveTollCalculator(new AlwaysHolidayProvider(), new FixedFeeSchedule(10));
            var date = DateTime.Parse("2019-10-10T15:00:00");

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }
    }
}
