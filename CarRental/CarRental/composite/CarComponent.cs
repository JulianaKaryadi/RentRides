using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.composite
{
    public class CarComponent : ICarComponent
    {
        private string _name;

        public CarComponent(string name)
        {
            _name = name;
        }

        public void Display()
        {
            Console.WriteLine(_name);
        }
    }
}