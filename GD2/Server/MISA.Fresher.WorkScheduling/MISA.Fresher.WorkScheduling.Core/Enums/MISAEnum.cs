using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Enums
{
    /// <summary>
    /// Mã code của hệ thống
    /// </summary>
    /// CREATEDBY: Lê Duy Tuân (20/12/2021)
    public enum MISAEnum
    {
        IsValid = 100,
        NotValid = 900,
        Success = 200,
        DBUnexpectedError = 998,
        UnexpectedError = 999
    }

    /// <summary>
    /// phân quyền
    /// </summary>
    /// CREATEDBY: Lê Duy Tuân (20/12/2021)
    public enum Role
    {
        EMPLOYEE = 0,
        MANAGER = 1
    }

    /// <summary>
    /// Trạng thái của entity
    /// </summary>
    /// CREATEDBY: Lê Duy Tuân (24/12/2021)
    public enum EntityState
    {
        GET = 0,
        ADD = 1,
        UPDATE = 2,
        REMOVE = 3
    }

    /// <summary>
    /// Trạng thái của event lịch
    /// </summary>
    /// CREATEDBY: Lê Duy Tuân (24/12/2021)
    public enum EventState
    {
        WAITING = 0,
        APPROVED = 1,
        COMPLETED = 2
    }
}
