using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Creational.Prototype
{
    /*
     * this pattern is used when the type of the object to create need to be decided in execution time
     * Where Foo is the prototype, is the class we'll going to copy
     */

    public static class ExtensionMethods
    {
        [Obsolete("Obsolete")]
        public static T DeepCopy<T>(this T self)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }

    public class Foo
    {
        public uint Stuff;
        public string Whatever;

        public override string ToString()
        {
            return $"{nameof(Stuff)}: {Stuff}, {nameof(Whatever)}: {Whatever}";
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Foo foo = new Foo { Stuff = 36, Whatever = "Christian" };
            Foo foo2 = foo.DeepCopyXml();

            foo2.Whatever = "xyz";
            Console.WriteLine(foo);
            Console.WriteLine(foo2);
        }
    }
}