using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Course
    {
        public int ID {  get; set; }
        public string Name { get; set; }

        public List<Module> Modules { get; set; }
        
    }
}