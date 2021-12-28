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
using MISA.Fresher.WorkScheduling.Core.Interfaces.IServices;

namespace MISA.Fresher.WorkScheduling.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IAuthService _authService;
        private IAuthRepository _authRepository;

        public AuthController(IAuthService authService, IConfiguration config, IAuthRepository authRepository)
        {
            _authService = authService;
            _config = config;
            _authRepository = authRepository;
        }


        [AllowAnonymous]
        [HttpGet("login")]
        async public Task<IActionResult> Login(string userName, string password)
        {
            //Gọi service xác thực tài khoản
            var response = await _authService.Authenticate(userName, password);

            //Tài khoản hợp lệ
            if (response.SuccessState)
            {
                var user = response.Data as User;

                //Tạo token mới từ user nhận được
                var tokenString = GenerteJSONWebToken(user);

                var token = new Token(tokenString, user.idUser);

                var addRes = _authRepository.saveToken(token);

                if (addRes)
                {
                    response.Data = new
                    {
                        user = user,
                        accessToken = tokenString,
                    };
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            //Tài khoản không hợp lệ
            else
            {
                return Ok(response);
            }
        }

        private string GenerteJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {

                new Claim(JwtRegisteredClaimNames.Sub, (userInfo?.userName)??""),
                new Claim("id", (userInfo?.idUser.ToString())??""),
                new Claim(ClaimTypes.Role, ((int) (userInfo?.role ?? (int)Core.Enums.Role.EMPLOYEE)).ToString()),
                new Claim("role", ((int) (userInfo?.role ?? (int)Core.Enums.Role.EMPLOYEE)).ToString()),
                new Claim("date", (DateTime.Now.ToString())),
                new Claim(JwtRegisteredClaimNames.Jti, (Guid.NewGuid().ToString()))
            };


            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddDays(2),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("registration")]
        public async Task<IActionResult> Registration(string userName, string password)
        {

            var response = await _authService.Registration(userName, password);

            if (response.SuccessState)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Lấy ra user tương ứng với access token hiện tại
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        async public Task<IActionResult> Get()
        {
            var res = await _authService.GetUserById(HttpContext.User.FindFirstValue("id"));

            if (!res.SuccessState)
            {
                return Unauthorized(res);
            }

            return Ok(res);
        }

        /*
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
        */

    }
}
