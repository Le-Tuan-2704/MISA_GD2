using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Interfaces.IServices
{
    public interface IEventService : IBaseService<EventCalendar>
    {
        /// <summary>
        /// Lấy tất cả bản ghi theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetByUserId(string userId);
        /// <summary>
        /// Lấy tất cả bản ghi theo employeeId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetByEmployeeId(string employeeId);

        /// <summary>
        /// Lấy những bản ghi đang trong trạng thái chờ theo nhóm
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetPending(string managerId);
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id cần xóa</param>
        /// <returns></returns>
        Task<ServiceResult> RemoveMultiple(IEnumerable<string> ids);
        /// <summary>
        /// Duyệt nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id cần duyệt</param>
        /// <param name="approverId"></param>
        /// <returns></returns>
        Task<ServiceResult> ApproveMultiple(IEnumerable<string> ids, string approverId);
        /// <summary>
        /// Hoàn thành 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> CompleteEvent(string id);
        /// <summary>
        /// Lấy danh sách sự kiện của employee theo khoảng thời gian
        /// </summary>
        /// <param name="employeeIdString"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<ServiceResult> GetOfEmployeeIdByRange(string employeeIdString, DateTime start, DateTime end);
    }
}
