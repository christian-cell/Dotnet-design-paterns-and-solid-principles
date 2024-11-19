using System.Text;

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
    
    public class HtmlElement
    {
        public string Name, Text;
        public readonly List<HtmlElement> Elements = new List<HtmlElement>();
        private const int IndentSize = 2;

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * ( indent + 1 )));
                sb.AppendLine(Text);

                
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    
    public class HtmlBuilder
    {
        private readonly string _rootName;
        HtmlElement _root = new HtmlElement("" , "");

        public HtmlBuilder(string rootName)
        {
            this._rootName = rootName;
            _root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string chidText)
        {
            var e = new HtmlElement(childName, chidText);
            _root.Elements.Add(e);

            return this;
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public void Clear()
        {
            _root = new HtmlElement(_rootName , "");
        }
    }
    
    public class FluentBuilder
    {
        /*
         * LIVE WITHOUT BUILDER PATTERN
         */
        static void /*Main*/ Mainy(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");

            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");

            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }

            sb.Append("</ul>");
            
            Console.WriteLine(sb);

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");//Fluent Builder
            
            Console.WriteLine(builder.ToString());
        }
    }
};

