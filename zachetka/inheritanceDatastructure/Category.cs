using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        public Category(string name, MessageType messageType, MessageTopic messageTopic)
        {
            Name = name;
            MessageType = messageType;
            MessageTopic = messageTopic;
        }

        public string Name { get; set; }
        private MessageType MessageType { get; set; }
        private MessageTopic MessageTopic { get; set; }
            
        public int this[int a]
        {
            get
            {
                return 7;
            }
            set
            {
                 ;
            }
        }

        public static bool operator ==(Category category1, Category category2)
        {
            return true;
        }

        public static bool operator !=(Category category1, Category category2)
        {
            return true;
        }

        public static bool operator <(Category category1, Category category2)
        {
            return true;
            //r1._fix_denominator(r2);
            //return r1.Numerator < r2.Numerator;
        }
        public static bool operator >(Category category1, Category category2)
        {
            return true;
            //r1._fix_denominator(r2);
            //return r1.Numerator < r2.Numerator;
        }

        public static bool operator >=(Category category1, Category category2)
        {
            return true;
            //return r2 < r1;
        }
        public static bool operator <=(Category category1, Category category2)
        {
            return true;
            //return r2 < r1;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return (this == (Category)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 666;
            return 1;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
