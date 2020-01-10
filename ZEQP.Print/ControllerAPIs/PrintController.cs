using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEQP.Print.Business;
using ZEQP.Print.Models;

namespace ZEQP.Print.ControllerAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        public IPrintModelService PrintSvc { get; set; }
        public PrintController(IPrintModelService printSvc)
            : base()
        {
            this.PrintSvc = printSvc;
        }
        [HttpPost("[Action]")]
        public async Task<ActionResult<PrintModel>> Print()
        {
            var result = await this.PrintSvc.GetPrintModel();
            return result;
        }
    }
}