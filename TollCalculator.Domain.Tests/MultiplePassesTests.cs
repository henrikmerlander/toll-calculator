using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.FeeSchedule;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class MultiplePassesTests
    {
        [TestMethod]
        public void OnlyTheHighestFeeIsChargedPerHour()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new CheapBeforeNoonExpensiveAfterNoonFeeSchedule());
            var beforeNoonPass = DateTime.Parse("2019-10-04T11:55:00");
            var afterNoonPass = DateTime.Parse("2019-10-04T12:05:00");

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { beforeNoonPass, afterNoonPass });

            Assert.AreEqual(20, tollFee);
        }

        private class CheapBeforeNoonExpensiveAfterNoonFeeSchedule : IFeeSchedule
        {
            public int GetFeeForTime(DateTime date)
            {
                return date.TimeOfDay.CompareTo(TimeSpan.Parse("12:00:00")) < 0 ? 10 : 20;
            }
        }
    }
}
