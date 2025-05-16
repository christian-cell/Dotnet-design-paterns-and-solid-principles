namespace FunctionalBuilder
{

    public class Person
    {
        public string Name, Position;

        public override string ToString()
        {
            return $"the person {nameof(Name)} is {Name} and {nameof(Position)} is {Position}";
        }
    }

    public abstract class FunctionlBuilder<TSubject, TSelf>
        where TSelf : FunctionlBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public TSelf Called(string name) => Do(p => p.Name = name);
        public TSelf Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        
        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p);
                
                return p;
            });

            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder : FunctionlBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    /*public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public PersonBuilder Called(string name) => Do(p => p.Name = name);
        public PersonBuilder Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        
        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p);
                
                return p;
            });

            return this;
        }
    }*/

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkAs(this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }
    
    public class FunctionalBuilder
    {
        public static void FunctionalBuilderMain(string[] args)
        {
            var person = PersonBuilderExtensions.WorkAs((PersonBuilder)new PersonBuilder()
                    .Called("Sarah"), (string)"coder")
                .Build();
            
            Console.WriteLine((object?)person);
        }
    }
}