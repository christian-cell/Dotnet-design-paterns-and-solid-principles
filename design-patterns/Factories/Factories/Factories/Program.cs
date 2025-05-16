namespace Factories
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;

        protected Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(double a, double b, CoordinateSystem cs = CoordinateSystem.Cartesian)
        {
            switch (cs)
            {
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    x = a;
                    y = b;
                    break;
            }
        }

        public static Point Origin => new Point(0, 0);

        public static Point Origin2 = new Point(0, 0);
        
    }
    
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    
}