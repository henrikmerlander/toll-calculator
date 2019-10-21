namespace TollCalculator.Domain.Vehicles
{
    public class Tractor : IVehicle
    {
        public string GetVehicleType()
        {
            return "Tractor";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
