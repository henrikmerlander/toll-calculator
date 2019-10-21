namespace TollCalculator.Domain.Vehicles
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
