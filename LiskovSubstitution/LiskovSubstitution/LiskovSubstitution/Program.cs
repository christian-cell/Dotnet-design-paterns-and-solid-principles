/*
 * do not declare again the properties if they already are in the base class
 * instead that declare them in base as virtual and override them in the new class
 */

namespace LiskovSubstitution
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle(int width = 0, int height = 0)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        /*
         * do not declare again Width and height
         */
        
        /*public new int Width
        {
            set { base.Width = base.Height = value; }
        }

        public new int Height
        {
            set { base.Width = base.Height = value; }
        }*/
        
        /*
         * instead that declare them in Rectangle class as virtual and override them as so
         */
        
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    
    public class Demo
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(String[] args)
        {
            Rectangle rc = new Rectangle(40,20);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area : {Area(sq)}");
        }
    }
}