using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCheckApp.Models.DbModel
{
    public class Homeworks
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public DateTime CreateTime { get; set; }
        public Students Students { get; set; }
    }
}
