using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        /// <summary>
        /// Xác thực người dùng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult> Authenticate(string Username, string Password);

        /// <summary>
        /// tạo tài khoản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult> Registration(string Username, string Password);

        /// <summary>
        /// Lấy người dùng hiện tại theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult> GetUserById(string id);
    }
}
