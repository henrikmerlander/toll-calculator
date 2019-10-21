using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class TimeOfDayTests
    {
        [TestMethod]
        public void NoFeeDuringNightTime()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider());
            var date = DateTime.Parse("2019-10-04T23:00:00"); // Friday night

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }
    }
}
