﻿namespace TollCalculator.Domain.Vehicles
{
    public class Foreign : IVehicle
    {
        public string GetVehicleType()
        {
            return "Foreign";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
