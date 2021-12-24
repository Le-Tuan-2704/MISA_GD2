﻿using MISA.Fresher.WorkScheduling.Core.Entities;
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
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        protected IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {

            _baseRepository = baseRepository;

            _serviceResult = new ServiceResult();
        }

        public ServiceResult Delete(string tEntityId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAll()
        {
            try
            {
                _serviceResult.SuccessState = true;
                _serviceResult.Data = _baseRepository.GetAll();
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

        public Task<ServiceResult> GetById(Guid tEntityId)
        {
            try
            {
                //Kiểm tra kiểu dữ liệu Guid
                if (CheckGuid($"id", tEntityId.ToString()))
                {
                    _serviceResult.SuccessState = true;
                    _serviceResult.Data = _baseRepository.GetById(tEntityId);
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

        public ServiceResult Insert(TEntity tEntity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(Guid tEntityId, TEntity tEntity)
        {
            throw new NotImplementedException();
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
    }
}