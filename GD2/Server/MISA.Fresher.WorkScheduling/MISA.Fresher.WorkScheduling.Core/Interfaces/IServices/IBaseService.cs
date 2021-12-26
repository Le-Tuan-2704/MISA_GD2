using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Interfaces.IServices
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// Lê Duy Tuân 20/12/2021
        /// </summary>
        /// <returns></returns>
        public Task<ServiceResult> GetAll();
        /// <summary>
        /// Lấy thông tin dữ liệu
        /// Lê Duy Tuân 20/12/2021
        /// </summary>
        /// <returns> khách hàng theo id</returns>
        public Task<ServiceResult> GetById(Guid tEntityId);
        /// <summary>
        /// tạo mới 
        /// Lê Duy Tuân 20/12/2021
        /// </summary>
        /// <param name="tEntity"></param>
        /// <returns> số bản ghi ảnh hưởng</returns>
        public Task<ServiceResult> Insert(TEntity tEntity);
        /// <summary>
        /// sửa thông tin 
        /// Lê Duy Tuân 20/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <param name="tEntity"></param>
        /// <returns> số bản ghi ảnh hưởng</returns>
        public Task<ServiceResult> Update(Guid tEntityId, TEntity tEntity);
        /// <summary>
        /// xóa theo id
        /// Lê Duy Tuân 20/12/2021
        /// </summary>
        /// <param name="tEntityId"></param>
        /// <returns> số bản ghi ảnh hưởng</returns>
        public Task<ServiceResult> Delete(string tEntityId);
    }
}
