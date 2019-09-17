using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Geometry
{
    public interface IVisitor
    {
        void Visit(Ball elem);
        void Visit(Cube elem);
        void Visit(Cyllinder elem);
    }
    public abstract class Body
    {
        public abstract void Accept(IVisitor visitor);
        public abstract double GetVolume();
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
            visitor.Visit(this);
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
            visitor.Visit(this);
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
             
            visitor.Visit(this);
        }
    }

    // Заготовка класса для задачи на IVisitor
    public class SurfaceAreaVisitor: IVisitor
    {
        public double SurfaceArea { get; private set; }

        public void Visit(Ball elem)
        {
            SurfaceArea = elem.Radius * elem.Radius * elem.Radius * Math.PI * 2;
        }

        public void Visit(Cube elem)
        {
            SurfaceArea = elem.Size * elem.Size * elem.Size * 2;
        }

        public void Visit(Cyllinder elem)
        {
            SurfaceArea = 2 * Math.PI * elem.Radius * elem.Radius + 2 * Math.PI * elem.Radius * elem.Height;
        }
    }

    public class DimensionsVisitor : IVisitor
    {

        public Dimensions Dimensions { get; private set; }

        public void Visit(Ball elem)
        {
            Dimensions = new Dimensions((double)elem.Radius * 2, (double)elem.Radius * 2);
        }

        public void Visit(Cube elem)
        {
            Dimensions = new Dimensions((double)elem.Size, (double)elem.Size);
        }

        public void Visit(Cyllinder elem)
        {
            Dimensions = new Dimensions((double)elem.Radius * 2, (double)elem.Height);
        }
    }
}
