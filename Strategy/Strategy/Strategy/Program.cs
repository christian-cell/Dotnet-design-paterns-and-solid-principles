namespace Strategy
{
    #region interface

    public interface ICalculator
    {
        double Calculate(double originalPrice);
    }

    #endregion

    #region CalculatorHandlers

    public class RegularClientCalculator: ICalculator
    {
        public double Calculate(double originalPrice)
        { 
            return originalPrice - ( originalPrice * 0.10 );
        }
    }
    
    public class PremiumClientCalculator: ICalculator
    {
        public double Calculate(double originalPrice)
        { 
            return originalPrice - ( originalPrice * 0.30 );
        }
    }
    
    public class NewClientCalculator: ICalculator
    {
        public double Calculate(double originalPrice)
        { 
            return originalPrice;
        }
    }

    #endregion

    public class Client
    {
        private ICalculator _calculator;

        public Client(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public double Calculate(double originalPrice)
        {
            return _calculator.Calculate(originalPrice);
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var regularClient = new Client(new RegularClientCalculator());
            var premiumClient = new Client(new PremiumClientCalculator());
            var newClient = new Client(new NewClientCalculator());
            
            Console.WriteLine($"the final price for regular client is : {regularClient.Calculate( 100 )}");
            Console.WriteLine($"the final price for premium client is : {premiumClient.Calculate( 100 )}");
            Console.WriteLine($"the final price for new client is : {newClient.Calculate( 100 )}");
        }
    }
};

