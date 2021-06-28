using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public partial   class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public   string  CalculateAge( )
        {

            if (DateOfBirth != null)
            {
                int age = 0;

                age = DateTime.Now.Year - DateOfBirth.Year;

                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age = age - 1;

                return age.ToString();
            }
            else
            {
                return null;
            }
          
           
        }
    }
}
