using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Assignment
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int Grade { get; set; }

        public DateTime DueDate { get; set; }
        
    }
}