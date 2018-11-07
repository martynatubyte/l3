using System;
using System.Linq;
using System.IO;

namespace Lab1.Stud
{
	class Program
    {
		const int MetuSkaicius = 3;
        static void Main(string[] args)
        {
            Program p = new Program();
			Branch[] branches = new Branch[MetuSkaicius];
            branches[0] = new Branch(2016);
			branches[1] = new Branch(2017);
            branches[2] = new Branch(2018);

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "data*.csv");
            foreach (string path in filePaths)
            {
                bool rado = p.ReadMembersData(path, branches);
                if (rado == false)
                    Console.WriteLine("Year in file {0} was not recognised", path);
            }

            DateTime maxAge = branches.Min(b => p.FindMaxAge(b));
			var oldestMembers = new MembersContainer(Branch.MaxNumberOfMembers);
			oldestMembers = p.FindOldestMembers(branches, maxAge);
			p.OldestMembersToConsole(oldestMembers);

            MembersContainer members = p.FindSeniorsOver(branches, 30);
            Console.WriteLine("Seniors over {0} years:", 30);
            p.PrintMembersToConsole(members);

            members = p.FindSeniorsWorkingIn(branches, "ATEA");
            members.SortMembers();
            p.PrintToFile(members, @"Atea.csv", false);

            members = p.FindWasntStudents(branches);
            p.PrintToFile(members, @"Buvo.csv", false);

            Console.ReadKey();
		}
		/// <summary>
		/// Nuskaito studentu duomenis is failo
		/// </summary>
		/// <returns></returns>
		public bool ReadMembersData(string file, Branch[] branches)
        {
            string[] lines = File.ReadAllLines(file);
			int year = int.Parse(lines[0]);
			bool firstLine = true;

            Branch branch = GetBranchByYear(branches, year);
            if (branch == null) //nerado metų
                return false;

            foreach(string line in lines)
            {
				if (firstLine)
				{
					firstLine = false;
					continue;
				}
                string[] values = line.Split(';');
                string type = values[0];
                string name = values[1];
                DateTime birthday = DateTime.Parse(values[2]);
                string number = values[3];
                switch (type)
                {
                    case "Student":
                        string cardId = values[4];
                        int course = int.Parse(values[5]);
                        bool fux = bool.Parse(values[6]);
                        Student student = new Student(name, birthday, number, cardId, course, fux);
                        if (!branch.Students.Contains(student))
                        {
                            branch.AddStudent(student);
                        }
                        break;
                    case "Senior":
                        string workplace = values[4];
                        Senior senior = new Senior(name, birthday, number, workplace);
                        if (!branch.Seniors.Contains(senior))
                        {
                            branch.AddSenior(senior);
                        }
                        break;
                }				
            }
            return true;
        }

        /// <summary>
        /// Randa branchą pagal nurodytus metus
        /// </summary>
        /// <param name="branches"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public Branch GetBranchByYear(Branch[] branches, int year)
        {
            foreach (Branch br in branches)
                if (br.Year == year)
                    return br;
            return null;
        }

        /// <summary>
        /// Metodas, kuris suranda vyriausia studenta
        /// </summary>
        /// <param name="students"></param>
        /// <returns>maxAge</returns>
        DateTime FindMaxAge(Branch branch)
        {
            DateTime maxAge = DateTime.MaxValue;
			for (int i = 0; i < branch.Students.Count; i++)
			{
				if (branch.Students.GetMember(i).Birthday < maxAge)
                {
                    maxAge = branch.Students.GetMember(i).Birthday;
                }
			}
            for (int i = 0; i < branch.Seniors.Count; i++)
            {
                if (branch.Seniors.GetMember(i).Birthday < maxAge)
                {
                    maxAge = branch.Seniors.GetMember(i).Birthday;
                }
            }
            return maxAge;
        }
        /// <summary>
        /// Metodas, kuris randa nurodyta data gimusius narius
        /// </summary>
        /// <param name="students"></param>
        /// <param name="maxAge"></param>
        MembersContainer FindOldestMembers(Branch[] branches, DateTime maxAge)
        {
            var OldestStudents = new MembersContainer(Branch.MaxNumberOfMembers);
			foreach (Branch branch in branches)
			{
				for (int i = 0; i < branch.Students.Count; i++)
				{
					if(branch.Students.GetMember(i).Birthday == maxAge)
					{
						OldestStudents.Add(branch.Students.GetMember(i));
					}
				}
                for (int i = 0; i < branch.Seniors.Count; i++)
				{
                    if (branch.Seniors.GetMember(i).Birthday == maxAge)
                    {
                        OldestStudents.Add(branch.Seniors.GetMember(i));
                    }
                }
            }
            return OldestStudents;
        }
        /// <summary>
        /// Metodas, kuris isveda vyriausius studentus i console
        /// </summary>
        /// <param name="OldestStudents"></param>
        void OldestMembersToConsole(MembersContainer students)
        {
            Console.WriteLine("Vyriausi atstovybes nariai:");
			for (int i = 0; i < students.Count; i++)
			{
				Console.WriteLine("Vardas, Pavarde: {0}\nAmzius: {1}\n", students.GetMember(i).Name, CalculateAge(students.GetMember(i).Birthday));
			}
        }

		static int CalculateAge(DateTime date)
		{
			int age = DateTime.Today.Year - date.Year;
			if (DateTime.Today.Month < date.Month || (DateTime.Today.Month == date.Month && DateTime.Today.Day < date.Day))
				age--;
			return age;
		}

        /// <summary>
        /// Randa "Senius", vyresnius nei nurodytas amzius
        /// </summary>
        /// <param name="branches"></param>
        /// <param name="age"></param>
        /// <returns>rastų "senių" konteineris</returns>
        public MembersContainer FindSeniorsOver(Branch[] branches, int age)
        {
            MembersContainer seniors = new MembersContainer(Branch.MaxNumberOfMembers);
            foreach (Branch branch in branches)
            {
                for (int i = 0; i < branch.Seniors.Count; i++)
                {
                    Member senior = branch.Seniors.GetMember(i);
                    if (CalculateAge(senior.Birthday) > age && !seniors.Contains(senior))
                        seniors.Add(senior);
                }
            }
            return seniors;
        }

        /// <summary>
        /// Randa "senius", dirbancius, nurodytoje darbovieteje
        /// </summary>
        /// <param name="branches"></param>
        /// <param name="workplace"></param>
        /// <returns></returns>
        public MembersContainer FindSeniorsWorkingIn(Branch[] branches, string workplace)
        {
            MembersContainer seniors = new MembersContainer(Branch.MaxNumberOfMembers);
            foreach (Branch branch in branches)
            {
                for (int i = 0; i < branch.Seniors.Count; i++)
                {
                    Senior senior = (Senior)branch.Seniors.GetMember(i);
                    if (senior.Workplace == workplace && !seniors.Contains(senior))
                        seniors.Add(senior);
                }
            }
            return seniors;
        }

        /// <summary>
        /// Randa studentus, kurie priklause atstovybei
        /// </summary>
        /// <param name="branches"></param>
        /// <returns></returns>
        public MembersContainer FindWasntStudents(Branch[] branches)
        {
            MembersContainer students = new MembersContainer(Branch.MaxNumberOfMembers);
            foreach (Branch branch in branches)
            {
                for (int i = 0; i < branch.Students.Count; i++)
                {
                    if (!students.Contains(branch.Students.GetMember(i)))
                        students.Add(branch.Students.GetMember(i));
                }
            }
            return students;
        }

        public void PrintMembersToConsole(MembersContainer members)
        {
            for (int i = 0; i < members.Count; i++)
                Console.WriteLine(members.GetMember(i));
        }
		
		/// <summary>
		/// spausdiną į nurodytą failą
		/// </summary>
		/// <param name="students">studentų konteineris</param>
		/// <param name="filename">failo vardas</param>
		public void PrintToFile(MembersContainer members, string filename, bool continueToFile)
		{
			StreamWriter sw = new StreamWriter(filename, continueToFile);
			for (int i = 0; i < members.Count; i++)
            {
                sw.WriteLine(members.GetMember(i));
            }
			sw.Close();
		}
	}
}
