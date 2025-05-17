using Autofac;
using static System.Console;

namespace Bridge
{
    #region Interface

    public interface IDevice
    {
        void TurnOn();
        void TurnOff();
    }

    #endregion

    public class Radio : IDevice
    {
        public void TurnOn()
        {
            Console.WriteLine($"Radio is on.");
        }

        public void TurnOff()
        {
            Console.WriteLine($"Radio is off.");
        }
    }
    
    public class Tv : IDevice
    {
        public void TurnOn()
        {
            Console.WriteLine($"Tv is on.");
        }

        public void TurnOff()
        {
            Console.WriteLine($"Tv is off.");
        }
    }

    public abstract class RemoteButton
    {
        protected IDevice Device;

        protected RemoteButton(IDevice device)
        {
            Device = device;
        }
        
        public void TurnOn()
        {
            Device.TurnOn();
        }

        public void TurnOff()
        {
            Device.TurnOff();
        }
    }
    
    public class ConcreteRemote : RemoteButton
    {
        public ConcreteRemote(IDevice device) : base(device)
        {
        }

        public void ExtraFunctionality()
        {
            Console.WriteLine($"Activating an extra functionality from the ConcreteRemote: mute");
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            IDevice tv = new Tv();

            RemoteButton remoteForTv = new ConcreteRemote(tv);
            remoteForTv.TurnOn(); // output: "TV is on."
            remoteForTv.TurnOff(); // output: "TV is off."

            IDevice radio = new Radio();
            RemoteButton remoteForRadio = new ConcreteRemote(radio);
            remoteForRadio.TurnOn(); // output: "Radio is on."
            remoteForRadio.TurnOff(); // output: "Radio is off."
        }
    }
}
