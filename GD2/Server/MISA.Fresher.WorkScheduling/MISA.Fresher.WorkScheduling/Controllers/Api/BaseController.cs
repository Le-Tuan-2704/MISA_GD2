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
        public async Task<IActionResult> Get()
        {
            var serviceResult = await _baseService.GetAll();

            return Ok(serviceResult);

        }

        /// <summary>
        /// Lấy theo Id
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <returns></returns>
        [HttpGet("{tEntityId}")]
        public async Task<IActionResult> Get(Guid tEntityId)
        {
            var res = await _baseService.GetById(tEntityId);

            return Ok(res);
        }

        /// <summary>
        /// Tạo mới
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(TEntity tEntity)
        {
            var res = await _baseService.Insert(tEntity);

            return Ok(res);
        }

        /// <summary>
        /// Sửa thông tin
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <param name="tEntity"></param>
        /// <returns></returns>
        [HttpPut("{tEntityId}")]
        public async Task<IActionResult> Update(Guid tEntityId, [FromBody] TEntity tEntity)
        {
            var res = await _baseService.Update(tEntityId, tEntity);

            return Ok(res);

        }

        /// <summary>
        /// Xóa theo Id
        /// Lê Duy Tuân 22/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <returns></returns>
        [HttpDelete("{tEntityId}")]
        public async Task<IActionResult> Delete(string tEntityId)
        {

            var res = await _baseService.Delete(tEntityId);

            return Ok(res);
        }
        #endregion

    }
}
