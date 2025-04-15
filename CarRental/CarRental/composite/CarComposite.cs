using CarRental.composite;
using System.Collections.Generic;

namespace CarRental.builder
{
    public class CarComposite : ICarComponent
    {
        private List<ICarComponent> _components = new List<ICarComponent>();

        public void Add(ICarComponent component)
        {
            _components.Add(component);
        }

        public void Remove(ICarComponent component)
        {
            _components.Remove(component);
        }

        public void Display()
        {
            foreach (var component in _components)
            {
                component.Display();
            }
        }
    }
}