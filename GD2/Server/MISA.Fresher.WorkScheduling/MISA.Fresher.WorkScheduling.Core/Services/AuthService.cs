using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Exceptions;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Core.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _authRepository;
        protected ServiceResult serviceResult;

        #region Constructor
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            serviceResult = new ServiceResult
            {
                SuccessState = true,
                MISACode = Enums.MISAEnum.Success
            };
        }

        #endregion

        async public Task<ServiceResult> Authenticate(string Username, string Password)
        {
            try
            {
                if (Username == "" || Username == null || Password == "" || Password == null)
                {
                    serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_UserNotFound;
                    serviceResult.DevMsg = serviceResult.UserMsg;
                    serviceResult.SuccessState = false;
                    return serviceResult;

                }

                User userLogin = _authRepository.CheckDuplicateName(Username);

                if (userLogin == null)
                {
                    serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_UserNotFound;
                    serviceResult.DevMsg = serviceResult.UserMsg;
                    serviceResult.SuccessState = false;
                    return serviceResult;
                }


                if (!BCryptVerify(Password, userLogin.password))
                {
                    serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_WrongPassword;
                    serviceResult.DevMsg = serviceResult.UserMsg;
                    serviceResult.SuccessState = false;
                    return serviceResult;
                }

                serviceResult.SuccessState = true;
                serviceResult.Data = userLogin;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.DevMsg = ex.ToString();
                serviceResult.SuccessState = false;
                return serviceResult;
            }
        }

        public async Task<ServiceResult> Registration(string Username, string Password)
        {
            if (Username == "" || Username == null || Password == "" || Password == null)
            {
                serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_UserNotFound;
                serviceResult.DevMsg = serviceResult.UserMsg;
                serviceResult.SuccessState = false;
                return serviceResult;

            }

            if (_authRepository.CheckDuplicateName(Username) == null)
            {
                var login = new User
                {
                    userName = Username,
                    password = Password
                };

                login.role = 1;
                login.password = BcryptHash(login.password);

                if (_authRepository.createUser(login))
                {
                    return serviceResult;
                }
                return serviceResult;
            }
            else
            {
                serviceResult.UserMsg = Properties.Resources.MISA_ResponseMessage_UserNotFound;
                serviceResult.DevMsg = serviceResult.UserMsg;
                serviceResult.SuccessState = false;
                return serviceResult;
            }
        }

        async public Task<ServiceResult> GetUserById(string id)
        {
            serviceResult.SuccessState = true;
            serviceResult.Data = await _authRepository.GetEntityById(Guid.Parse(id));
            serviceResult.MISACode = Enums.MISAEnum.Success;

            return serviceResult;
        }

        /// <summary>
        /// Băm theo SHA256
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Hashed string</returns>
        public static string Sha256Hash(string value)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// Hash theo BCrypt
        /// </summary>
        /// <param name="value"></param>
        /// <returns>String đã được hash</returns>
        public static string BcryptHash(string value)
        {
            return BCryptNet.HashPassword(value);
        }

        /// <summary>
        /// Kiểm tra bcrypt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns>true is ok| false not ok</returns>
        public static bool BCryptVerify(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }


    }
}
