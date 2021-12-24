using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Entities
{
    public class ApiReturn
    {
        #region Properties
        public bool Success { get; set; }
        public dynamic Data { get; set; }
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public string ErrorCode { get; set; }
        public string MoreInfo { get; set; }
        public string TraceId { get; set; }
        #endregion
    }
}
