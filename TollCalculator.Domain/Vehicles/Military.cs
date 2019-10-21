namespace TollCalculator.Domain.Vehicles
{
    public class Military : IVehicle
    {
        public string GetVehicleType()
        {
            return "Military";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
