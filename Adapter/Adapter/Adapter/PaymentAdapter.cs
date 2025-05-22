namespace Adapter
{

    #region Interface

    public interface IPaymentService
    {
        bool ProcessPayment(decimal amount);
    }

    #endregion

    public class ExternalPaymentProcessor
    {
        public bool MakePayment(double amount)
        {
            return true;
        }
    }
    
    public class PaymentAdapter : IPaymentService
    {
        private readonly ExternalPaymentProcessor _externalPaymentProcessor;

        public PaymentAdapter(ExternalPaymentProcessor externalPaymentProcessor)
        {
            _externalPaymentProcessor = externalPaymentProcessor;
        }

        public bool ProcessPayment(decimal amount)
        {
            return _externalPaymentProcessor.MakePayment((double)amount);
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            ExternalPaymentProcessor externalPaymentProcessor = new ExternalPaymentProcessor();
            IPaymentService paymentService = new PaymentAdapter(externalPaymentProcessor);
            
            decimal amount = 100.50m;
            bool result = paymentService.ProcessPayment(amount);
            
            Console.WriteLine($"Payment of {amount} processed: {result}");
        }
    }
    
    
};

