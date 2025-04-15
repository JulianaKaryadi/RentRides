namespace CarRental.chain
{
    public class CarBrandValidator : CarDetailValidator
    {
        public override bool Validate(string brand, int year, decimal dailyRate)
        {
            if (string.IsNullOrEmpty(brand))
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
