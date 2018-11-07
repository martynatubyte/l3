using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Stud
{
	class MembersContainer
	{
		private Member[] Members;
		public int Count { get; private set; }

		public MembersContainer(int size)
		{
			Members = new Member[size];
			Count = 0;
		}
		public void Add(Member member)
		{
			Members[Count++] = member;
		}
		public void Set(Member member, int index)
		{
			Members[index] = member;
		}
		public Member GetMember(int index)
		{
			return Members[index];
		}

		public bool Contains(Member stud)
		{
            return Members.Contains(stud);
		}

        public void SortMembers()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                Member minMember = Members[i];
                int index = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (Members[j] <= minMember)
                    {
                        minMember = Members[j];
                        index = j;
                    }
                    Members[index] = Members[i];
                    Members[i] = minMember;
                }
            }
        }
	}
}
