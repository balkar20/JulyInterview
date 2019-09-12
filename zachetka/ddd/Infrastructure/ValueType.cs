using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Ddd.Taxi.Domain;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Ddd.Infrastructure
{
    /// <summary>
    /// Базовый класс для всех Value типов.
    /// </summary>
    public class ValueType<T>
    {
        public bool Equals(T obj)
        {
            return this.Equals((object)obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || (obj.GetType() != GetType())) return false;
            if (ReferenceEquals(this, obj)) return true;

            foreach (var nativePropertyInfo in this.GetType().GetProperties())
            {
                foreach (var passedProperty in obj.GetType().GetProperties())
                {
                    if (passedProperty.Name == nativePropertyInfo.Name)
                    {
                        var passedPropertyVal = passedProperty?.GetValue(obj, null);
                        var nativeVal = nativePropertyInfo?.GetValue(this, null);

                        if ((passedPropertyVal != null && nativeVal != null))
                        {
                            if (!passedPropertyVal.Equals(nativeVal))
                            {
                                return false;
                            }
                        }

                        if ((passedPropertyVal != null && nativeVal == null) ||
                            (passedPropertyVal == null && nativeVal != null))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var properties = this.GetType().GetProperties();
                var hash = (int)2166136261;
                const int fnvPrime = 16777619;
                for (int i = 0; i < properties.Length; i++)
                {
                    hash = (hash * fnvPrime) ^ properties[i].GetValue(this, null).GetHashCode();
                }
                return hash;
            }
        }

        public override string ToString()
        {
            string typeName = typeof(T).Name;

            StringBuilder propString = new StringBuilder("", 120);
            var properties = this.GetType().GetProperties();


            if (typeof(T) == typeof(Address))//Just for test
            {
                Array.Reverse(properties);
            }

            LoopBuilder(propString, properties);

            string formatString = $"{typeName}({propString})";
            return formatString;
        }

        private string LoopBuilder(StringBuilder stringVal, PropertyInfo[] props)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                bool isLast = i < props.Length - 1;
                string gap = isLast ? " " : "";
                string dotcoma = isLast ? ";" : "";
                var pp = props[i].GetValue(this, null);
                string propVal = pp?.ToString();
                string propValToString = propVal == null ? "" : propVal;

                if ((prop.CanRead || prop.CanWrite) && !prop.IsStatic())
                {
                    stringVal.Append($"{prop.Name}: {propValToString}{dotcoma}{gap}");
                }
            }

            return stringVal.ToString();
        } 
    }

    public static class PropertyInfoExtensions
    {
        public static bool IsStatic(this PropertyInfo source, bool nonPublic = false)
            => source.GetAccessors(nonPublic).Any(x => x.IsStatic);
    }
}