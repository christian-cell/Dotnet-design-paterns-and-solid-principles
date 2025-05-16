namespace Builder
{
    /*
     * StepWiseBuilder : is the way to get a builder perform a set of steps in a specific order
     * in the next example we need to get first of all the CarType and then  we use interface segregation
     */

    #region Car

    public enum CarType
    {
        Sedan, Crossover
    }

    public class Car
    {
        public CarType CarType { get; set; }
        public int WheelSize { get; set; }

        public override string ToString()
        {
            return $"the car is of {nameof(CarType)} : {CarType} and has a {nameof(WheelSize)} of {WheelSize}";
        }
    }

    #endregion
    
    #region CarBuilderRegion
    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType carType);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }
    
    public interface IBuildCar
    {

        public Car Build();
    }
    #endregion

    public class CarBuilder
    {
        private class Implementation : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car _car = new Car();
            
            public ISpecifyWheelSize OfType(CarType type)
            {
                _car.CarType = type;

                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (_car.CarType)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"wrong size of wheel for {_car.CarType}");
                }

                _car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return _car;
            }
            
        }
        
        public static ISpecifyCarType Create()
        {
            return new Implementation();
        }
    }
    
    
    public class Demo
    {
       
        
        public static void Mainer(string[] args)
        {
            var car = CarBuilder
                .Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
            
            Console.WriteLine(car);
        }
    }
}