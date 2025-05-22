namespace AbstractFactory
{
    
    /*
     * tenemos por una lado un grupo de animales y por otro lado un grupo de comidas favoritas de cada animal
     */

    #region Interface

    public interface IFood
    {
        string GetName();
    }

    public interface IAnimal
    {
        string GetName();
        IFood GetFavoriteFood();
    }
    
    #endregion
    
    public class Grass: IFood
    {
        public string GetName() => "Grass";
    }
    
    public class Meat: IFood
    {
        public string GetName() => "Meat";
    }

    public class Cow : IAnimal
    {
        private IFood _food;

        public Cow(IFood food)
        {
            _food = food;
        }

        public string GetName() => "Cow";

        public IFood GetFavoriteFood() => _food;
    }
    
    public class Lion : IAnimal
    {
        private IFood _food;

        public Lion(IFood food)
        {
            _food = food;
        }

        public string GetName() => "Lion";

        public IFood GetFavoriteFood() => _food;
    }

    public interface IAnimalFactory
    {
        IAnimal CreateAnimal();
        IFood CreateFood();
    }

    public class CowFactory: IAnimalFactory
    {
        public IAnimal CreateAnimal() => new Cow(CreateFood());
        public IFood CreateFood() => new Grass();
    }
    
    public class LionFactory: IAnimalFactory
    {
        public IAnimal CreateAnimal() => new Cow(CreateFood());
        public IFood CreateFood() => new Meat();
    }

    public class AbstractFactory
    {
        public static void Mainabstract(string[] args)
        {
            IAnimalFactory cowFactory = new CowFactory();
            IAnimal cow = cowFactory.CreateAnimal();

            Console.WriteLine(
                $"El animal es: {cow.GetName()}, y su comida favorita es : {cow.GetFavoriteFood().GetName()}");
        }
    }
};

