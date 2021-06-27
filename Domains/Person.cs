using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public abstract class Person
    {
        protected string Name { get; set; }
        protected string Gender { get; set; }
        protected DateTime DateOfBirth { get; set; }

        protected string age()
        {
            // return the age
            return null;
        }
    }
}
