using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.FeeSchedule;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class EvolveFeeScheduleTests
    {
        [TestMethod]
        public void NoFeeDuringNightTime()
        {
            var sut = new EvolveFeeSchedule();
            var fridayNight = DateTime.Parse("2019-10-04T23:00:00");

            var tollFee = sut.GetFeeForTime(fridayNight);

            Assert.AreEqual(0, tollFee);
        }

        [DataTestMethod]
        [DataRow("2019-10-10T05:00", 0)]
        [DataRow("2019-10-10T06:00", 8)]
        [DataRow("2019-10-10T06:30", 13)]
        [DataRow("2019-10-10T07:00", 18)]
        [DataRow("2019-10-10T08:00", 13)]
        [DataRow("2019-10-10T08:30", 8)]
        [DataRow("2019-10-10T12:00", 8)]
        [DataRow("2019-10-10T12:15", 8)]
        [DataRow("2019-10-10T12:30", 8)]
        [DataRow("2019-10-10T12:45", 8)]
        [DataRow("2019-10-10T13:00", 8)]
        [DataRow("2019-10-10T15:00", 13)]
        [DataRow("2019-10-10T15:30", 18)]
        [DataRow("2019-10-10T16:00", 18)]
        [DataRow("2019-10-10T17:00", 13)]
        [DataRow("2019-10-10T18:00", 8)]
        [DataRow("2019-10-10T18:30", 0)]
        [DataRow("2019-10-10T20:00", 0)]
        public void TollFeeTimeOfDay(string dateString, int expectedFee)
        {
            var sut = new EvolveFeeSchedule();
            var date = DateTime.Parse(dateString);

            var tollFee = sut.GetFeeForTime(date);
        }
    }
}
