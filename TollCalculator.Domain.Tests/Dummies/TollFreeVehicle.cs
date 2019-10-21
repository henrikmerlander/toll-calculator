using System;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain.Tests.Dummies
{
    public class TollFreeVehicle : IVehicle
    {
        public string GetVehicleType()
        {
            return "Toll Free Vehicle";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
