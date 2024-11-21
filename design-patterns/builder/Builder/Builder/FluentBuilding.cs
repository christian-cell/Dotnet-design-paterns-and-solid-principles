namespace Builder
{
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public Person(string name, string position)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }


        public class Builder : PersonJobBuilder<Builder>;
        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)} : {Name} , {nameof(Position)} : {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected readonly Person Person = new Person("", "");

        public Person Build()
        {
            return Person;
        }
    }

    public class PersonInfoBuilder<TSelf> : PersonBuilder where TSelf : PersonInfoBuilder<TSelf>
    {
        public TSelf Called(string name)
        {
            Person.Name = name;

            return (TSelf)this;
        }
    }

    public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>> where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorkAs(string position)
        {
            Person.Position = position;

            return (TSelf) this;
        }
    } 
    
    public class FluentBuilding
    {
        
        public static void MainBuilder(string[] args)
        {
            var me = Person.New.Called("christian").WorkAs("coder").Build();
            
            Console.WriteLine(me);
        }
    }
};

