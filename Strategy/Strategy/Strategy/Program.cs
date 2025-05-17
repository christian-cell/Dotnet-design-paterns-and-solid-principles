namespace Strategy
{
    
    /*
     *  In SwitchCase file we have an example of a switch case method
     *  And here we get the same functionality with STRATEGY pattern
     */

    public interface IDiscountStrategy
    {
        double ApplyDiscount(double purchaseTotal);
    }
    
    public class RegularCustomerDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double purchaseTotal)
        {
            return purchaseTotal - purchaseTotal * 0.1;
        }
    }
    
    public class PremiumCustomerDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double purchaseTotal)
        {
            return purchaseTotal - purchaseTotal * 0.2;
        }
    }
    
    public class NewCustomerDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double purchaseTotal)
        {
            return purchaseTotal;
        }
    }

    public class Customer
    {
        private IDiscountStrategy _discountStrategy;

        public Customer(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public double ApplyDiscount(double purchaseTotal)
        {
            return _discountStrategy.ApplyDiscount(purchaseTotal);
        }        
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var regularCustomer = new Customer(new RegularCustomerDiscount());

            var premiumCustomer = new Customer(new PremiumCustomerDiscount());
            
            var newCustomer = new Customer(new NewCustomerDiscount());
            
            Console.WriteLine(regularCustomer.ApplyDiscount(100.0));
            Console.WriteLine(premiumCustomer.ApplyDiscount(100.0));
            Console.WriteLine(newCustomer.ApplyDiscount(100.0));

        }
    }
}