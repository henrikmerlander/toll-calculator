using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Tests.Dummies;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class EvolveTollCalculatorTests
    {
        [TestMethod]
        public void ThrowsWhenPassesAreOnDifferentDays()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new NoFeeSchedule());
            var dates = new DateTime[]
            {
                DateTime.Parse("2019-10-09T15:00:00"),
                DateTime.Parse("2019-10-10T16:00:00")
            };

            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetTollFee(new Car(), dates));
            Assert.AreEqual("Dates must be on the same day", ex.Message);
        }
    }
}
