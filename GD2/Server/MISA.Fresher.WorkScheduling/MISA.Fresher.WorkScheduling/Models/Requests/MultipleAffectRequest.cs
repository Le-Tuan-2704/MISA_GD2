using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Models.Requests
{
    public class MultipleAffectRequest
    {
        public IEnumerable<string> Ids { get; set; }
    }
}
