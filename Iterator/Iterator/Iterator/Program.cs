using System.Collections;
using System.Collections.Generic;

namespace Iterator
{

    #region Product

    public class Product
    {
        public string Name { get; set; }
        public Product(string name) => Name = name;
    }

    #endregion
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var productos = new List<Product>
            {
                new Product("Manzana"),
                new Product("Pera"),
                new Product("Fresas"),
                new Product("Aguacate"),
                new Product("Platano"),
                new Product("Sandia"),
                new Product("Piña"),
                new Product("Melocoton"),
                new Product("Arándanos"),
                new Product("Papaya")
            };

            int page = 2;
            int pageSize = 3;

            var pagedProducts = GetPage(productos, page, pageSize);
            
            Console.WriteLine($"Página {page}");

            foreach (var product in pagedProducts)
            {
                Console.WriteLine(product.Name);
            }
        }

        public static IEnumerable<Product> GetPage(IEnumerable<Product> products, int page, int pageSize)
        {
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}