namespace /*OpenClose*/OtherThing
{
    /*
     * The open closed principle states the entities as clases should be openned for extension
     * and not for modification, in this sample the Class ProductFilter if we need an extra
     * filter we need to modify this class ProductFilter, instead that we create a class BetterFilter to
     * extend filters , creating a new specification and adding that in AndSpecification as second or third
     */
    
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }
        
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }
        
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (var p in products)
            {
                if (p.Color == color && p.Size == size)
                    yield return p;
            }
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            this._color = color;
        }
        public bool IsSatisfied(Product product)
        {
            return product.Color == _color;
        }
    }
    
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        
        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product product)
        {
            return product.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this._first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this._second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                    yield return i;
            }
        }
    }
    
    public class Demo
    {
        static void AnyMain(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Gress products(old):");

            var productsFilteredByColor = pf.FilterByColor(products, Color.Green);
            
            foreach (var p in productsFilteredByColor)
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();
            
            var productsBestFilteredByColor = bf.Filter(products, new ColorSpecification(Color.Green));
            
            Console.WriteLine("Green products (new):");

            foreach (var p in productsBestFilteredByColor)
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            Console.WriteLine("Large blue items");

            var specifications = bf.Filter(products, new AndSpecification<Product>(
                new ColorSpecification(Color.Blue),
                new SizeSpecification(Size.Large)
            ));

            foreach (var p in specifications)
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}