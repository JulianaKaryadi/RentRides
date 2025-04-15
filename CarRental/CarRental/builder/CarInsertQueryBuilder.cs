using System.Data.SqlClient;

namespace CarRental.builder
{
    public class CarInsertQueryBuilder
    {
        private string sql;
        private SqlCommand command;

        public CarInsertQueryBuilder()
        {
            sql = "INSERT INTO CarDetails (Brand, Type, Year, Color, Transmission, Seats, FuelType, DailyRate, NoPlat, CarImage) " +
                  "VALUES (@Brand, @Type, @Year, @Color, @Transmission, @Seats, @FuelType, @DailyRate, @NoPlat, @CarImage)";
            command = new SqlCommand(sql);
        }

        public CarInsertQueryBuilder WithBrand(string brand)
        {
            command.Parameters.AddWithValue("@Brand", brand);
            return this;
        }

        public CarInsertQueryBuilder WithType(string type)
        {
            command.Parameters.AddWithValue("@Type", type);
            return this;
        }

        public CarInsertQueryBuilder WithYear(int year)
        {
            command.Parameters.AddWithValue("@Year", year);
            return this;
        }

        public CarInsertQueryBuilder WithColor(string color)
        {
            command.Parameters.AddWithValue("@Color", color);
            return this;
        }

        public CarInsertQueryBuilder WithTransmission(string transmission)
        {
            command.Parameters.AddWithValue("@Transmission", transmission);
            return this;
        }

        public CarInsertQueryBuilder WithSeats(int seats)
        {
            command.Parameters.AddWithValue("@Seats", seats);
            return this;
        }

        public CarInsertQueryBuilder WithFuelType(string fuelType)
        {
            command.Parameters.AddWithValue("@FuelType", fuelType);
            return this;
        }

        public CarInsertQueryBuilder WithDailyRate(decimal dailyRate)
        {
            command.Parameters.AddWithValue("@DailyRate", dailyRate);
            return this;
        }

        public CarInsertQueryBuilder WithNoPlat(string noPlat)
        {
            command.Parameters.AddWithValue("@NoPlat", noPlat);
            return this;
        }

        public CarInsertQueryBuilder WithCarImage(string carImage)
        {
            command.Parameters.AddWithValue("@CarImage", carImage);
            return this;
        }

        public SqlCommand Build()
        {
            return command;
        }
    }
}
