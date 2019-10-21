using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests
{
    [TestClass]
    public class TollCalculatorTests
    {
        private TollCalculator _tollCalculator;

        public TollCalculatorTests()
        {
            _tollCalculator = new TollCalculator();
        }
    }
}
