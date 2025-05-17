using Autofac;

namespace Bridge
{
    
    /*
     *  The Bridge pattern allow disengage an abstraction of an implementation in order to both of them can
     *  independently vary 
     * 
     */

    #region Interface

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    #endregion

    #region Methods

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle radius {radius}");
        }
    }

    #endregion


    #region ShapeCalculates

    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            this.Renderer = renderer;
        }

        public abstract void Draw();

        public abstract void Resize(float factor);
    }

    #endregion

    #region myCircle

    public class Circle : Shape
    {
        private float radius;
        
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            Renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    #endregion
    
    public class Program
    {
        public static void Mainy(string[] args)
        {
            var cb = new ContainerBuilder();

            cb.RegisterType<VectorRenderer>().As<IRenderer>();

            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(
                    new PositionalParameter(0, 5.0f)
                );

                circle.Draw();
                circle.Resize(2);
                circle.Draw();
            }

        }
    }
}