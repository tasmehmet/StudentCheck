using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCheckApp.Models.DbModel
{
    public class Students
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public ICollection<CheckDay> CheckDays { get; set; }
        public ICollection<Homeworks> Homeworks { get; set; }
    }
}
