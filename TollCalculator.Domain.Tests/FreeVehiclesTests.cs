using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Tests.Dummies;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class FreeVehiclesTests
    {
        [TestMethod]
        public void TollFreeVehichlesAreNotCharged()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new FixedFeeSchedule(10));

            var tollFee = sut.GetTollFee(new TollFreeVehicle(), new DateTime[] { DateTime.Parse("2019-10-04T15:00:00") });

            Assert.AreEqual(0, tollFee);
        }
    }
}
