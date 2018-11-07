using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Stud
{
    abstract class Member
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Number { get; set; }

        public Member (string name, DateTime birthday, string number)
        {
            Name = name;
            Birthday = birthday;
            Number = number;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Member);
        }

        public bool Equals(Member x)
        {
            if (ReferenceEquals(x, null))
            {
                return false;
            }
            if (GetType() != x.GetType())
            {
                return false;
            }
            return (Birthday == x.Birthday) && (Name == x.Name);
        }

        public override int GetHashCode()
        {
            return Birthday.GetHashCode() ^ Name.GetHashCode();
        }

        public static bool operator ==(Member left, Member right)
        {
            if (Object.ReferenceEquals(left, null))
            {
                if (Object.ReferenceEquals(right, null)) { return true; }

                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !(left == right);
        }

        public static bool operator <=(Member left, Member right)
        {
            return (left.Name.CompareTo(right.Name) < 0);
        }

        public static bool operator >=(Member left, Member right)
        {
            return (left.Name.CompareTo(right.Name) > 0);
        }
    }
}
