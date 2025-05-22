namespace SimpleFactory
{

    public interface IProduct
    {
        double GetTax();
    }
    
    public class Book : IProduct
    {
        public double GetTax()
        {
            return 4.5;
        }
    }
    
    public class Phone : IProduct
    {
        public double GetTax()
        {
            return 21.5;
        }
    }
    
    public class Pencil : IProduct
    {
        public double GetTax()
        {
            return 12.5;
        }
    }

    public enum ProductType
    {
        Book, Pencil,Phone
    }

    public class ProductFactory
    {
        public IProduct ProductHandler(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Book:
                    return new Book();
                case ProductType.Pencil:
                    return new Pencil();
                case ProductType.Phone:
                    return new Phone();
                default: return null;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var productFactory = new ProductFactory();
            IProduct book = productFactory.ProductHandler(ProductType.Book);
            double bookTax = book.GetTax();
            
            Console.Write($"the book has a tax of : {bookTax}");
        }
    }
}