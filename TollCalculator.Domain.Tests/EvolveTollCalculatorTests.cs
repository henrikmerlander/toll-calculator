using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class EvolveTollCalculatorTests
    {
        private EvolveTollCalculator _evolveTollCalculator;

        public EvolveTollCalculatorTests()
        {
            _evolveTollCalculator = new EvolveTollCalculator();
        }

        [TestMethod]
        public void WeekdayIsCharged()
        {
            var date = DateTime.Parse("2019-10-04T15:00:00"); // Friday afternoon

            var tollFee = _evolveTollCalculator.GetTollFee(new Car(), new DateTime[] { date });

            Assert.IsTrue(tollFee > 0);
        }

        [TestMethod]
        public void NoFeeDuringNightTime()
        {
            var date = DateTime.Parse("2019-10-04T23:00:00"); // Friday night

            var tollFee = _evolveTollCalculator.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [DataTestMethod]
        [DataRow("2019-10-05")] // Saturday
        [DataRow("2019-10-06")] // Sunday
        public void WeekendsAreFeeFree(string dateString)
        {
            var date = DateTime.Parse(dateString);

            var tollFee = _evolveTollCalculator.GetTollFee(new Car(), new DateTime[] { date });

            Assert.AreEqual(0, tollFee);
        }

        [DataTestMethod]
        [DynamicData(nameof(FeeFreeVehicles), DynamicDataSourceType.Method)]
        public void FeeFreeVehichlesAreNotCharged(IVehicle vehicle)
        {
            var date = DateTime.Parse("2019-10-04T15:00:00"); // Friday afternoon

            var tollFee = _evolveTollCalculator.GetTollFee(vehicle, new DateTime[] { date });

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
