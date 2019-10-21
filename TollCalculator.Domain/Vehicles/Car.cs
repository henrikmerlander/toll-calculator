namespace TollCalculator.Domain.Vehicles
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return "Car";
        }

        public bool IsTollFree()
        {
            return false;
        }
    }
}