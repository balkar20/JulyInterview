using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Geometry
{
    public interface IVisitor
    {
        void VisitBody(Body elem);
    }
    public abstract class Body
    {
        public abstract double GetVolume();
        public abstract void Accept(IVisitor visitor);

    }

    public class Ball : Body
    {
        public double Radius { get; set; }
        public override double GetVolume()
        {
            return 4.0 * Math.PI * Radius * Radius * Radius / 3;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBody(this);
        }
    }

    public class Cube : Body
    {
        public double Size { get; set; }
        public override double GetVolume()
        {
            return Size * Size * Size;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBody(this);
        }
    }

    public class Cyllinder : Body
    {
        public double Height { get; set; }
        public double Radius { get; set; }
        public override double GetVolume()
        {
            return Math.PI * Radius * Radius * Height;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBody(this);
        }
    }

    // Заготовка класса для задачи на IVisitor
    public class SurfaceAreaVisitor: IVisitor
    {
        public double SurfaceArea { get; private set; }
        public void VisitBody(Body elem)
        {
            elem.Accept(this);
        }
    }

    public class DimensionsVisitor : IVisitor
    {
        public Dimensions Dimensions { get; private set; }
        public void VisitBody(Body elem)
        {
            DimensionsVisitor
            elem.Accept(this);
        }
    }
}
