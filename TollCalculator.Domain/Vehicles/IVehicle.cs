namespace TollCalculator.Domain.Vehicles
{
    public interface IVehicle
    {
        string GetVehicleType();
        bool IsTollFree();
    }
}