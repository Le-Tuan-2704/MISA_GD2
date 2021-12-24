using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.Fresher.WorkScheduling.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text;
using System.Threading.Tasks;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;

namespace MISA.Fresher.WorkScheduling.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private IAuthRepository _authRepository;

        public AuthController(IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
        }



        [HttpGet("login")]
        public IActionResult Login(string userName, string password)
        {
            User login = new User();
            login.userName = userName;
            login.password = password;

            if (login.userName == "" || login.userName == null || login.password == "" || login.password == null)
            {
                return Ok("các trường không được phép null");

            }

            User userLogin = _authRepository.CheckDuplicateName(userName);

            if (userLogin == null)
            {
                return Ok("userName không tồn tại");
            }


            if (!BCryptVerify(login.password, userLogin.password))
            {
                return Ok("password không đúng");
            }

            IActionResult response = Unauthorized();

            var tokenStr = GenerteJSONWebToken(userLogin);
            var token = new Token(tokenStr, userLogin.idUser);
            if (_authRepository.saveToken(token))
            {
                response = Ok(new { token = tokenStr });
            }
            else
            {
                response = Ok("error save token");
            }
            return response;
        }

        private string GenerteJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.idUser.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);
            var encodetoke = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoke;
        }


        [HttpGet("registration")]
        public IActionResult Registration(string userName, string password)
        {
            User login = new User();
            login.idUser = Guid.NewGuid();
            login.userName = userName;
            login.password = password;

            if (login.userName == "" || login.userName == null || login.password == "" || login.password == null)
            {
                return Ok("các trường không được phép null");

            }

            if (_authRepository.CheckDuplicateName(userName) == null)
            {
                login.role = 1;
                login.password = BcryptHash(login.password);

                if (_authRepository.createUser(login))
                {
                    return Ok(login);
                }
                return Ok("error");
            }
            else
            {
                return Ok("userName đã tồn tại");
            }


        }


        [Authorize]
        [HttpPost("post")]
        public IActionResult User([FromBody] User test)
        {
            if (test.userName == "" || test.userName == null || test.password == "" || test.password == null)
            {
                return BadRequest(test);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            test.password = Sha256Hash(test.password);
            return Ok(claim[0].Value + " " + claim[1].Value);
        }

        [Authorize]
        [HttpGet("get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
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
