/*
 * The open closed principle states the entities as clases should be openned for extension
 * but closed by modification, in this sample the Class ProductFilter if we need an extra
 * filter we need to modify this class ProductFilter, instead that we create a class BetterFilter to
 * extend filters , creating a new specification and adding that in AndSpecification as second or third
 */

namespace OpenClose
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public enum Age
    {
        Doce, Trece, Catorce
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
        public Age Age;

        public Product(string name, Color color, Size size, Age age)
        {
            Name = name;
            Color = color;
            Size = size;
            Age = age;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Color == _color;
        }
    }
    
    public class AgeSpecification : ISpecification<Product>
    {
        private Age _age;

        public AgeSpecification(Age age)
        {
            _age = age;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Age == _age;
        }
    }
    
    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Size == _size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second, _third;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second, ISpecification<T> third)
        {
            this._first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this._second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            this._third = second ?? throw new ArgumentNullException(paramName: nameof(third));
        }

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t) && _third.IsSatisfied(t);
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class BestFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i)) yield return i;
            }
        }
    }
    
    /*
     
     //Modifiyng ProductFilter is wrong instead that extend by adding new specifications
     
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
     */
    
    public class Demo
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small, Age.Doce);
            var tree = new Product("Tree", Color.Green, Size.Large, Age.Trece);
            var house = new Product("House", Color.Blue, Size.Large, Age.Catorce);

            Product[] products = { apple, tree, house };

            var bestFilter = new BestFilter();

            var productsFilteredBySpecifications = bestFilter.Filter(products, new AndSpecification<Product>(
                new ColorSpecification(Color.Blue),
                new SizeSpecification(Size.Large),
                new AgeSpecification(Age.Trece)
            ));

            foreach (var p in productsFilteredBySpecifications)
            {
                Console.WriteLine($" - {p.Name} is {p.Size} color: {p.Color} and age: {p.Age}");
            }
        }
    }

};

