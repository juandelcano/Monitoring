using Dapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebMonitoring.Context;
using WebMonitoring.Dapper_ORM;
using WebMonitoring.Models;


namespace WebMonitoring.Controllers
{
    public class KinerjaBankController : Controller
    {
        private readonly IConfiguration config;
        private readonly MyContext context;
        readonly IDapper dapper;

        public KinerjaBankController(IConfiguration config, MyContext context, IDapper dapper)
        {
            this.config = config;
            this.context = context;
            this.dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LihatKinerja()
        {
            return View();
        }

        [HttpGet]
        public List<KinerjaPerbankan> GetAllKinerja()
        {
            List<KinerjaPerbankan> kinerjaList = new List<KinerjaPerbankan>();
            var dbParams = new DynamicParameters();
            var result = Task.FromResult(this.dapper.GetAll<KinerjaPerbankan>("[SP_GetAllKinerja]",
                dbParams,
                commandType: CommandType.StoredProcedure));
            if (result != null || result.Result.Count != 0)
            {
                kinerjaList = result.Result;
            }
            return kinerjaList;
        }

        [HttpPost]
        public ActionResult GetKinerjaByPeriode(string data)
        {
            List<KinerjaPerbankan> kinerjaList = new List<KinerjaPerbankan>();
            kinerjaList = null;
            var dbParams = new DynamicParameters();
            var fixPeriod = DateTime.Parse(data);
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

        [HttpGet]
        public IActionResult UploadDataKinerja(List<KinerjaPerbankan> listKinerja = null)
        {
            listKinerja = listKinerja == null ? new List<KinerjaPerbankan>() : listKinerja;
            return View(listKinerja);
        }

        [HttpPost]
        public IActionResult ViewDataKinerja(IFormFile fileToUpload, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{fileToUpload.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                fileToUpload.CopyTo(fileStream);
                fileStream.Flush();
            }
            var listKinerja = this.GetDataKinerja(fileToUpload.FileName);

            if (listKinerja.Count != 0)
            {
                return Ok(listKinerja);
            }
            return NoContent();

        }

        [HttpPost]
        public IActionResult UploadDataKinerja(IFormFile fileToUpload, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{fileToUpload.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                fileToUpload.CopyTo(fileStream);
                fileStream.Flush();
            }
            var listKinerja = this.GetDataKinerja(fileToUpload.FileName);

            if (listKinerja.Count != 0)
            {
                var dbParams = new DynamicParameters();
                var result = Task.FromResult(this.dapper.Get<int>("[SP_CountDataKinerja]",
                    dbParams,
                    commandType: CommandType.StoredProcedure));
                var count = result.Result;
                var countAfter = 0;
                if (count > 0)
                {
                    this.context.KinerjaPerbankans.AddRange(listKinerja.Where(x => this.context.KinerjaPerbankans.Any(y => y.Periode != x.Periode && y.SandiBank != x.SandiBank)));
                    var resultAfter = Task.FromResult(this.dapper.Get<int>("[SP_CountDataKinerja]",
                        dbParams,
                        commandType: CommandType.StoredProcedure));
                    countAfter = resultAfter.Result;
                }
                else
                {
                    this.context.KinerjaPerbankans.AddRange(listKinerja);
                    var resultAfter = Task.FromResult(this.dapper.Get<int>("[SP_CountDataKinerja]",
                        dbParams,
                        commandType: CommandType.StoredProcedure));
                    countAfter = result.Result;
                }
                this.context.SaveChanges();
                if (count != countAfter)
                {
                    return Ok();
                }
                return BadRequest();

            }
            else
            {
                return BadRequest("Data tidak ada");
            }
            //var kinsejaDr = new 

            //string connectionString = _configuration.GetConnectionString("DevConnection");
            //using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            //{
            //    destinationConnection.Open();
            //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
            //    {
            //        bulkCopy.DestinationTableName = "dbo.TB_M_KinerjaPerbankan";
            //    }
            //}
        }

        private List<KinerjaPerbankan> GetDataKinerja(string fName)
        {
            List<KinerjaPerbankan> listKinerja = new List<KinerjaPerbankan>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {

                        if (!reader.GetValue(0).Equals("Periode"))
                        {
                            DateTime periode = new DateTime();
                            var exPeriode = reader.GetValue(0).ToString().Trim();
                            var sandiBank = reader.GetValue(1).ToString().Trim();
                            var kolKredit1 = Convert.ToDouble(reader.GetValue(2).ToString().Trim());
                            var kolKredit2 = Convert.ToDouble(reader.GetValue(3).ToString().Trim());
                            var kolKredit3 = Convert.ToDouble(reader.GetValue(4).ToString().Trim());
                            var kolKredit4 = Convert.ToDouble(reader.GetValue(5).ToString().Trim());
                            var kolKredit5 = Convert.ToDouble(reader.GetValue(6).ToString().Trim());
                            var laba = Convert.ToDouble(reader.GetValue(7).ToString().Trim());
                            var modal = Convert.ToDouble(reader.GetValue(8).ToString().Trim());
                            var atmr = Convert.ToDouble(reader.GetValue(10).ToString().Trim());
                            var totalAset = Convert.ToDouble(reader.GetValue(9).ToString().Trim());
                            var bebanOperasional = Convert.ToDouble(reader.GetValue(11).ToString().Trim());
                            var pendapatanOperasional = Convert.ToDouble(reader.GetValue(12).ToString().Trim());
                            var danaPihak3 = Convert.ToDouble(reader.GetValue(13).ToString().Trim());
                            periode = getPeriode(exPeriode);
                            double npl = getNPLValue(kolKredit3, kolKredit4, kolKredit5, kolKredit1, kolKredit2);
                            double roe = getROEValue(laba, modal);
                            double car = getCARValue(modal, atmr);
                            double roa = getROAValue(laba, totalAset);
                            double bopo = getBOPOValue(bebanOperasional, pendapatanOperasional);
                            double ldr = getLDRValue(kolKredit1, kolKredit2, kolKredit3, kolKredit4, kolKredit5, danaPihak3);
                            listKinerja.Add(new KinerjaPerbankan
                            {
                                Periode = periode,
                                SandiBank = sandiBank,
                                KreditKol1 = kolKredit1,
                                KreditKol2 = kolKredit2,
                                KreditKol3 = kolKredit3,
                                KreditKol4 = kolKredit4,
                                KreditKol5 = kolKredit5,
                                Laba = laba,
                                Modal = modal,
                                ATMR = atmr,
                                TotalAset = totalAset,
                                BebanOperasional = bebanOperasional,
                                PendapatanOperasional = pendapatanOperasional,
                                DanaPihakKetiga = danaPihak3,
                                NPL = npl,
                                CAR = car,
                                ROA = roa,
                                ROE = roe,
                                BOPO = bopo,
                                LDR = ldr,
                                UserId = 1
                            });
                        }

                    }
                }
            }
            return listKinerja;
        }


        private double getLDRValue(double kolKredit1, double kolKredit2, double kolKredit3, double kolKredit4, double kolKredit5, double danaPihak3)
        {
            var totKredit = kolKredit1 + kolKredit2 + kolKredit3 + kolKredit4 + kolKredit5;
            return totKredit / danaPihak3;
        }

        private double getBOPOValue(double bebanOperasional, double pendapatanOperasional)
        {
            return bebanOperasional / pendapatanOperasional;
        }

        private double getROAValue(double laba, double totalAset)
        {
            return laba / totalAset;
        }

        private double getCARValue(double modal, double atmr)
        {
            return modal / atmr;
        }

        private double getROEValue(double laba, double modal)
        {
            return laba / modal;
        }

        private double getNPLValue(double kolKredit3, double kolKredit4, double kolKredit5, double kolKredit1, double kolKredit2)
        {
            var totKredit = kolKredit1 + kolKredit2 + kolKredit3 + kolKredit4 + kolKredit5;
            var npl = kolKredit3 + kolKredit4 + kolKredit5;
            return npl / totKredit;
        }

        private DateTime getPeriode(string exPeriode)
        {
            var periode = new DateTime();
            if (exPeriode.Contains("/") || exPeriode.Contains("-"))
            {
                periode = DateTime.Parse(exPeriode);
            }
            else
            {
                var year = exPeriode.Substring(0, 4);
                var month = exPeriode.Substring(4, 2);
                var dateP = year + "/" + month;
                periode = DateTime.Parse(dateP);
            }
            return periode;
        }
    }
}
