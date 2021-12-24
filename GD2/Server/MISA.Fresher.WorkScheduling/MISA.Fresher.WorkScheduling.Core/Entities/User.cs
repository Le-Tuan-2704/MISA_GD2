using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Entities
{
    public class User
    {
        public Guid idUser { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int? role { get; set; }
    }
}
