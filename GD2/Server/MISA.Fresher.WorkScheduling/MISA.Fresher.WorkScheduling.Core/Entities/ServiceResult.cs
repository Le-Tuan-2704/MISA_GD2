using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Entities
{
    public class ServiceResult
    {
        #region Properties
        public bool SuccessState { get; set; }
        public object Data { get; set; }
        public string UserMsg { get; set; }
        public string DevMsg { get; set; }
        public Enums.MISAEnum MISACode { get; set; }
        #endregion
    }
}
