﻿namespace TollCalculator.Domain.Vehicles
{
    public class Diplomat : IVehicle
    {
        public string GetVehicleType()
        {
            return "Diplomat";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
