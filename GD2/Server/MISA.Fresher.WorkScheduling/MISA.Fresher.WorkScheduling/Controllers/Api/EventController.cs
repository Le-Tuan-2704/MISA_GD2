using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Enums;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;
using MISA.Fresher.WorkScheduling.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : BaseController<EventCalendar>
    {
        protected ServiceResult _serviceResult;
        IEventService _eventService;
        public EventController(IEventService eventService) : base(eventService)
        {
            _serviceResult = new ServiceResult();
            _eventService = eventService;
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpGet("All")]
        [Authorize]
        async public Task<IActionResult> GetForUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[1].Value;

            var role = int.Parse(claim[2].Value);

            var res = new ServiceResult();

            if (role == 1)
            {
                res = await _eventService.GetByUserId(userId);
            }
            else if (role == 9)
            {
                res = await _eventService.GetAll();
            }


            if (res.SuccessState)
            {
                return Ok(res.Data);
            }
            else
            {
                return Unauthorized(res);
            }
        }

        public async override Task<IActionResult> Post(EventCalendar eventCalendar)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[1].Value;
            var userName = claim[0].Value;
            eventCalendar.employeeId = Guid.Parse(userId);
            eventCalendar.employeeName = userName;
            eventCalendar.currentStatus = 0;

            var res = await _baseService.Insert(eventCalendar);

            return Ok(res);
        }

        [HttpPut("{eventCalendarId}")]
        public async override Task<IActionResult> Update(Guid eventCalendarId, [FromBody] EventCalendar eventCalendar)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[1].Value;
            var newEventCalendar = new EventCalendar
            {
                employeeId = Guid.Parse(userId),
                content = eventCalendar.content,
                title = eventCalendar.title,
                start = eventCalendar.start,
                end = eventCalendar.end,
            };

            var res = await _baseService.Update(eventCalendarId, newEventCalendar);

            return Ok(res);

        }

        /// <summary>
        /// Lấy theo khoảng thời gian
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpGet("Range")]
        [Authorize]
        async public Task<IActionResult> GetByRangeForEmployee(DateTime start, DateTime end)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[1].Value;

            var res = await _eventService.GetOfEmployeeIdByRange(userId, start, end);

            if (res.SuccessState)
            {
                return Ok(res);
            }
            else
            {
                return Unauthorized(res);
            }
        }

        /// <summary>
        /// Lấy danh sách chờ phê duyệt theo group
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpGet("Group/{id}")]
        [Authorize]
        async public Task<IActionResult> GetPendingOfGroup([FromRoute] string id)
        {
            Role userRole = (Role)Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "0");
            var employeeId = HttpContext.User.FindFirst("employeeId").Value;

            if (userRole != Role.MANAGER)
            {
                return Unauthorized();
            }

            var res = await _eventService.GetPending(employeeId);

            if (!res.SuccessState)
            {
                return Unauthorized(res);
            }

            return Ok(res);
        }


        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpDelete("MultipleRemoval")]
        [Authorize]
        async public Task<IActionResult> RemoveMultiple([FromBody] MultipleAffectRequest body)
        {
            Role userRole = (Role)Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "0");
            var employeeId = HttpContext.User.FindFirst("employeeId").Value;

            //TODO: Kiểm tra event đang lấy có thuộc thẩm quyền của user không

            var res = await _eventService.RemoveMultiple(body.Ids);

            if (res.SuccessState)
            {
                return Ok(res);
            }
            else
            {
                return Unauthorized(res);
            }
        }

        /// <summary>
        /// Duyệt nhiều
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpPut("MultipleApproval")]
        [Authorize]
        async public Task<IActionResult> ApproveMultiple([FromBody] MultipleAffectRequest body)
        {
            Role userRole = (Role)Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "0");
            var employeeId = HttpContext.User.FindFirst("employeeId").Value;

            if (userRole != Role.MANAGER)
            {
                return Unauthorized();
            }

            //TODO: Kiểm tra event đang lấy có thuộc thẩm quyền của user không

            var res = await _eventService.ApproveMultiple(body.Ids, employeeId);

            if (res.SuccessState)
            {
                return Ok(res);
            }
            else
            {
                return Unauthorized(res);
            }
        }

        /// <summary>
        /// Duyệt nhiều
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        [HttpPut("Completion/{id}")]
        [Authorize]
        async public Task<IActionResult> CompleteEvent([FromRoute] string id)
        {
            var employeeId = HttpContext.User.FindFirst("employeeId").Value;

            //TODO: Kiểm tra event đang lấy có thuộc thẩm quyền của user không

            var res = await _eventService.CompleteEvent(id);

            if (res.SuccessState)
            {
                return Ok(res);
            }
            else
            {
                return Unauthorized(res);
            }
        }
    }
}
