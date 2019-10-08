using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class MaximumFeeTests
    {
        [TestMethod]
        public void NoMoreThan60SEKChargedPerDay()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new FixedFeeSchedule(20));
            var dates = new DateTime[]
            {
                DateTime.Parse("2019-10-10T06:30:00"),
                DateTime.Parse("2019-10-10T08:00:00"),
                DateTime.Parse("2019-10-10T15:00:00"),
                DateTime.Parse("2019-10-10T16:00:00"),
                DateTime.Parse("2019-10-10T17:00:00"),
                DateTime.Parse("2019-10-10T18:00:00"),
            };

            var tollFee = sut.GetTollFee(new Car(), dates);

            Assert.AreEqual(60, tollFee);
        }
    }
}
