using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Entities
{
    public class EventCalendar
    {
        public Guid eventcalendarId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public Guid? employeeId { get; set; }
        public string employeeName { get; set; }
        public Guid? approverId { get; set; }
        public string approverName { get; set; }
        public Guid? groupId { get; set; }
        public int currentStatus { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
