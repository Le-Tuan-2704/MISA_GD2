using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Exceptions;
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
        private ServiceResult _serviceResult;
        private IAuthRepository _authRepository;
        public EventService(IEventRepository eventRepository, IAuthRepository authRepository) : base(eventRepository)
        {
            _authRepository = authRepository;
            _eventRepository = eventRepository;
            _serviceResult = new ServiceResult
            {
                SuccessState = true,
                MISACode = Enums.MISAEnum.Success
            };
        }

        #region Methods
        async public Task<ServiceResult> GetByUserId(string userId)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", userId))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(userId);

                //Lấy user từ DB
                var user = await _authRepository.GetEntityById(parsedId);

                if (user == null)
                {
                    _serviceResult.SuccessState = false;
                    _serviceResult.UserMsg = "";
                    _serviceResult.DevMsg = "";
                    return _serviceResult;
                }

                var employeeId = user.idUser;

                _serviceResult.SuccessState = true;
                //Lấy events từ db theo employeeId
                _serviceResult.Data = await _eventRepository.GetEntitiesByEmployeeId(employeeId);

                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        async public Task<ServiceResult> GetByEmployeeId(string userId)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", userId))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(userId);

                var user = await _eventRepository.GetEntitiesByEmployeeId(parsedId);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = user;

                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        public async Task<ServiceResult> GetOfEmployeeIdByRange(string employeeIdString, DateTime start, DateTime end)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", employeeIdString))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(employeeIdString);

                if (end <= start)
                {
                    _serviceResult.SuccessState = false;
                    _serviceResult.Data = "";
                    _serviceResult.DevMsg = Properties.Resources.MISA_ResponseMessage_WrongTimeOrder;
                    _serviceResult.UserMsg = _serviceResult.DevMsg;
                    return _serviceResult;
                }

                var user = await _eventRepository.GetEntitiesOfEmployeeByRange(parsedId, start, end);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = user;

                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        async public Task<ServiceResult> GetPending(string managerId)
        {
            try
            {

                //kiểm tra id là guid
                if (!CheckGuid($"managerId", managerId))
                {
                    return _serviceResult;
                }

                /*//Kiểm tra group đang lấy có thuộc thẩm quyền của user không
                var groups = await _groupRepository.GetOfManager(Guid.Parse(managerId));

                var groupFound = groups.FirstOrDefault(group => group.GroupId.ToString() == groupId);

                //Không có quyền
                if (groupFound == null)
                {
                    _serviceResult.SuccessState = false;
                    _serviceResult.DevMsg = "";
                    _serviceResult.UserMsg = "";
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(groupId);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _eventRepository.GetPendingEntitiesByGroupId(parsedId);
                */
                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        async public Task<ServiceResult> GetByManagerId(string managerId)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", managerId))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(managerId);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _eventRepository.GetEntitiesByManagerId(parsedId);

                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        async public Task<ServiceResult> RemoveMultiple(IEnumerable<string> ids)
        {
            try
            {

                foreach (string id in ids)
                {
                    //kiểm tra id là guid
                    if (!CheckGuid($"id", id))
                    {
                        return _serviceResult;
                    }
                }

                var idsString = string.Join(",", ids);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _eventRepository.DeleteEntitesByIds(idsString);


                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        public async Task<ServiceResult> ApproveMultiple(IEnumerable<string> ids, string approverId)
        {
            try
            {
                if (!CheckGuid("approverId", approverId))
                {
                    return _serviceResult;
                }

                foreach (string id in ids)
                {
                    //kiểm tra id là guid
                    if (!CheckGuid($"id", id))
                    {
                        return _serviceResult;
                    }
                }

                var parsedId = Guid.Parse(approverId);

                var idsString = string.Join(",", ids);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _eventRepository.ApproveEntitesByIds(idsString, parsedId);


                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        public async Task<ServiceResult> CompleteEvent(string id)
        {
            try
            {
                if (!CheckGuid("id", id))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(id);

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _eventRepository.CompleteById(parsedId);

                return _serviceResult;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    ex.Message,
                    UnexpectedErrorResponse(ex.Message)
                );
            }
        }

        async protected override Task<bool> CustomValidate(EventCalendar entity)
        {
            if (entity.end < entity.start)
            {
                _serviceResult.SuccessState = false;
                _serviceResult.Data = "";
                _serviceResult.DevMsg = Properties.Resources.MISA_ResponseMessage_WrongTimeOrder;
                _serviceResult.UserMsg = _serviceResult.DevMsg;
                return false;
            }
            return true;
        }

        protected async override Task<bool> ValidUpdate(EventCalendar entity, EventCalendar entityRole)
        {
            var isValid = true;
            if (!(entity.employeeId == entityRole.employeeId))
            {
                isValid = false;
            }
            if (entity.currentStatus != 0)
            {
                isValid = false;
            }
            return isValid;
        }
        #endregion
    }
}
