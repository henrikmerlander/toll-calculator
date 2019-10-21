﻿namespace TollCalculator.Domain.Vehicles
{
    public class Emergency : IVehicle
    {
        public string GetVehicleType()
        {
            return "Emergency";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
