using CarRental.flyweight;
using System.Collections.Generic;

namespace CarRental.builder
{
    public class CarFlyweightFactory
    {
        private Dictionary<string, CarFlyweight> _flyweights = new Dictionary<string, CarFlyweight>();

        public CarFlyweight GetFlyweight(string brand, string type)
        {
            string key = $"{brand}-{type}";
            if (!_flyweights.ContainsKey(key))
            {
                _flyweights[key] = new CarFlyweight(brand, type);
            }
            return _flyweights[key];
        }
    }
}