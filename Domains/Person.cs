using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public partial   class Person
    {
        protected string Name { get; set; }
        protected string Gender { get; set; }
        protected DateTime DateOfBirth { get; set; }

        protected static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0; 
            age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
