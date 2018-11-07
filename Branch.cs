using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Stud
{
	class Branch
	{
		public const int MaxNumberOfMembers = 50;

		public int Year { get; set; }
		public MembersContainer Students { get; set; }
        public MembersContainer Seniors { get; set; }

		public Branch(int year)
		{
			Year = year;
			Students = new MembersContainer(MaxNumberOfMembers);
            Seniors = new MembersContainer(MaxNumberOfMembers);
		}

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
        public void AddSenior(Senior senior)
        {
            Seniors.Add(senior);
        }

    }
}
