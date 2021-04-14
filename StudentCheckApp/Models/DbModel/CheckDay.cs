using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCheckApp.Models.DbModel
{
    public class CheckDay
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public Students Students { get; set; }

    }
}
