using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebMonitoring.Context;
using WebMonitoring.Dapper_ORM;
using WebMonitoring.Models;

namespace WebMonitoring.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MyContext context;
        readonly IDapper dapper;

        public DashboardController(MyContext context, IDapper dapper)
        {
            this.context = context;
            this.dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDataByPeriode(string periode)
        {
            List<KinerjaPerbankan> kinerjaList = new List<KinerjaPerbankan>();
            kinerjaList = null;
            var dbParams = new DynamicParameters();
            var fixPeriod = DateTime.Parse(periode);
            dbParams.Add("@Periode", fixPeriod, DbType.DateTime);
            var result = Task.FromResult(this.dapper.GetAll<KinerjaPerbankan>("[SP_GetKinerjaByPeriode]",
                dbParams,
                commandType: CommandType.StoredProcedure));
            if (result != null || result.Result.Count != 0)
            {
                kinerjaList = result.Result;
                return Ok(kinerjaList);
            }
            return BadRequest();
        }

    }
}
