using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class FreeVehiclesTests
    {
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
