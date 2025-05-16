namespace Adapter
{
    
    /*
     * Adapter pattern catch an object calls and transform them to a format and interface for the second object
     * The example of bellow answer the following questions
     * wich classes are composed ?
     * wich roles have these classes ?
     * wich way the patter items interact ?
     */
    
    # region Interface
    public interface ITarget
    {
        string GetRequest();
    }
    # endregion

    #region Object to adapt

    public class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request";
        }
    }

    #endregion

    #region Adapter

    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"This is '{this._adaptee.GetSpecificRequest()}'";
        }
    }

    #endregion
    
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);
            
            Console.WriteLine($"Adaptee interface is incompatible with the client !");
            Console.WriteLine($"But with adapter client can call it's method");
            
            Console.Write($"{target.GetRequest()}");
        }
    }
}