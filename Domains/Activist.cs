using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Activist : Person
    {
        public int? superiorId { get; set; }

        public int ActivistTypeId { get; set; }
    }
}
