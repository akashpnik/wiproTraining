//open closed princiople
namespace SolidPrinciples
{
    public class ocp
    {
        // Violation
        /*
        public class DiscountCalculator
        {
            public double CalculateDiscount(string customerType)
            {
                if (customerType == "Regular") return 0.1;
                else if (customerType == "Premium") return 0.2;
                return 0;
            }
        }
        */
        // OCP Applied using polymorphism
        public interface IDiscountStrategy
        {
            double GetDiscount();
        }

        public class RegularCustomer : IDiscountStrategy
        {
            public double GetDiscount() => 0.1;
        }

        public class PremiumCustomer : IDiscountStrategy
        {
            public double GetDiscount() => 0.2;
        }

        public class DiscountCalculator
        {
            public double CalculateDiscount(IDiscountStrategy customer)
            {
                return customer.GetDiscount();
            }
        }

    }
}
