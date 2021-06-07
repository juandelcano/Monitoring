using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMonitoring.Dapper_ORM;
using WebMonitoring.Models;
using WebMonitoring.ViewModel;

namespace WebMonitoring.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDapper dapper;
        public IConfiguration _configuration;

        public LoginController(IConfiguration configuration, IDapper dapper)
        {
            _configuration = configuration;
            this.dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<string> Login(UserVM user)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@Email", user.Email, DbType.String);
                dbparams.Add("@Password", user.Password, DbType.String);
                var result = await Task.FromResult(this.dapper.Get<User>("[dbo].[SP_Login_User]",
                    dbparams, commandType: CommandType.StoredProcedure));
                if (BCrypt.Net.BCrypt.Verify(user.Password, result.Password))
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("User_Email", user.Email),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                return "Password Salah";
            }
            catch (Exception)
            {

                return "Error";
            }
        }
    }
}
