using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Exceptions;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        protected IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {

            _baseRepository = baseRepository;

            _serviceResult = new ServiceResult
            {
                SuccessState = true,
                MISACode = Enums.MISAEnum.Success
            };
        }

        public async Task<ServiceResult> Delete(string tEntityId)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", tEntityId))
                {
                    return _serviceResult;
                }

                var parsedId = Guid.Parse(tEntityId);

                //lấy enity từ repository
                var entity = await _baseRepository.GetById(parsedId);

                if (entity == null)
                {
                    _serviceResult.SuccessState = false;
                    _serviceResult.DevMsg = string.Format(Properties.Resources.MISA_ResponseMessage_RecordIdNotExists, tEntityId);
                    _serviceResult.UserMsg = string.Format(Properties.Resources.MISA_ResponseMessage_RecordIdNotExists, tEntityId);
                    _serviceResult.MISACode = Enums.MISAEnum.NotValid;
                }
                else
                {
                    _serviceResult.SuccessState = true;
                    _serviceResult.Data = await _baseRepository.Delete(parsedId.ToString());

                    if (int.Parse(_serviceResult.Data.ToString()) <= 0)
                    {
                        _serviceResult = RowAffectingUnexpectedFailureResponse();
                    }
                }
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

        public async Task<ServiceResult> GetAll()
        {
            try
            {
                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _baseRepository.GetAll();
                _serviceResult.MISACode = Enums.MISAEnum.Success;
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

        public async Task<ServiceResult> GetById(Guid tEntityId)
        {
            try
            {
                //Kiểm tra kiểu dữ liệu Guid
                if (CheckGuid($"id", tEntityId.ToString()))
                {
                    _serviceResult.SuccessState = true;
                    _serviceResult.Data = await _baseRepository.GetById(tEntityId);
                    _serviceResult.MISACode = Enums.MISAEnum.Success;
                }

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

        public async Task<ServiceResult> Insert(TEntity tEntity)
        {
            try
            {

                //validate entity
                var isValid = await Validate(tEntity);
                if (!isValid)
                {
                    return _serviceResult;
                }

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _baseRepository.Insert(tEntity);
                _serviceResult.MISACode = Enums.MISAEnum.IsValid;

                //Không tác động được bản ghi
                if (int.Parse(_serviceResult.Data.ToString()) <= 0)
                {
                    _serviceResult = RowAffectingUnexpectedFailureResponse();
                }

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

        public async Task<ServiceResult> Update(Guid tEntityId, TEntity tEntity)
        {
            try
            {
                //kiểm tra id là guid
                if (!CheckGuid($"id", tEntityId.ToString()))
                {
                    return _serviceResult;
                }

                //Lấy dữ liệu từ repository
                var en = await _baseRepository.GetById(tEntityId);

                if (en == null)
                {
                    _serviceResult.SuccessState = false;
                    _serviceResult.DevMsg = string.Format(Properties.Resources.MISA_ResponseMessage_RecordIdNotExists, tEntityId);
                    _serviceResult.UserMsg = string.Format(Properties.Resources.MISA_ResponseMessage_RecordIdNotExists, tEntityId);
                    _serviceResult.MISACode = Enums.MISAEnum.NotValid;
                    return _serviceResult;
                }

                //validate entity
                var isValid = await Validate(tEntity);

                if (!isValid)
                {
                    return _serviceResult;
                }

                _serviceResult.SuccessState = true;
                _serviceResult.Data = await _baseRepository.Update(tEntityId, tEntity);
                _serviceResult.MISACode = Enums.MISAEnum.IsValid;

                //Không tác động được bản ghi
                if (int.Parse(_serviceResult.Data.ToString()) <= 0)
                {
                    _serviceResult = RowAffectingUnexpectedFailureResponse();
                }

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


        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>true: hợp lệ | false: không hợp lệ</returns>
        private async Task<bool> Validate(TEntity entity)
        {
            var isValid = true;

            var properties = entity.GetType().GetProperties();



            return isValid && await CustomValidate(entity);
        }

        /// <summary>
        /// Hàm validate tùy chọn
        /// </summary>
        /// <returns>true | false</returns>
        protected async virtual Task<Boolean> CustomValidate(TEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Kiểm tra string là guid
        /// </summary>
        /// <param name="fieldName">tên trường</param>
        /// <param name="toCheck">string cần kiểm tra</param>
        /// <returns>true | false</returns>
        protected bool CheckGuid(string fieldName, string toCheck)
        {
            var check = Guid.TryParse(toCheck, out _);

            if (!check)
            {
                _serviceResult.SuccessState = false;
                _serviceResult.DevMsg = string.Format(Properties.Resources.MISA_ResponseMessage_NotUUID, fieldName);
                _serviceResult.UserMsg = string.Format(Properties.Resources.MISA_ResponseMessage_NotUUID, fieldName);
                _serviceResult.MISACode = Enums.MISAEnum.NotValid;
            }

            return check;
        }

        /// <summary>
        /// Kết quả của service khi gặp lỗi không xác định
        /// </summary>
        /// <param name="errorMessage">Message lỗi</param>
        /// <returns>Kết quả của service</returns>
        protected ServiceResult UnexpectedErrorResponse(string errorMessage)
        {
            _serviceResult.SuccessState = false;
            _serviceResult.DevMsg = errorMessage;
            _serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_Default;
            _serviceResult.MISACode = Enums.MISAEnum.DBUnexpectedError;
            return _serviceResult;
        }

        /// <summary>
        /// Kết quả của service khi không thể thay đổi bản ghi trong CSDL không rõ nguyên nhân
        /// </summary>
        /// <returns>Kết quả của service</returns>
        protected ServiceResult RowAffectingUnexpectedFailureResponse()
        {
            _serviceResult.SuccessState = false;
            _serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_RowAffectingUnexpectedFailure;
            _serviceResult.DevMsg = Properties.Resources.MISA_ResponseMessage_RowAffectingUnexpectedFailure;
            _serviceResult.MISACode = Enums.MISAEnum.UnexpectedError;
            return _serviceResult;
        }

    }
}
