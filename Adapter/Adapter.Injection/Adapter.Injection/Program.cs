using Autofac;
using Autofac.Features.Metadata;

namespace Adapter.Injection
{

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine($"Saving current file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine($"Opening a file");
        }
    }

    public class Button
    {
        private ICommand _command;
        private string _name;

        public Button(ICommand command, string name)
        {

            if (command == null)
            {
                throw new ArgumentNullException(paramName: nameof(command));
            }
            
            this._command = command;
            this._name = name;
        }

        public void Click()
        {
            _command.Execute();
        }

        public void Printme()
        {
            Console.WriteLine($"I am a button called {_name}");
        }
    }

    public class Editor
    {
        private readonly IEnumerable<Button> _buttons;
        
        public IEnumerable<Button> Buttons => _buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            _buttons = buttons;
        }

        public void ClickAll()
        {
            foreach (var button in _buttons)
            {
                button.Click();
            }
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenCommand>()
                .As<ICommand>()
                .WithMetadata("Name", "Open");

            builder.RegisterType<SaveCommand>()
                .As<ICommand>()
                .WithMetadata("Name", "Save");
            
            builder.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd, ""));
            builder.RegisterAdapter<Meta<ICommand>, Button>(cmd =>
                new Button(cmd.Value, (string)cmd.Metadata["Name"]));

            builder.RegisterType<Editor>();

            using (var c = builder.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();

                foreach (var btn in editor.Buttons)
                    btn.Printme();
            }
        }
    }
}