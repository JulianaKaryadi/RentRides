using System.Data.SqlClient;

using CarRental.builder;

namespace CarRental.template
{
    public abstract class CarAdderTemplate
    {
        public void AddCar(string brand, string type, int year, string color, string transmission, int seats, string fuelType, decimal dailyRate, string noPlat, string carImage)
        {
            if (ValidateCarDetails(brand, year, dailyRate))
            {
                var command = BuildCarInsertCommand(brand, type, year, color, transmission, seats, fuelType, dailyRate, noPlat, carImage);
                ExecuteCarInsertCommand(command);
            }
        }

        protected abstract bool ValidateCarDetails(string brand, int year, decimal dailyRate);
        protected abstract SqlCommand BuildCarInsertCommand(string brand, string type, int year, string color, string transmission, int seats, string fuelType, decimal dailyRate, string noPlat, string carImage);
        protected abstract void ExecuteCarInsertCommand(SqlCommand command);
    }
}
