using System;
using TollCalculator.Domain.Vehicles;

namespace TollCalculator.Domain
{
    public interface ITollCalculator
    {
        int GetTollFee(IVehicle vehicle, DateTime date);
        int GetTollFee(IVehicle vehicle, DateTime[] dates);
    }
}
