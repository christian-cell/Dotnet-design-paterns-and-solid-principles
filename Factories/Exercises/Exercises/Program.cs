namespace Exercises
{
    public interface ICar
    {
        string BuildDoors();
    }

    public interface IBike
    {
        string BuilderWheels();
    }

    internal class Car : ICar
    {
        public string BuildDoors()
        {
            return $"these are the doors of the car";
        }
    }

    internal class Bike : IBike
    {
        public string BuilderWheels()
        {
            return "these are the wheels of the bike";
        }
    }

    internal class MotorBike : IBike
    {
        public string BuilderWheels()
        {
            return "these are the wheels of the motorBike";
        }
    }
    

    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var item in typeof(Bike).Assembly.GetTypes())
            {
                Console.WriteLine($"is a class : {item.IsClass}");
                Console.WriteLine($"the baseType is : {item.BaseType}");
                Console.WriteLine($"the class name is : {item.FullName}");
            }
        }
    }
}