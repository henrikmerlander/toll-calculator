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

        [TestMethod]
        public void PassingTwoTimesInTheMorningAndTwoTimesInTheAfternoonChargesTwice()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new FixedFeeSchedule(10));
            var earlyPass = DateTime.Parse("2019-10-04T08:00:00");
            var earlyPass2 = DateTime.Parse("2019-10-04T08:05:00");

            var latePass = DateTime.Parse("2019-10-04T14:00:00");
            var latePass2 = DateTime.Parse("2019-10-04T14:05:00");

            var tollFee = sut.GetTollFee(new Car(), new DateTime[] { earlyPass, earlyPass2, latePass, latePass2 });

            Assert.AreEqual(20, tollFee);
        }

        [TestMethod]
        public void PassingContinuouslyWithOneHourInBetweenChargesEachTime()
        {
            var sut = new EvolveTollCalculator(new NeverHolidayProvider(), new FixedFeeSchedule(10));

            var tollFee = sut.GetTollFee(new Car(), new DateTime[]
            {
                DateTime.Parse("2019-10-04T11:00:00"),
                DateTime.Parse("2019-10-04T12:00:00"),
                DateTime.Parse("2019-10-04T13:00:00"),
                DateTime.Parse("2019-10-04T14:00:00"),
                DateTime.Parse("2019-10-04T15:00:00"),
            });

            Assert.AreEqual(50, tollFee);
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
