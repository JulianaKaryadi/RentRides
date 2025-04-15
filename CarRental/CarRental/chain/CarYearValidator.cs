namespace CarRental.chain
{
    public class CarYearValidator : CarDetailValidator
    {
        public override bool Validate(string brand, int year, decimal dailyRate)
        {
            if (year <= 0)
            {
                return false;
            }

            if (nextValidator != null)
            {
                return nextValidator.Validate(brand, year, dailyRate);
            }

            return true;
        }
    }
}
