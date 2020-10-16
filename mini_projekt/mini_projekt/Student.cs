using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_projekt
{
    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
