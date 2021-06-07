using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using WebMonitoring.Context;
using WebMonitoring.Dapper_ORM;
using WebMonitoring.Models;
using WebMonitoring.ViewModel;

namespace WebMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MyContext context;
        readonly IDapper dapper;

        public HomeController(MyContext context, IDapper dapper)
        {
            this.context = context;
            this.dapper = dapper;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
