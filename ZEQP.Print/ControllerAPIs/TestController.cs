using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEQP.Print.Models;
using ZEQP.Print.Framework;
using Microsoft.Extensions.Hosting;

namespace ZEQP.Print.ControllerAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IHostEnvironment HostEnv { get; set; }
        public TestController(IHostEnvironment hostEnv)
            : base()
        {
            this.HostEnv = hostEnv;
        }
        [HttpGet("[action]")]
        public ActionResult<TestModel> Get()
        {
            var testXps = $"{this.HostEnv.ContentRootPath}\\wwwroot\\Test.xps";
            if (System.IO.File.Exists(testXps))
            {
                var printerName = "KONICA MINOLTA bizhub C226 PCL (10.76.20.30) UPD";
                XpsPrintHelper.Print(testXps, printerName, DateTime.Now.ToString(), false);
                return new TestModel()
                {
                    Id = 1,
                    Name = printerName,
                    CreateTime = DateTime.Now
                };
            }
            else
                return NotFound();
            
        }
    }
}