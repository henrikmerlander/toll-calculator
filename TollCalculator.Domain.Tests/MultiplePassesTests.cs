using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class MultiplePassesTests
    {
        [TestMethod]
        public void OnlyTheHighestFeeIsChargedPerHour()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var cheapPass = DateTime.Parse("2019-10-04T14:30:00"); // Before rush hour
            var expensivePass = DateTime.Parse("2019-10-04T15:00:00"); // Rush hour

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { cheapPass, expensivePass });

            Assert.AreEqual(13, tollFee);
        }
    }
}
