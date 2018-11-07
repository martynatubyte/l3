using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Stud
{
    class Senior : Member
    {
        public string Workplace { get; set; }

        public Senior(string name, DateTime date, string number, string workplace) : base(name, date, number)
        {
            Workplace = workplace;
        }

        public override string ToString()
        {
            return String.Format("Name: {0, -20} Birthday: {1:yyyy-MM-dd} Number: {2, -10} Workplace: {3}",
                Name, Birthday, Number, Workplace);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Senior);  //kviečiame tipui specifinį metodą toje pačioje klasėje 
        }

        public bool Equals(Senior senior)
        {
            return base.Equals(senior); //kviečiame tėvinės klasės Animal Equals metodą
        }

        public override int GetHashCode()
        {
            return Birthday.GetHashCode() ^ Name.GetHashCode();
        }

        public static bool operator ==(Senior left, Senior right)
        {
            if (Object.ReferenceEquals(left, null))
            {
                if (Object.ReferenceEquals(right, null)) { return true; }

                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Senior left, Senior right)
        {
            return !(left == right);
        }
    }
}
