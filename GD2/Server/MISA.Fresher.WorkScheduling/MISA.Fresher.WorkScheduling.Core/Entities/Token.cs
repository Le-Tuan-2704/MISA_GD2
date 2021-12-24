using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Entities
{
    public class Token
    {
        public Guid idToken { get; set; }
        public string token { get; set; }
        public Guid idUser { get; set; }

        public Token(string tok, Guid idUs)
        {
            idToken = Guid.NewGuid();
            token = tok;
            idUser = idUs;
        }
    }
}
