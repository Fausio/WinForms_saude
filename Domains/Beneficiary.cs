using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Beneficiary : Person
    {
        public int ActivistId   { get; set; }

        public virtual Activist Activist { get; set; }
    }
}
