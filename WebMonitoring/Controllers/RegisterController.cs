using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebMonitoring.Context;
using WebMonitoring.Dapper_ORM;
using WebMonitoring.ViewModel;

namespace WebMonitoring.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MyContext context;
        readonly IDapper dapper;

        public RegisterController(MyContext context, IDapper dapper)
        {
            this.context = context;
            this.dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterNewUser(UserVM data)
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("@Fname", data.FirstName, DbType.String);
            dbParams.Add("@Lname", data.LastName, DbType.String);
            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            dbParams.Add("@Password", data.Password, DbType.String);
            dbParams.Add("@Email", data.Email, DbType.String);
            var result = Task.FromResult(this.dapper.Get<int>("[SP_Register]",
                dbParams,
                commandType: CommandType.StoredProcedure));
            if (result != null || result.Result != 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
