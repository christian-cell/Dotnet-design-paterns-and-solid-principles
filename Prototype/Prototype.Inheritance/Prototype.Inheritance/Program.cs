namespace Prototype.Inheritance
{
    /*
     * Create a clone of an existing object
     * this is really usefull when you need complex object info, and you do not want the copied object be affected by changes in the original object
     * This is known as Deep Copy
     * in this example CopyTo copy the object properties to ohter object with the same type
     * When a class implements this interfaze , this class is able to be cloned and declare the way to do
     * Finally DeepCopyExtensions are the methods to do the Deep Copy and ease the funcionallity
     */
    
     #region Copy Interface

    public interface IDeepCopyable<T> where T : new()
    {
        void CopyTo(T target);

        public T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }

    #endregion

    #region Objects to copy

    public class Address : IDeepCopyable<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address()
        {
            
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public void CopyTo(Address target)
        {
            target.HouseNumber = HouseNumber;
            target.StreetName = StreetName;
        }
    }

    public class Person : IDeepCopyable<Person>
    {
        public string[] Names;
        public Address Address;
        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Person()
        {
            
        }

        public void CopyTo(Person target)
        {
            target.Names = ((string[]) Names.Clone());
            target.Address = Address.DeepCopy();
        }
        
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;
        
        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }
        
        public override string ToString()
        {
            return $"{base.ToString()},{nameof(Salary)}: {Salary}";
        }
    }

    #endregion
    
    #region extensios methods to copy

    public static class DeepCopyExtensions
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item) 
            where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person)
            where T : Person, new()
        {
            return ((IDeepCopyable<T>) person).DeepCopy();
        }
    }
    
    #endregion
    
    public class Program
    {
        static void Main()
        {
            var john = new Employee();
            john.Names = new[] {"John", "Doe"};
            john.Address = new Address {HouseNumber = 123, StreetName = "London Road"};
            john.Salary = 321000;
            var copy = john.DeepCopy();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;
            copy.Salary = 123000;
      
            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }
}