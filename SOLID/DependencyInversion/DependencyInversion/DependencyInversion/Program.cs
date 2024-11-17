/*
 * The dependency inversion principle is very simple, and it states that high level parts of the system
 * should not depend on low level parts of the system directly, that instead they should depend on some
 * kind of abstraction.
 */

namespace DependencyInversion
{
  public enum Relationship
  {
    Parent, Child, Sibling
  }

  public class Person
  {
    public string Name;

    public Person(string name)
    {
      Name = name;
    }
  }

  public interface IRelationshipBrowser
  {
    IEnumerable<Person> FindAllChildrenOf(string name);
  }

  // low-level
  public class Relationships : IRelationshipBrowser
  {
    private readonly List<(Person, Relationship, Person)> _relations = new List<(Person, Relationship, Person)>();

    public void AddParentChild(Person parent, Person child)
    {
      _relations.Add(((parent, Relationship.Parent, child)));
      _relations.Add(((child, Relationship.Child, parent)));
    }

    //public List<(Person, Relationship, Person)> Relations => _relations;
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
      return _relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(r => r.Item3);
    }
  }
  
  public class Research
  {

    /*public Research( Relationships relationships )
    {
      var relations = relationships.Relations;

      foreach (var r in relations.Where(x => x.Item1.Name == "Jhon" && x.Item2 == Relationship.Parent))
      {
        Console.WriteLine($"Jhon has a child called {r.Item3.Name}");
      }
    }*/

    public Research(IRelationshipBrowser browser)
    {
      foreach (var p in browser.FindAllChildrenOf("John"))
          Console.WriteLine($"John has a child called {p.Name}");
    }
    
    static void Main()
    {
      var parent = new Person ("John");
      var child1 = new Person ("Chris");
      var child2 = new Person ("Mary");

      var relationships = new Relationships();
      relationships.AddParentChild(parent, child1);
      relationships.AddParentChild(parent, child2);
      
      new Research(relationships);
    }
  }  
};