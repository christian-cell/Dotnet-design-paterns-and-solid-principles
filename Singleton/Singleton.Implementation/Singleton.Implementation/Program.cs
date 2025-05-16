using MoreLinq;
using NUnit.Framework;

namespace Singleton.Implementation
{
    /*
     * Creating a singleton pattern we just instance once and reuse the same in whole project
     */

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database");

            var filePath = Path.Combine(
                new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt");

            Console.WriteLine($"Attempting to read file at path: {filePath}");

            capitals = File.ReadAllLines(filePath)
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        // laziness + thread safety
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() =>
        {
            instanceCount++;
            return new SingletonDatabase();
        });

        public static IDatabase Instance => instance.Value;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;

            var city = "Tokyo";
            
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
};

