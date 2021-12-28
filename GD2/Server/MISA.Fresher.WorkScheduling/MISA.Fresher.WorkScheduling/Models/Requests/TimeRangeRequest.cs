using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Models.Requests
{
    /// <summary>
    /// Request body khi cần gửi khoảng thời gian
    /// </summary>
    public class TimeRangeRequest
    {
        public DateTime start;
        public DateTime end;
    }
}
