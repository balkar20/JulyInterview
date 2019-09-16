using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{

    public class Rational
    {
        public Rational(int int_value)
        {
            set(int_value, 1);
        }

        public Rational(int numerator, int denominator)
        {
            set(numerator, denominator);
            _regularize();
        }

        public bool IsNan
        {
            get { return Denominator == 0 ? true: false; }
        }
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public static implicit operator double(Rational value)
        {
            return value.Denominator == 0 ? double.NaN : (double)value.Numerator / value.Denominator;
        }

        public static explicit operator int(Rational value)
        {
            if (value.Numerator == 0)
            {
                return 0;
            }
            if (value.Denominator == 0 ||
                value.Numerator % value.Denominator != 0)
            {
                throw new Exception();
            }
            return (int)value.Numerator / value.Denominator;
        }

        public static implicit operator Rational(int value)
        {
            return  new Rational(value, 1);
        }

        public void set(int new_numerator, int new_denominator)
        {
            Numerator = new_numerator;
            Denominator = new_denominator;
        }

        static int gcd(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (a != b)
                if (a < b) b = b - a;
                else a = a - b;
            return (a);
        }

        private void _regularize()
        {
            if (Denominator != 0 && Numerator != 0)
            {
                int divisor = Math.Sign(Denominator) * gcd(Numerator, Denominator);
                if (divisor == 0)
                {
                    Numerator = 0;
                    Denominator = 1;
                }
                else
                {
                    Numerator /= divisor;
                    Denominator /= divisor;
                }
            }
        }

        private void _fix_denominator(Rational other)
        {
            int tmp = Denominator;
            Numerator *= other.Denominator;
            Denominator *= other.Denominator;

            other.Numerator *= tmp;
            other.Denominator *= tmp;
        }

        public static bool operator ==(Rational r1, Rational r2)
        {
            r1._regularize();
            r2._regularize();
            return (r1.Numerator == r2.Numerator && r1.Denominator == r2.Denominator);
        }

        public static bool operator !=(Rational r1, Rational r2)
        {
            return (r1 != r2);
        }

        public static bool operator <(Rational r1, Rational r2)
        {
            r1._fix_denominator(r2);
            return (r1.Numerator < r2.Numerator);
        }

        public static bool operator >(Rational r1, Rational r2)
        {
            return (r2 < r1);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return (this == (Rational)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Numerator | (Denominator << 16));
        }

        public static Rational operator +(Rational r) 
        {
            return new Rational(r.Numerator, r.Denominator);
        }

        public static Rational operator -(Rational r) 
        {
            return new Rational(-r.Numerator, r.Denominator);
        }

        public static Rational operator +(Rational r1, Rational r2) 
        {
            r1._fix_denominator(r2);
            return new Rational(r1.Numerator + r2.Numerator, r1.Denominator);
        }

        public static Rational operator -(Rational r1, Rational r2) 
        {
            r1._fix_denominator(r2);
            return new Rational(r1.Numerator - r2.Numerator, r1.Denominator);
        }

        public static Rational operator *(Rational r1, Rational r2)
        {
            return new Rational(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);
        }

        public static Rational operator /(Rational r1, Rational r2)
        {
            if (r2.Numerator == 0 ||
                r1.Denominator == 0 ||
                r2.Denominator == 0)
            {
                return new Rational(r1.Numerator * r2.Denominator, 0);
            }
            return new Rational(r1.Numerator * r2.Denominator, r1.Denominator * r2.Numerator);
        }

        public override string ToString()
        {
            _regularize();
            if (Denominator == 1) return Numerator.ToString();

            return $"({Numerator}/{Denominator})";
        }
    }
}


