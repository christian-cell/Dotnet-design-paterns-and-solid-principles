namespace FluentBuilder
{

    #region Person

    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Person(string name, string email, int age)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Age = age;
        }

        public class Builder : PersonAgeBuilder<Builder>;

        public static Builder NewPerson => new Builder(); 

        public override string ToString()
        {
            return $"{nameof(Name)} : {Name} and {nameof(Email)} : {Email} and {nameof(Age)} : {Age}";
        }
    }

    #endregion

    #region Builder

    public abstract class PersonBuilder
    {
        protected readonly Person Person = new Person("", "", 0);

        public Person Build()
        {
            return Person;
        } 
    }

    public class PersonNameBuilder<TSelf>: PersonBuilder where TSelf : PersonNameBuilder<TSelf> 
    {
        public TSelf SetName(string name)
        {
            Person.Name = name;

            return (TSelf)this;
        }
    }

    public class PersonEmailBuilder<TSelf>: PersonNameBuilder<PersonEmailBuilder<TSelf>> where TSelf : PersonEmailBuilder<TSelf>
    {
        public TSelf SetEmail(string email)
        {
            Person.Email = email;

            return (TSelf)this;
        }
    }

    public class PersonAgeBuilder<TSelf>: PersonEmailBuilder<PersonAgeBuilder<TSelf>> where TSelf : PersonAgeBuilder<TSelf>
    {
        public TSelf SetAge(int age)
        {
            Person.Age = age;

            return (TSelf)this;
        }
    }

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var me = Person.NewPerson
                .SetAge(25)
                .SetName("christian")
                .SetEmail("cristo@gmail.com")
                .Build();
            
            Console.Write(me);
        }
    }
}