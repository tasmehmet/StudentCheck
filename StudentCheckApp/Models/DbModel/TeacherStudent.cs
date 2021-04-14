using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCheckApp.Models.DbModel
{
    public class TeacherStudent
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string TeacherID { get; set; }
    }
}
