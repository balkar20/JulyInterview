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

        public string Name { get; }
        private MessageType MessageType { get; }
        private MessageTopic MessageTopic { get; set; }

        public static bool operator ==(Category category1, Category category2)
        {
            bool result;
            try
            {
                result = category1.Name == category2.Name &&
                                   category1.MessageTopic == category2.MessageTopic &&
                                   category1.MessageType == category2.MessageType;
            }
            catch (NullReferenceException e)
            {
                result = false;
            }

            return result;
        }

        public static bool operator !=(Category category1, Category category2)
        {
            return category1 == category2 ? false : true;
        }

        public static bool operator <(Category category1, Category category2)
        {
            try
            {
                if (category1 != category2)
                {
                    return CheckForСomparisonMarks(category1, category2, СomparisonMark.Less);
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }
            
            return false;
        }
        public static bool operator >(Category category1, Category category2)
        {
            try
            {
                if (category1 != category2)
                {
                    return CheckForСomparisonMarks(category1, category2, СomparisonMark.More);
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }
           
            return false;
        }

        public static bool operator >=(Category category1, Category category2)
        {
            bool result = category1 == category2;
            try
            {
                result = CheckForСomparisonMarks(category1, category2, СomparisonMark.More) ? true : result;
            }
            catch (NullReferenceException e)
            {
                result = false;
            }

            return result;
        }
        public static bool operator <=(Category category1, Category category2)
        {
            bool result = category1 == category2;
            try
            {
                result = CheckForСomparisonMarks(category1, category2, СomparisonMark.Less)  ? true : result;
            }
            catch (NullReferenceException e)
            {
                result = false;
            }

            return result;
        }

        enum СomparisonMark
        {
            More,
            Less
        }

        static bool CheckForСomparisonMarks(Category category1, Category category2, СomparisonMark coMark)
        {
            int intNames = -category1.Name.CompareTo((category2).Name);
            int intMessageType = -
                category1.MessageType.ToString().
                    CompareTo((category2).
                        MessageType.ToString());
            int intMessageTopic = category1.MessageTopic.ToString().
                CompareTo((category2).
                    MessageTopic.ToString());
            if (coMark == СomparisonMark.Less)
            {
                return intNames > 0 ||
                       (intNames == 0 && intMessageType > 0) ||
                       (intNames == 0 && intMessageType == 0 && intMessageTopic > 0);
            }
            else 
            {
                return intNames < 0 ||
                       (intNames == 0 && intMessageType < 0) ||
                       (intNames == 0 && intMessageType == 0 && intMessageTopic < 0);
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (obj.GetType() == this.GetType())
                {
                    return this == (Category)obj;
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
                return Name.GetHashCode() |
                       (MessageType.ToString().GetHashCode() << this.MessageType.ToString().GetHashCode());
        }

        public override string ToString()
        {
            return $"{Name}.{MessageType}.{MessageTopic}";
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            if (this > (Category)obj)
            {
                result = 1;
            }
            else if (this < (Category)obj)
            {
                result = -1;
            }
            else if (this == (Category)obj)
            {
                result = 0;
            }

            return result;
        }
    }
}
