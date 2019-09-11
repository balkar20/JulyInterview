using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;

namespace Ddd.Infrastructure
{
	/// <summary>
	/// Базовый класс для всех Value типов.
	/// </summary>
	public class ValueType<T>
	{
	   

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if ((obj.GetType() != GetType())) return false;
	        return false;

	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            return 1;
	        }
	    }

	    public override string ToString()
	    {
	        return base.ToString();
	    }
	}
}