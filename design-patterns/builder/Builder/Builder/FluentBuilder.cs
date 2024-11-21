namespace Builder
{
    /*
     * BUILDER: 🧑‍🔧🛠️ : when construction gets a little bit too complicated.
     * Motivation: Some objects are simple and can be created in a single constructor call
     * Other objects require a lot of ceremony to create
     * Having an object with 10 constructor arguments is not productive
     * Instead, opt for piecewise construction
     * Builder 🧑‍🔧🛠️ provides an API for constructing an object step-by-step.
     *
     * Whe piecewise object construction is complicated, provide an API for doing it succinctly
     */
    
    /*public class Person
    {
        public string Name;
        public string Position;
        
        public Person(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public class Builder : PersonJobBuilder<Builder>;
        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)} : {Name} , {nameof(Position)}, {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected readonly Person Person = new Person("","");

        public Person Build()
        {
            return Person;
        }
    }

    public class PersonInfoBuilder<TSelf>:PersonBuilder where TSelf : PersonInfoBuilder<TSelf> 
    {
        public TSelf Called(string name)
        {
            Person.Name = name;
            return (TSelf) this;
        }
    }

    public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>> where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorkAsA(string position)
        {
            Person.Position = position;
            return (TSelf) this;
        }
        
    }

    internal class FluentBuilder
    {
        public static void Main(string[] args)
        {
            var me = Person.New.Called("dimitri")
                .WorkAsA("coder")
                .Build();
            
            Console.WriteLine(me);
        }
    }*/
}