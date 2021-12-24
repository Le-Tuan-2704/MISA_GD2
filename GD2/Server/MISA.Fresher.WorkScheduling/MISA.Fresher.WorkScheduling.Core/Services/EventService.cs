using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Services
{
    public class EventService : BaseService<EventCalendar>, IEventService
    {
        IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository) : base(eventRepository)
        {
            _eventRepository = eventRepository;
        }
    }
}
