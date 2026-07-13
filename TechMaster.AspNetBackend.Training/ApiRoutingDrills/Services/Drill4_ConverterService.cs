namespace ApiRoutingDrills.Services
{
    public class Drill4_ConverterService
    {
        public decimal ConvertCelsiusToFahrenheit(decimal celsius)
        {
            return (celsius * 9 / 5) + 32;
        }
    }
}
