using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Exceptions
{

    /// <summary>
    /// Exception cho service
    /// </summary>
    /// CREATEDBY: Lê Duy Tuân (24/12/2021)
    class ServiceException : Exception
    {
        #region Properties
        public ServiceResult ServiceResult { get; }
        #endregion

        #region Constructors
        public ServiceException(string msg, ServiceResult res) : base(msg)
        {
            ServiceResult = res;
        }
        #endregion
    }
}
