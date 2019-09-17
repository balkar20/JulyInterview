using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Geometry
{
	public abstract class Body
	{
	    public abstract double GetVolume();
	}

	public class Ball : Body
	{
		public double Radius { get; set; }
	    public override double GetVolume()
	    {
	        return 4.0 * Math.PI * Radius * Radius * Radius / 3;
        }
	}

	public class Cube : Body
	{
		public double Size { get; set; }
	    public override double GetVolume()
	    {
	        return Size * Size * Size;
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
    }

	// Заготовка класса для задачи на Visitor
	public class SurfaceAreaVisitor
	{
		public double SurfaceArea { get; private set; }
	}

	public class DimensionsVisitor
	{
		public Dimensions Dimensions { get; private set; }
	}
}
