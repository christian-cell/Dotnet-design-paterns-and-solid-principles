/*
 * Instead of having one big interface, you should have lots of smaller interfaces which are more
 * atomic, shall we say, in the sense that they are kind of self-contained and they cover a particular
 * concern
 */

namespace InterfaceSegregation
{
    public abstract class Demo
    {
        public abstract class Document;

        private interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultifunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                //;
            }

            public void Scan(Document d)
            {
                //;
            }

            public void Fax(Document d)
            {
                //;
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }

            public void Fax(Document d)
            {
                //
            }
        }

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        private interface IMultiFunctionDevice : IScanner, IPrinter;

        public class MultifunctionMachine(IPrinter printer, IScanner scanner) : IMultiFunctionDevice
        {
            private readonly IPrinter _printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            private readonly IScanner _scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));

            public void Print(Document d)
            {
                _printer.Print(d);
            }

            public void Scan(Document d)
            {
                _scanner.Scan(d);
            }// decorator pattern
        }

        private static void Main()
        {
            Console.WriteLine($"estamos arrancando el programa");
        }
    }
};

