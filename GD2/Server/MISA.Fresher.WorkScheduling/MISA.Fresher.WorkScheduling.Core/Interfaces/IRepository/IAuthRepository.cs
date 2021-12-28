using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository
{
    public interface IAuthRepository
    {
        /// <summary>
        /// kiểm tra tên khách hàng đã có trong db hay chưa?
        /// </summary>
        /// <param name="tEntityId"> name</param>
        /// <returns>User</returns>
        public User CheckDuplicateName(string name);

        /// <summary>
        /// tạo user
        /// </summary>
        /// <param name="tEntityId"> name</param>
        /// <returns>false đã có, true chưa có</returns>
        public bool createUser(User user);

        /// <summary>
        /// lưu token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true lưu thành công | false lưu không thành công</returns>
        public bool saveToken(Token token);

        public Task<User> GetEntityById(Guid userid);
    }
}
