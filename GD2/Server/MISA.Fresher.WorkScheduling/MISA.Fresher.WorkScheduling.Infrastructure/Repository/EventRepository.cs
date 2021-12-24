using Microsoft.Extensions.Configuration;
using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Infrastructure.Repository
{
    public class EventRepository : BaseRepository<EventCalendar>, IEventRepository
    {
        public EventRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
