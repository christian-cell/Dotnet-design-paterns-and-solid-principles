namespace Strategy
{
    public enum CustomerType
    {
        Regular,
        Premium,
        New
    }

    public class CustomerSwitch
    {
        public CustomerType Type;

        public CustomerSwitch(CustomerType type)
        {
            Type = type;
        }

        public double ApplyDiscount(double purchaseTotal)
        {
            switch (Type)
            {
                case CustomerType.Regular:
                    return purchaseTotal * 0.1;
                case CustomerType.Premium:
                    return purchaseTotal * 0.2;
                case CustomerType.New:
                    return purchaseTotal * 0.15;
                default:
                    return purchaseTotal;
            }
        }
    }
    
    public class SwitchCase
    {
        public static void Mainy(string[] args)
        {
            CustomerSwitch customer = new CustomerSwitch(CustomerType.Regular);

            double finalQty = customer.ApplyDiscount(23);
            
            Console.WriteLine($"the final quantity is {finalQty}");
        }
    }
};

