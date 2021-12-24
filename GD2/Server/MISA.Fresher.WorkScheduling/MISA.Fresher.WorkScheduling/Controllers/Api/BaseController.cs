using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Enums;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Fresher.WorkScheduling.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        #region properties
        protected IBaseService<TEntity> _baseService;
        protected ServiceResult _service;
        #endregion

        #region constructor
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
            _service = new ServiceResult();
        }
        #endregion

        #region method
        /// <summary>
        /// lấy tất cả
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> Get()
        {
            var serviceResult = _baseService.GetAll();

            return Ok(serviceResult);

        }

        /// <summary>
        /// Lấy theo Id
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <returns></returns>
        [HttpGet("{tEntityId}")]
        public IActionResult Get(Guid tEntityId)
        {
            try
            {
                var serviceResult = _baseService.GetById(tEntityId);
                return StatusCode(200, serviceResult);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Tạo mới
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(TEntity tEntity)
        {
            try
            {
                var serviceResult = _baseService.Insert(tEntity);
                if (serviceResult.SuccessState)
                {
                    return StatusCode(201, serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <param name="tEntity"></param>
        /// <returns></returns>
        [HttpPut("{tEntityId}")]
        public IActionResult Update(Guid tEntityId, [FromBody] TEntity tEntity)
        {
            try
            {
                var serviceResult = _baseService.Update(tEntityId, tEntity);
                if (serviceResult.SuccessState)
                {
                    return StatusCode(201, serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa theo Id
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <returns></returns>
        [HttpDelete("{tEntityId}")]
        public IActionResult Delete(string tEntityId)
        {
            try
            {
                var serviceResult = _baseService.Delete(tEntityId);
                if (serviceResult.SuccessState)
                {
                    return StatusCode(201, serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }

            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// thông tin trả ra khi có lỗi
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected ObjectResult HandleException(Exception ex)
        {
            _service.DevMsg = ex.Message;
            _service.UserMsg = "";
            _service.SuccessState = false;
            return StatusCode(404, _service);
        }
        #endregion
    }
}
