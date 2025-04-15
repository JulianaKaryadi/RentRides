namespace CarRental.chain
{
    public abstract class CarDetailValidator
    {
        protected CarDetailValidator nextValidator;

        public void SetNext(CarDetailValidator nextValidator)
        {
            this.nextValidator = nextValidator;
        }

        public abstract bool Validate(string brand, int year, decimal dailyRate);
    }
}
