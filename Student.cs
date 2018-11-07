using System;

namespace Lab1.Stud
{
	class Student : Member
    {
        
        public string CardId { get; set; }
        public int Course { get; set; }
		public bool Fux { get; set; }
		
        public Student(string name, DateTime birthday, string number, string id, int course, bool fux) : base(name, birthday, number)
        {
            CardId = id;
            Course = course;
            Fux = fux;
        }

        public override string ToString()
        {
            return String.Format("Name: {0, -20} Birthday: {1:yyyy-MM-dd} Number: {2, -10} Card Id: {3, 7}, Course: {4}, Fux: {5}",
                Name, Birthday, Number, CardId, Course, Fux ? "yes" : "no");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Student);  //kviečiame tipui specifinį metodą toje pačioje klasėje 
        }

        public bool Equals(Student student)
        {
            return base.Equals(student); //kviečiame tėvinės klasės Animal Equals metodą
        }

        public override int GetHashCode()
        {
            return Birthday.GetHashCode() ^ Name.GetHashCode();
        }

        public static bool operator ==(Student left, Student right)
        {
            if (Object.ReferenceEquals(left, null))
            {
                if (Object.ReferenceEquals(right, null)) { return true; }

                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }
    }
}