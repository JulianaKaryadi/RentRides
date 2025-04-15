using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.flyweight
{
    public class CarFlyweight
    {
        public string Brand { get; private set; }
        public string Type { get; private set; }

        public CarFlyweight(string brand, string type)
        {
            Brand = brand;
            Type = type;
        }

        public void Display(string color, string transmission, int seats, string fuelType, decimal dailyRate, string noPlat, string carImage)
        {
            Console.WriteLine($"Car: {Brand}, {Type}, {color}, {transmission}, {seats}, {fuelType}, {dailyRate}, {noPlat}, {carImage}");
        }
    }
}